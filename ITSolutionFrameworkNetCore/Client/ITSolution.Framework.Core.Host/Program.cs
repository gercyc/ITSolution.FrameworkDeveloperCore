using ITSolution.Framework.Core.Server.BaseClasses.Configurators;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ITSolution.Framework.Core.Host
{
    class Program
    {
        //static IWebHostBuilder _webHostBuilder;
        //static void Main(string[] args)
        //{
        //    CreateWebHostBuilder(args);
        //    Console.WriteLine("Servidor Iniciado");
        //    Console.ReadLine();
        //}

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        //{

        //    _webHostBuilder = WebHost.CreateDefaultBuilder<Startup>(args).ConfigureAppConfiguration();
        //    string url = $"http://*:{EnvironmentInformation.ServerPort}";
        //    _webHostBuilder.UseUrls(url).UseEnvironment("Development");
        //    _webHostBuilder.Build().Start();
        //    return _webHostBuilder;
        //}
        private static IWebHost _webHost;
        static IWebHostBuilder _webHostBuilder;
        public static void Main(string[] args)
        {
            _webHost = CreateWebHostBuilder(args).Build();
            _webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            _webHostBuilder = WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration()
                .UseStartup<Startup>();
            return _webHostBuilder;
        }
    }

}
