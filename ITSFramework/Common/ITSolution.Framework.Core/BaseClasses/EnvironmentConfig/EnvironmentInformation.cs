using System;

namespace ITSolution.Framework.Common.BaseClasses.EnvironmentConfig
{
    public static class EnvironmentInformation
    {
        public static int ServerPort { get { return EnvironmentConfiguration.Instance.ServerPort; } }
        public static string AssemblyRegisterServices { get { return EnvironmentConfiguration.Instance.AssemblyRegisterServices; } }
        public static string ApiAssemblyFolder { get { return EnvironmentConfiguration.Instance.ApiAssemblyFolder; } }
        public static string CoreAssemblyFolder { get { return EnvironmentConfiguration.Instance.CoreAssemblyFolder; } }
        public static string ConnectionString { get { return EnvironmentConfiguration.Instance.ConnectionStrings["DevConnection"]; } }
        public static DatabaseType DatabaseType { get { return EnvironmentConfiguration.Instance.DatabaseType; } }
        public static string DefaultConnectionName { get { return EnvironmentConfiguration.Instance.DefaultConnection; } }
        public static string TnsAtpSource { get { return EnvironmentManager.Configuration.AppConfig.TnsAtp; } }
        public static string WalletLocation { get { return EnvironmentConfiguration.Instance.WalletLocation; } }

        public static ITSApplicationPlataform ApplicationType
        {
            get
            {
                ITSApplicationPlataform applicationType = AppDomain.CurrentDomain.FriendlyName.Contains("w3wp")
                   || AppDomain.CurrentDomain.FriendlyName.Contains("iisexpress")
                   || AppDomain.CurrentDomain.BaseDirectory.Contains("wwwroot") ? ITSApplicationPlataform.Web : ITSApplicationPlataform.Desktop;
                return applicationType;
            }
        }
    }
}
