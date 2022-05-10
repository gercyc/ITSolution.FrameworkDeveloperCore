using Hangfire;
using ITSolution.Framework.Core.Server.BaseInterfaces;
using ITSolution.Framework.Core.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using ITSolution.Framework.Common.Abstractions.EntityFramework;
using ITSolution.Framework.Common.Abstractions.Identity;
using ITSolution.Framework.Common.Abstractions.Modules;
using ITSolution.Framework.Common.Abstractions.OpenApi;
using ITSolution.Framework.Common.BaseClasses;
using ITSolution.Framework.Common.BaseClasses.EnvironmentConfig;

namespace ITSolution.Framework.Core.Server.BaseClasses
{
    public abstract class StartupBase : IStartup
    {
        private readonly IConfiguration _configuration;

        string ITSolutionCorsPolicy = "ITSolutionCorsPolicy";
        protected StartupBase()
        {
        }

        protected abstract IServiceCollection ServiceDescriptors { get; set; }

        protected StartupBase(IConfiguration configuration) : this()
        {
            _configuration = configuration;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Utils.ShowExceptionStack(e.ExceptionObject as Exception);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddIdentity(useDefaultUI: false);
                services.AddRazorPages();
                services.AddScoped(typeof(ItsDbContextOptions));
                services.AddCookiePolicy(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
                });

                services.AddJwtAuth(_configuration);
                services.AddScoped<ITokenService, TokenService>();
                

                services.AddCors(options =>
                {
                    options.AddPolicy(name: ITSolutionCorsPolicy,
                        builder =>
                        {
                            builder.WithOrigins("*");
                            builder.WithHeaders(HeaderNames.ContentType, "x-custom-header");
                            builder.WithMethods("PUT", "DELETE", "GET");
                        });
                });

                services.AddHttpContextAccessor();
                services.AddHangfire(config => config.UseSqlServerStorage(EnvironmentConfiguration.Instance.ConnectionStrings["DevConnection"]));
                services.AddHangfireServer();
                services.AddApplicationParts();
             
                services.AddDatabaseDeveloperPageExceptionFilter();
                services.AddOpenApi(_configuration);

                foreach (var item in ServiceDescriptors)
                {
                    services.Replace(item);
                }

                //standard ITS framework. nao remover.
                return services.BuildServiceProvider();
            }
            catch (Exception ex)
            {
                Utils.ShowExceptionStack(ex);
                throw;
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetService<IHostEnvironment>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            app.UseStaticFiles();
            if (Directory.Exists(staticFilesPath))
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(staticFilesPath),
                    RequestPath = "/Uploads"
                });
            }

            app.UseCors(ITSolutionCorsPolicy);
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //hangfire jobs
            app.UseHangfireDashboard("/jobs", new DashboardOptions() { Authorization = new[] { new AuthorizationFilter() } });
            app.UseOpenApiDocumentation(_configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}