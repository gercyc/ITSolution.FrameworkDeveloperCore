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
using Hangfire;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using ITSolution.Framework.Core.Server.BaseClasses.Configurators;
using System.IO;
using Newtonsoft.Json.Converters;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.FileProviders;
//using Microsoft.Net.Http.Headers;

namespace ITSolution.Framework.Core.Server.BaseClasses
{
    public abstract class StartupBase : IStartup
    {
        string HunterPolicy = "HunterPolicy";
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
            return ItsAssemblyResolve.ItsLoader.LoadFromAssemblyName(arg2);
        }

        protected virtual IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.ConfigureIdentity();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            services.AddHttpContextAccessor();

            services.AddHangfire(config =>
                config.UseSqlServerStorage(EnvironmentManager.Configuration.ConnectionString));

            try
            {

                IMvcBuilder mvcBuilder = services.AddMvc()
                    .AddNewtonsoftJson(o =>
                    {
                        o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        o.SerializerSettings.Converters.Add(new StringEnumConverter());
                    });

                //starting application parts
                //foreach (var file in ItsAssemblyResolve.ItsLoader.GetServerAssemblies())
                //{
                //    Assembly asm = ItsAssemblyResolve.ItsLoader.Load(file);
                //    mvcBuilder.AddApplicationPart(asm);
                //}

                foreach (var item in ServiceDescriptors)
                {
                    services.Replace(item);
                }

                services.AddRazorPages();

                //TODO:
                //implementar logica de carregar xml's de documentacao de assemblies...
                //string caminhoXmlDoc = Path.Combine(EnvironmentInformation.APIAssemblyFolder, "Hunter.API.CadastrosBasicos.xml");
                var dic = Directory.GetFiles(AppContext.BaseDirectory, "Hunter.*.xml");

                //TODO:
                //encapsular para método a chamada da config do swagger
                services.AddSwaggerGen(c =>
                {

                    c.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Title = "Hunter Investimentos",
                            Version = "v1",
                            Description = "APIs Hunter Investimentos",
                            Contact = new OpenApiContact
                            {
                                Name = "IT Solution",
                                Url = new Uri("https://www.itsolutioninformatica.com.br")
                            }
                        });
                    //c.IncludeXmlComments(caminhoXmlDoc);

                    foreach (var f in dic)
                    {
                        c.IncludeXmlComments(f);
                    }
                });

                services.AddCors(options =>
                    {
                        options.AddPolicy(name: HunterPolicy,
                            builder =>
                            {
                                builder.WithOrigins("*");
                                builder.WithHeaders(HeaderNames.ContentType, "x-custom-header");
                                builder.WithMethods("PUT", "DELETE", "GET");
                            });

                        //options.AddDefaultPolicy(policy => 
                        //{ 

                        //    policy.AllowAnyOrigin();
                        //    policy.AllowAnyMethod();
                        //    policy.AllowAnyHeader();
                        //});
                    });

                //react
                services.ConfigureReact();

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
                app.UseDatabaseErrorPage();
                //app.UseBrowserLink();

            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/Uploads"
            });

            app.UseCors(HunterPolicy);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //hangfire jobs
            app.UseHangfireDashboard("/jobs", new DashboardOptions() { Authorization = new[] { new AuthorizationFilter() } });//
            app.UseHangfireServer();

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Hunter Investimentos");

            });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapControllers();

            });

            app.UseReact(env);

        }
    }
}