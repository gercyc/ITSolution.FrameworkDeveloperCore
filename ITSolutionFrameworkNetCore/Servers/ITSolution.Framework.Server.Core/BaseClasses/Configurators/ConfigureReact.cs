using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITSolution.Framework.Core.Server.BaseClasses.Configurators
{
    public static class ConfigureReactExtenstions
    {
        public static void ConfigureReact(this IServiceCollection services)
        {
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }
        public static void UseReact(this IApplicationBuilder builder, IHostEnvironment env)
        {

            //react
            builder.UseSpaStaticFiles();

            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            builder.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

        }
    }
}
