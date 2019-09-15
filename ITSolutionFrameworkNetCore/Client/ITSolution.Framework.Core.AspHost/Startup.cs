using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using ITSolution.Framework.Core.BaseClasses;
using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ITSolution.Framework.Core.AspHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //assinando o evento para definir quem vai resolver os assemblies
            AssemblyLoadContext.Default.Resolving += Default_Resolving;
        }
        private Assembly Default_Resolving(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            return ITSAssemblyResolve.ITSLoader.LoadFromAssemblyName(arg2);
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            IMvcBuilder mvcBuilder = services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            try
            {
                //starting application parts
                foreach (var file in ITSAssemblyResolve.ITSLoader.GetServerAssemblies())
                {
                    Assembly asm = ITSAssemblyResolve.ITSLoader.Load(file);
                    Type[] types = asm.GetTypes().Where(t => t.BaseType.Name.Contains("ITSolutionContext")).ToArray();
                    if (types != null)
                    {
                        services.AddDbContext<ITSolutionContext>();

                        ITSDbContextOptions dbContextOptions = new ITSDbContextOptions();
                        object instance = Activator.CreateInstance(types[0], dbContextOptions);
                        ServiceDescriptor descriptor = new ServiceDescriptor(typeof(DbContext), typeof(ITSolutionContext), ServiceLifetime.Scoped);
                        services.Replace(descriptor);
                    }
                    mvcBuilder.AddApplicationPart(asm);
                }
            }
            catch (Exception ex)
            {
                Utils.ShowExceptionStack(ex);
                throw ex;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
