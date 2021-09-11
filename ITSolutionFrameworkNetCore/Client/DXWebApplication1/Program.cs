using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ITSolution.Framework.BaseClasses;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ITSolution.Framework.Client.App
{
    public class Program
    {
        public static IWebHost DefaultWebHostBuilder { get { return _webHost; } }
        private static IWebHost _webHost;
        static IWebHostBuilder _webHostBuilder;
        public static void Main(string[] args)
        {
            _webHost = CreateWebHostBuilder(args).Build();
            _webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            string url = string.Format("http://*:{0}", EnvironmentInformation.ServerPort);
            string urlHttps = string.Format("https://*:{0}", 5001);

            _webHostBuilder = WebHost.CreateDefaultBuilder(args)
            //.ConfigureKestrel(serverOptions =>
            //{

            //    serverOptions.ConfigureEndpointDefaults(listenOptions =>
            //    {
            //        // Configure endpoint defaults
            //    });
            //});

            //_webHostBuilder.UseKestrel(options =>
            //{
            //    options.Limits.MaxConcurrentConnections = 100;
            //    options.Limits.MaxConcurrentUpgradedConnections = 100;
            //    options.Limits.MaxRequestBodySize = 10 * 1024;
            //    options.Limits.MinRequestBodyDataRate =
            //    new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
            //    options.Limits.MinResponseDataRate =
            //    new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
            //    //options.Listen(IPAddress.Any, 5000);
            //})
                .UseStartup<Startup>()
                .UseUrls(url, urlHttps)
                ;

            return _webHostBuilder;
        }
    }
}