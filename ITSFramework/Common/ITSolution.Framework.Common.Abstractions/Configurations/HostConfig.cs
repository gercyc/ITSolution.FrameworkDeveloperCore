using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ITSolution.Framework.Common.Abstractions.Configurations;

public static class CustomConfiguration
{
    /// <summary>
    /// Sobrescrever o local padrão das configurações do aplicativo
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    public static IWebHostBuilder ConfigureAppConfiguration(this IWebHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, config) =>
        {
            const string configurationsDirectory = "Configuration";
            var env = context.HostingEnvironment;
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/EnvironmentConfiguration.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/appsettings.json", optional: true, reloadOnChange: true)
                
                .AddEnvironmentVariables();
        });
        
        return host;
    }

    /// <summary>
    /// Sobrescrever o local padrão das configurações do aplicativo
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    public static ConfigureHostBuilder ConfigureAppConfiguration(this ConfigureHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, config) =>
        {
            const string configurationsDirectory = "Configuration";
            var env = context.HostingEnvironment;
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/EnvironmentConfiguration.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{configurationsDirectory}/appsettings.json", optional: true, reloadOnChange: true)

                .AddEnvironmentVariables();
        });

        return host;
    }
}