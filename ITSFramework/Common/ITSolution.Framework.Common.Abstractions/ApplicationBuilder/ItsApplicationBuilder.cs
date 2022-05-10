using Hangfire;
using ITSolution.Framework.Common.Abstractions.Configurations;
using ITSolution.Framework.Common.Abstractions.EntityFramework;
using ITSolution.Framework.Common.Abstractions.Identity;
using ITSolution.Framework.Common.Abstractions.Modules;
using ITSolution.Framework.Common.Abstractions.OpenApi;
using ITSolution.Framework.Common.BaseClasses.EnvironmentConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

#pragma warning disable CS8618

namespace ITSolution.Framework.Common.Abstractions.ApplicationBuilder
{
    /// <summary>
    /// Aplicação padrão IT Solution Framework
    /// </summary>
    public class ItsApplicationBuilder
    {
        public WebApplicationBuilder WebApplicationBuilder { get; }
        public WebApplication WebApplication { get; private set; }

        string ITSolutionCorsPolicy = "ITSolutionCorsPolicy";
        
        public ItsApplicationBuilder(string[] args)
        {
            //AppDomain.CurrentDomain.
            WebApplicationBuilder = WebApplication.CreateBuilder(args);

            //Add custom configurations
            WebApplicationBuilder.Host.ConfigureAppConfiguration();
            // Add services to the container.
            WebApplicationBuilder.Services.AddRazorPages();
            WebApplicationBuilder.Services.AddScoped(typeof(ItsDbContextOptions));
            WebApplicationBuilder.Services.AddIdentity(useDefaultUI: true);

            WebApplicationBuilder.Services.AddCors(options =>
            {
                options.AddPolicy(name: ITSolutionCorsPolicy,
                    builder =>
                    {
                        builder.WithOrigins("*");
                        builder.WithHeaders(HeaderNames.ContentType, "x-custom-header");
                        builder.WithMethods("PUT", "DELETE", "GET");
                    });
            });

            WebApplicationBuilder.Services.AddHttpContextAccessor();
            WebApplicationBuilder.Services.AddHangfire(config => config.UseSqlServerStorage(EnvironmentConfiguration.Instance.ConnectionStrings["DevConnection"]));
            WebApplicationBuilder.Services.AddHangfireServer();
            WebApplicationBuilder.Services.AddDatabaseDeveloperPageExceptionFilter();
            WebApplicationBuilder.Services.AddOpenApi(WebApplicationBuilder.Configuration);
            WebApplicationBuilder.Services.AddApplicationParts();
        }

        private void ConfigureApplication()
        {
            WebApplication = WebApplicationBuilder.Build();

            // Configure the HTTP request pipeline.
            if (!WebApplication.Environment.IsDevelopment())
            {
                WebApplication.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                WebApplication.UseHsts();
            }
            WebApplication.UseHttpsRedirection();
            WebApplication.UseStaticFiles();
            WebApplication.UseCors(ITSolutionCorsPolicy);
            WebApplication.UseRouting();
            WebApplication.UseAuthentication();
            WebApplication.UseAuthorization();

            //hangfire jobs
            WebApplication.UseHangfireDashboard("/jobs", new DashboardOptions());
            WebApplication.UseOpenApiDocumentation(WebApplicationBuilder.Configuration);
            //WebApplication.MapRazorPages();
            //WebApplication.MapControllers();

            WebApplication.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Inicializar o aplicativo. 
        /// </summary>
        public void StartApp()
        {
            ConfigureApplication();
            WebApplication.Run();
        }

    }
}

