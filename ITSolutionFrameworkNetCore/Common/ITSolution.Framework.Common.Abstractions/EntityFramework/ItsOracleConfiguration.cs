using ITSolution.Framework.Core.Common.BaseClasses.EnvironmentConfig;
using Oracle.ManagedDataAccess.Client;
//using Oracle.ManagedDataAccess.Client;

namespace ITSolution.Framework.Common.Abstractions.EfOptions
{
    public static class ItsOracleConfiguration
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
