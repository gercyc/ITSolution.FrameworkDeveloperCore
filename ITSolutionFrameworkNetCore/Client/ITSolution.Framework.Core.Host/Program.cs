using ITSolution.Framework.BaseClasses;
using ITSolution.Framework.Core.BaseClasses;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace ITSolution.Framework.Core.Host
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
                .UseStartup<Startup>().UseStaticWebAssets();

            return _webHostBuilder;
        }
    }

    /*
         class Program
    {
        static IWebHostBuilder webHostBuilder;
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args);
            Console.WriteLine("Servidor Iniciado");
            Console.ReadLine();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            string url = string.Format("http://*:{0}", EnvironmentInformation.ServerPort);
            webHostBuilder = WebHost.CreateDefaultBuilder(args).UseUrls(url).UseStartup<Startup>();
            webHostBuilder.Build().Start();
            return webHostBuilder;
        }
    }
     */

}
