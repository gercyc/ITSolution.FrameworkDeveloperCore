using ITSolution.Framework.BaseClasses;
using ITSolution.Framework.Core.BaseClasses;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository
{
    public class ConnectionFactory
    {
        DbConnection _connection;
        public DbConnection ITSDbConnection
        {
            get
            {
                try
                {
                    if (EnvironmentInformation.DatabaseType == DatabaseType.Oracle)
                        _connection = new OracleConnection(EnvironmentInformation.ConnectionString);

                    return _connection;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void Open()
        {
            if (_connection.State != System.Data.ConnectionState.Open)
                _connection.Open();

        }
        private void Close()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();

        }

        public DataTable ExecuteQuery(string _commandText)
        {
            DataTable dt = new DataTable();
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter((OracleCommand)CreateCommand(_commandText));

            oracleDataAdapter.Fill(dt);

            return dt;
        }
        public DbCommand CreateCommand(string _commandText)
        {
            DbCommand dbCommand = _connection.CreateCommand();
            dbCommand.CommandText = _commandText;

            return dbCommand;
        }

        public ConnectionFactory()
        {

        }
    }
}
