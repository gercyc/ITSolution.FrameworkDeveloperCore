﻿namespace ITSolution.Framework.Core.Common.BaseClasses.Tools
{
    public abstract class ConnectionFile
    {
        public string ConnectionString { get; private set; }//somente leitura
        public string PathFile { get; set; }

        public ConnectionFile(string connectionString, string file)
        {
            this.ConnectionString = connectionString;
            this.PathFile = file;
        }

    }
}
