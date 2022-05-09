using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ITSolution.Framework.Common.Abstractions.Configurations;

namespace ITSolution.Framework.Core.AspHost
{
    public class Program
    {
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
