using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using ITSolution.Framework.Core.BaseClasses;
using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
        }

        private Assembly Default_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            if (arg2.Name.Contains("ITSolution"))
                return ITSAssemblyResolve.ITSLoader.LoadFromAssemblyName(arg2);
            else
            {
                AssemblyLoadContext ctx = new AssemblyLoadContext("StandardNET", false);
                return ctx.LoadFromAssemblyName(arg2);
            }
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
                
                IMvcBuilder mvcBuilder = services.AddMvc();
                //starting application parts
                foreach (var file in ITSAssemblyResolve.ITSLoader.GetServerAssemblies())
                {
                    Assembly asm = ITSAssemblyResolve.ITSLoader.Load(file);
                    mvcBuilder.AddApplicationPart(asm);
                }

                foreach (var item in ServiceDescriptors)
                {
                    services.Replace(item);
                }

                services.AddRazorPages();
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
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

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