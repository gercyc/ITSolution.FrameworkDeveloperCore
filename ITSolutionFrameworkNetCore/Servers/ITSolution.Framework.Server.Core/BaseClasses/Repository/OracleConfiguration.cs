using ITSolution.Framework.BaseClasses;
using Oracle.ManagedDataAccess.Client;
//using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository
{
    public static class ITSOracleConfiguration
    {
        /// <summary>
        /// Add oracle tns provider. Needs use instant client or normal client
        /// </summary>
        public static void ConfigureDataSources()
        {
            OracleConfiguration.OracleDataSources.Add(EnvironmentInformation.DefaultConnectionName, EnvironmentInformation.TnsAtpSource);
            OracleConfiguration.WalletLocation = EnvironmentInformation.WalletLocation;
        }
    }
}
