using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ITSolution.Framework.Common.BaseClasses.EnvironmentConfig;

public class EnvironmentConfiguration
{
    private static EnvironmentConfiguration _environmentConfiguration;

    public static EnvironmentConfiguration Instance
    {
        get
        {
            if (_environmentConfiguration == null)
                _environmentConfiguration = Initialize();
            return _environmentConfiguration;
        }
    }

    public string AssemblyRegisterServices { get; set; }
    public int ServerPort { get; set; }
    public string ApiAssemblyFolder { get; set; }
    public string CoreAssemblyFolder { get; set; }
    public DatabaseType DatabaseType { get; set; }
    public string DefaultConnection { get; set; }
    public Dictionary<string, string> ConnectionStrings { get; set; }
    public string WalletLocation { get; set; }

    private static EnvironmentConfiguration Initialize()
    {
        string json = File.ReadAllText(ConnectionConfigPath);
        var obj = JsonConvert.DeserializeObject<EnvironmentConfiguration>(json);
        return obj;
    }

    /// <summary>
    /// O path do arquivo de configuração de conexão
    ///     Observação: O arquivo ITSConfig.xml está localizado em ITSolution.Framework.Configuration\
    /// </summary>
    public static string ConnectionConfigPath
    {
        get
        {
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;
            string xmlPath = Path.Combine(startupPath, "Configuration", "EnvironmentConfiguration.json");
            return xmlPath;
        }
    }
}