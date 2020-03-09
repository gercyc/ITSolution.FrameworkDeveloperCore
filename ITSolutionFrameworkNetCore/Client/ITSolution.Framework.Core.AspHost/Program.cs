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

namespace ITSolution.Framework.Core.AspHost
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

            _webHostBuilder = WebHost.CreateDefaultBuilder(args).UseUrls(url)
                .UseStartup<Startup>();

            return _webHostBuilder;
        }
    }
}
