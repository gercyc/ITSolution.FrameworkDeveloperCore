using System.Reflection;
using ITSolution.Framework.Core.Common.BaseClasses;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

namespace ITSolution.Framework.Common.Abstractions.Modules;

public static class ConfigureApplicationPart
{
    /// <summary>
    /// Adiciona application parts (modulos)
    /// </summary>
    /// <param name="services"></param>
    public static void AddApplicationParts(this IServiceCollection services)
    {
        IMvcBuilder mvcBuilder = services.AddMvc()
            .AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

        mvcBuilder.AddApplicationPart(Assembly.GetExecutingAssembly());


        //starting application parts
        foreach (var file in ItsAssemblyResolve.ItsLoader.GetServerAssemblies())
        {
            Assembly asm = ItsAssemblyResolve.ItsLoader.Load(file);
            try
            {
                if (asm.ExportedTypes != null)
                    mvcBuilder.AddApplicationPart(asm);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

}