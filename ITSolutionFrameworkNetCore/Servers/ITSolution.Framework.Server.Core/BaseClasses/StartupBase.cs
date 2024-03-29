﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using ITSolution.Framework.Core.BaseClasses;
using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Swagger;
//using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ITSolution.Framework.Core.Server.BaseClasses.Repository.Identity;
using ITSolution.Framework.BaseClasses;
using ITSolution.Framework.Core.Server.BaseClasses.Repository;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.FileProviders;

namespace ITSolution.Framework.Core.Server.BaseClasses
{
    public abstract class StartupBase : IStartup
    {
        protected StartupBase()
        {
        }

        protected abstract IServiceCollection ServiceDescriptors { get; set; }

        protected StartupBase(IConfiguration configuration) : this()
        {
            Configuration = configuration;

            //assinando o evento para definir quem vai resolver os assemblies
            //AssemblyLoadContext.Default.Resolving += Default_Resolving;
            ITSAssemblyResolve.ITSLoader.Resolving += Default_Resolving;
        }

        private Assembly Default_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            Assembly ret = null;
            if (arg2.Name.Contains("ITSolution"))
                return ITSAssemblyResolve.ITSLoader.LoadFromAssemblyName(arg2);
            else
            {
                 ret = AssemblyLoadContext.Default.LoadFromAssemblyName(arg2);
                //return Assembly.Load(arg2);
            }

            return ret;
        }

        protected virtual IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.ConfigureIdentity();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            try
            {
                services.AddDatabaseDeveloperPageExceptionFilter();

                IMvcBuilder mvcBuilder = services.AddMvc();
                //starting application parts
                foreach (var file in ITSAssemblyResolve.ITSLoader.GetServerAssemblies())
                {
                    Assembly asm = ITSAssemblyResolve.Default.LoadFromAssemblyPath(file);
                    mvcBuilder.AddApplicationPart(asm)
                            .AddRazorRuntimeCompilation(options =>
                            {
                                options.FileProviders.Add(new EmbeddedFileProvider(asm));
                            });
                }

                foreach (var item in ServiceDescriptors)
                {
                    services.Replace(item);
                }
                services.AddSwaggerGen();
                services.AddRazorPages();
                services.AddControllersWithViews();
                services.AddScoped<IDbContextOptions, ItsDbContextOptions>();
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
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();

            });

        }
    }
}