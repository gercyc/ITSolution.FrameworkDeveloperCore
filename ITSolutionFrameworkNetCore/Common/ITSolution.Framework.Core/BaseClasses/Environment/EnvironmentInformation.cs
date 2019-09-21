using ITSolution.Framework.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSolution.Framework.BaseClasses
{
    public static class EnvironmentInformation
    {
        public static int ServerPort { get { return EnvironmentManager.Configuration.ServerPort; } }
        public static string AssemblyRegisterServices { get { return EnvironmentManager.Configuration.AsmRegisterServices; } }
        public static string APIAssemblyFolder { get { return EnvironmentManager.Configuration.APIAssemblyFolder; } }
        public static string CoreAssemblyFolder { get { return EnvironmentManager.Configuration.CoreAssemblyFolder; } }
        public static string ConnectionString { get { return EnvironmentManager.Configuration.ConnectionString; } }
        public static DatabaseType DatabaseType { get { return EnvironmentManager.Configuration.DatabaseType; } }
        public static string DefaultConnectionName { get { return EnvironmentManager.Configuration.DefaultConnectionName; } }
        public static string TnsAtpSource { get { return EnvironmentManager.Configuration.AppConfig.TnsAtp; } }
        public static string WalletLocation { get { return EnvironmentManager.Configuration.WalletLocation; } }

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
