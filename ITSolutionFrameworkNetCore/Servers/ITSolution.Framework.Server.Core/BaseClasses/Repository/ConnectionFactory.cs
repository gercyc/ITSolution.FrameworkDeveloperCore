using ITSolution.Framework.BaseClasses;
using ITSolution.Framework.Core.BaseClasses;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Internal;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository
{
    public class ConnectionFactory
    {
        #region Declarations

        readonly object lockOb = new object();
        private static ConcurrentDictionary<string, ConnectionData> _connections;
        private static ConnectionFactory _instance;
        private static ConcurrentBag<DbDataReader> _readers;

        #endregion

        #region Private methods

        /// <summary>
        /// Método é executado o tempo todo. Se houver alguma conexão aberta por mais de 10 segundos, a mesma é fechada e removida do pool
        /// </summary>
        private void StartTaskWatcher()
        {
            lock (lockOb)
            {
                Task taskWatcher = new Task
                (() =>
                {
                    while (true)
                    {
                        double interval = TimeSpan.FromSeconds(10).TotalSeconds;
                        int intervalVerifyTask = (int) TimeSpan.FromSeconds(2).TotalMilliseconds;
                        Thread.Sleep(intervalVerifyTask);
                        foreach (var conn in _connections.Where(
                            c => c.Value.SecondsOpened > interval))
                        {
                            conn.Value.StopTimer();
                            if (
                                CloseConnection(conn.Value))
                            {
                                _connections.TryRemove(conn.Key, out ConnectionData cn);

                                Console.WriteLine(
                                    $"{DateTime.Now}: Uma conexão aberta por mais de {interval} segundos foi removida do pool");
                            }
                        }
                    }
                });

                taskWatcher.Start();
            }
        }

        private DbConnection GetConnection()
        {
            lock (lockOb)
            {
                DbConnection conn = null;
                if (EnvironmentInformation.DatabaseType == DatabaseType.Oracle)
                    conn = new OracleConnection(EnvironmentInformation.ConnectionString);
                else if (EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
                    conn = new SqlConnection(EnvironmentInformation.ConnectionString);

                return conn;
            }
        }

        private ConnectionState OpenConnection(ConnectionData connectionData)
        {
            lock (lockOb)
            {
                if (connectionData.DbConnection.State != System.Data.ConnectionState.Open
                    && connectionData.DbConnection.State != ConnectionState.Connecting
                    || connectionData.DbConnection.State == ConnectionState.Closed)
                {
                    connectionData.DbConnection.Open();
                }

                return connectionData.DbConnection.State;
            }
        }

        private bool CloseConnection(ConnectionData connectionData)
        {
            lock (lockOb)
            {
                bool isClosed = false;
                if (connectionData.DbConnection.State == System.Data.ConnectionState.Open &&
                    _readers.All(r => r.IsClosed))
                {
                    connectionData.DbConnection.Close();
                    isClosed = connectionData.DbConnection.State == ConnectionState.Closed;
                    _readers.Clear();
                }

                return isClosed;
            }
        }

        private ConnectionData GetConnectionData()
        {
            //tenta obter uma conexao
            lock (lockOb)
            {
                ConnectionData currentConnection;

                _connections.TryGetValue(EnvironmentInformation.ConnectionString,
                    out currentConnection);

                if (currentConnection == null)
                {
                    try
                    {
                        //se nao encontrou, tente criar uma e add
                        var cn = GetConnection();
                        if (_connections.TryAdd(cn.ConnectionString, new ConnectionData(cn)))
                        {
                            Console.WriteLine($"{DateTime.Now}: Conexão adicionada ao pool");
                            //tente obter a conexao
                            _connections.TryGetValue(EnvironmentInformation.ConnectionString,
                                out currentConnection);
                        }

                        //se nao encontrou, lanca exception
                        if (currentConnection == null)
                            throw new Exception("Não foi possível obter uma conexão! ");
                    }
                    catch (Exception e)
                    {
                        Utils.ShowExceptionStack(e);
                    }
                }

                return currentConnection;
            }
        }

        private DbCommand CreateCommand(string commandText, ConnectionData connectionData,
            ParameterList parameterList = null)
        {
            DbCommand dbCommand = connectionData.DbConnection.CreateCommand();
            dbCommand.CommandType = CommandType.Text;
            dbCommand.CommandTimeout = 90;
            if (parameterList != null)
            {
                string separator = EnvironmentInformation.DatabaseType == DatabaseType.Oracle ? ":" : "@";

                commandText += UtilsParameters.GetWhereCondition(parameterList);

                foreach (CustomDbParameter parameter in parameterList)
                {
                    if (EnvironmentInformation.DatabaseType == DatabaseType.Oracle)
                        dbCommand.Parameters.Add(new OracleParameter(parameter.ParameterName, parameter.Value));
                    else
                        dbCommand.Parameters.Add(new SqlParameter(parameter.ParameterName, parameter.Value));
                }
            }

            dbCommand.CommandText = commandText;
            return dbCommand;
        }

        private DataTable InternalExecuteQuery(string tableName, string commandText, ParameterList parameterList = null)
        {
            lock (lockOb)
            {
                DataTable dt = new DataTable(tableName);
                try
                {
                    ConnectionData currentConnection = GetConnectionData();

                    //se achou uma conexao, continue
                    if (OpenConnection(currentConnection) == ConnectionState.Open)
                    {
                        if (EnvironmentInformation.DatabaseType == DatabaseType.Oracle)
                        {
                            OracleDataAdapter oracleDataAdapter =
                                new OracleDataAdapter(
                                    (OracleCommand) CreateCommand(commandText,
                                        currentConnection, parameterList));
                            oracleDataAdapter.Fill(dt);
                        }
                        else if (EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
                        {
                            SqlDataAdapter sqlDataAdapter =
                                new SqlDataAdapter((SqlCommand) CreateCommand(commandText,
                                    currentConnection, parameterList));
                            sqlDataAdapter.Fill(dt);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Utils.ShowExceptionStack(exception);
                }

                return dt;
            }
        }

        private DbDataReader InternalExecuteReader(string commandText, ParameterList parameterList = null)
        {
            lock (lockOb)
            {
                DbDataReader dbDataReader = null;
                try
                {
                    ConnectionData currentConnection = GetConnectionData();

                    //se achou uma conexao, continue
                    if (OpenConnection(currentConnection) == ConnectionState.Open)
                    {
                        if (EnvironmentInformation.DatabaseType == DatabaseType.Oracle)
                        {
                            dbDataReader =
                                ((OracleCommand) CreateCommand(commandText, currentConnection, parameterList))
                                .ExecuteReader();
                        }
                        else if (EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
                        {
                            dbDataReader = ((SqlCommand) CreateCommand(commandText, currentConnection, parameterList))
                                .ExecuteReader();
                        }

                        _readers.Add(dbDataReader);
                    }
                }
                catch (Exception exception)
                {
                    Utils.ShowExceptionStack(exception);
                }

                return dbDataReader;
            }
        }

        #endregion

        private ConnectionFactory()
        {
            if(EnvironmentInformation.DatabaseType == DatabaseType.Oracle)
                ITSOracleConfiguration.ConfigureDataSources();
            
            if (_connections == null)
                _connections = new ConcurrentDictionary<string, ConnectionData>();

            if (_readers == null)
                _readers = new ConcurrentBag<DbDataReader>();

            StartTaskWatcher();
        }

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static ConnectionFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConnectionFactory();

                return _instance;
            }
        }

        public DataTable ExecuteQuery(string tableName, string commandText)
        {
            return InternalExecuteQuery(tableName, commandText);
        }

        public DbDataReader ExecuteReader(string commandText, ParameterList parameterList = null)
        {
            return InternalExecuteReader(commandText, parameterList);
        }
    }
}