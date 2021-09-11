using ITSolution.Framework.Core.BaseClasses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ITSolution.Framework.Core.Server.BaseClasses.SetupServices
{
   public static  class SetupMVC
    {
        /// <summary>
        /// Configure MVC services and resolve custom assemblies
        /// </summary>
        /// <param name="services"></param>
        /// <param name="ServiceDescriptors"></param>
        public static void ConfigureMVC(this IServiceCollection services, IServiceCollection ServiceDescriptors)
        {
            try
            {
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
                services.AddRazorPages();
                services.AddControllersWithViews()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });
            }
            catch (Exception ex)
            {
                Utils.ShowExceptionStack(ex);
                throw;
            }
        }
    }
}
