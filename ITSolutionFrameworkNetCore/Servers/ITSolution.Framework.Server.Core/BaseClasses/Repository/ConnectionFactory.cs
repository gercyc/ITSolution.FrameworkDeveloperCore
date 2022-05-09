//using Oracle.ManagedDataAccess.Client;
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
using Microsoft.EntityFrameworkCore.Internal;
using Oracle.ManagedDataAccess.Client;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using ITSolution.Framework.Common.Abstractions.EfOptions;
using ITSolution.Framework.Common.BaseClasses;
using ITSolution.Framework.Common.BaseClasses.EnvironmentConfig;

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
        /// Método é executado o tempo todo. Se houver alguma conexão aberta por mais de 30 segundos, a mesma é fechada e removida do pool
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
                        double interval = TimeSpan.FromSeconds(30).TotalSeconds;
                        int intervalVerifyTask = (int)TimeSpan.FromSeconds(2).TotalMilliseconds;
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
                {
                    conn = new OracleConnection(EnvironmentInformation.ConnectionString);
                }
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
            ParameterList parameterList = null, bool addWhereCondition = false)
        {
            DbCommand dbCommand = connectionData.DbConnection.CreateCommand();
            dbCommand.CommandType = CommandType.Text;
            dbCommand.CommandTimeout = 90;
            if (parameterList != null)
            {
                string separator = EnvironmentInformation.DatabaseType == DatabaseType.Oracle ? ":" : "@";

                if (addWhereCondition)
                    commandText += UtilsParameters.GetWhereCondition(parameterList);

                foreach (CustomDbParameter parameter in parameterList)
                {
                    if (EnvironmentInformation.DatabaseType == DatabaseType.Oracle)
                    {
                        dbCommand.Parameters.Add(new OracleParameter(parameter.ParameterName, parameter.Value));
                    }
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
                                    (OracleCommand)CreateCommand(commandText,
                                        currentConnection, parameterList));
                            oracleDataAdapter.Fill(dt);
                        }
                        else if (EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
                        {
                            SqlDataAdapter sqlDataAdapter =
                                new SqlDataAdapter((SqlCommand)CreateCommand(commandText,
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
                                ((OracleCommand)CreateCommand(commandText, currentConnection, parameterList))
                                .ExecuteReader();
                        }
                        else if (EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
                        {
                            dbDataReader = ((SqlCommand)CreateCommand(commandText, currentConnection, parameterList))
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
            if (EnvironmentInformation.DatabaseType == DatabaseType.Oracle)
                ItsOracleConfiguration.ConfigureDataSources();

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
        public bool BatchUpdate<T>(DataTable dtChanges, T type, bool insert = false)
        {
            bool result = false;
            List<DataColumn> cols = new List<DataColumn>();

            var props = type.GetType().GetProperties().Where(a => a.GetCustomAttribute<ForeignKeyAttribute>() != null);

            foreach (var item in props)
            {
                dtChanges.Columns.Remove(item.Name);
            }

            try
            {
                //TODO:
                lock (lockOb)
                {
                    ConnectionData currentConnection = GetConnectionData();

                    //se achou uma conexao, continue
                    if (OpenConnection(currentConnection) == ConnectionState.Open)
                    {
                        if (EnvironmentInformation.DatabaseType == DatabaseType.Oracle)
                        {
                            OracleDataAdapter oracleDataAdapter =
                                new OracleDataAdapter();
                        }
                        else if (EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
                        {
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                            sqlDataAdapter.UpdateCommand = (SqlCommand)CreateCommand(GetUpdateCommand(dtChanges), currentConnection, new ParameterList());
                            sqlDataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;

                            sqlDataAdapter.InsertCommand = (SqlCommand)CreateCommand(GetInsertCommand(dtChanges), currentConnection, new ParameterList());
                            sqlDataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;

                            sqlDataAdapter.DeleteCommand = (SqlCommand)CreateCommand(GetDeleteCommand(dtChanges), currentConnection, new ParameterList());
                            sqlDataAdapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;

                            sqlDataAdapter.RowUpdating += SqlDataAdapter_RowUpdating;
                            sqlDataAdapter.UpdateBatchSize = 1000;
                            Console.WriteLine($"BatchUpdateSize = {sqlDataAdapter.UpdateBatchSize}");
                            sqlDataAdapter.Update(dtChanges);

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Utils.ShowExceptionStack(ex);
            }

            return result;
        }

        private void SqlDataAdapter_RowUpdating(object sender, SqlRowUpdatingEventArgs e)
        {
            e.Command.Parameters.Clear();
            //DataTable dt = e.Row.Table;

            string separator = EnvironmentInformation.DatabaseType == DatabaseType.Oracle ? ":" : "@";

            foreach (CustomDbParameter parameter in GetParameters(e.Row))
            {
                if (EnvironmentInformation.DatabaseType == DatabaseType.Oracle)
                {
                    e.Command.Parameters.Add(new OracleParameter(string.Format("{0}{1}", separator, parameter.ParameterName), parameter.Value));
                }
                else
                    e.Command.Parameters.Add(new SqlParameter(string.Format("{0}{1}", separator, parameter.ParameterName), parameter.Value));
            }
        }

        private string GetUpdateCommand(DataTable dt)
        {
            string updateText = string.Empty;
            try
            {
                string separator = EnvironmentInformation.DatabaseType == DatabaseType.Oracle ? ":" : "@";
                StringBuilder sbUpdate = new StringBuilder();
                sbUpdate.AppendFormat("UPDATE {0} SET ", dt.TableName);

                foreach (DataColumn col in dt.Columns)
                {
                    //update only fields outside primarykey
                    if (!dt.PrimaryKey.Contains(col) && col.ColumnName != "Id")
                        sbUpdate.AppendFormat("{0} = {1}{2}, ", col.ColumnName, separator, col.ColumnName);
                }
                updateText = sbUpdate.ToString().Substring(0, sbUpdate.Length - 2);

                sbUpdate.Clear();
                sbUpdate.Append(" WHERE 1 = 1 ");

                foreach (DataColumn dtKey in dt.PrimaryKey)
                {
                    sbUpdate.AppendFormat(" AND {0} = {1}{2} ", dtKey.ColumnName, separator, dtKey.ColumnName);
                }

                updateText += sbUpdate.ToString();

            }
            catch (Exception ex)
            {
                Utils.ShowExceptionStack(ex);
            }

            return updateText;
        }

        private string GetDeleteCommand(DataTable dt)
        {
            string deleteText = string.Empty;
            try
            {
                string separator = EnvironmentInformation.DatabaseType == DatabaseType.Oracle ? ":" : "@";
                StringBuilder sbDelete = new StringBuilder();
                sbDelete.AppendFormat("DELETE FROM {0}", dt.TableName);

                sbDelete.Append(" WHERE 1 = 1 ");

                foreach (DataColumn dtKey in dt.PrimaryKey)
                {
                    sbDelete.AppendFormat(" AND {0} = {1}{2} ", dtKey.ColumnName, separator, dtKey.ColumnName);
                }

                deleteText = sbDelete.ToString();
                sbDelete.Clear();

            }
            catch (Exception ex)
            {
                Utils.ShowExceptionStack(ex);
            }

            return deleteText;
        }

        private string GetInsertCommand(DataTable dt)
        {
            string insertText = string.Empty;
            try
            {
                string separator = EnvironmentInformation.DatabaseType == DatabaseType.Oracle ? ":" : "@";
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat("INSERT INTO {0} (", dt.TableName);

                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName != "Id")
                        sbInsert.AppendFormat("{0}, ", col.ColumnName);
                }

                insertText = sbInsert.ToString().Substring(0, sbInsert.Length - 2);
                sbInsert.Clear();
                sbInsert.Append(") VALUES ( ");

                foreach (DataColumn dtKey in dt.Columns)
                {
                    if (dtKey.ColumnName != "Id")
                        sbInsert.AppendFormat(" {0}{1}, ", separator, dtKey.ColumnName);
                }
                insertText += sbInsert.ToString().Substring(0, sbInsert.Length - 2);
                insertText += ")";

            }
            catch (Exception ex)
            {
                Utils.ShowExceptionStack(ex);
            }

            return insertText;
        }

        private ParameterList GetParameters(DataTable dt, bool primaryKey = false)
        {
            ParameterList customDbParameters = new ParameterList();
            try
            {
                if (!primaryKey)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        customDbParameters.Add(col.ColumnName, Operator.Equals, null, col.DataType);
                    }
                }
                else
                {
                    foreach (DataColumn col in dt.PrimaryKey)
                    {
                        customDbParameters.Add(col.ColumnName, Operator.Equals, null, col.DataType);
                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    if (!primaryKey)
                    {
                        for (int i = 0; i < row.ItemArray.Length; i++)
                        {
                            customDbParameters[i].Value = row[i];
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dt.PrimaryKey.Length; i++)
                        {
                            customDbParameters[i].Value = row[i];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowExceptionStack(ex);
            }
            return customDbParameters;

        }

        private ParameterList GetParameters(DataRow row, bool primaryKey = false)
        {
            ParameterList customDbParameters = new ParameterList();
            try
            {
                if (!primaryKey)
                {
                    foreach (DataColumn col in row.Table.Columns)
                    {
                        customDbParameters.Add(col.ColumnName, Operator.Equals, null, col.DataType);
                    }
                }
                else
                {
                    foreach (DataColumn col in row.Table.PrimaryKey)
                    {
                        customDbParameters.Add(col.ColumnName, Operator.Equals, null, col.DataType);
                    }
                }

                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    customDbParameters[i].Value = row[i];
                }

            }
            catch (Exception ex)
            {
                Utils.ShowExceptionStack(ex);
            }
            return customDbParameters;

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