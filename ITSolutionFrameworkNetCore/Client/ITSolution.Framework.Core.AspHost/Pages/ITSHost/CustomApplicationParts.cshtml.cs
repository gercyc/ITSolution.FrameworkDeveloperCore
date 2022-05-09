using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace ITSolution.Framework.Core.AspHost.Pages.ITSHost
{
    public class CustomApplicationPartsModel : PageModel
    {
        public string Message { get; set; }
        internal List<AppPart> LoadedParts { get; private set; }

        public void OnGet()
        {

            List<Assembly> loadedAsemblies = new List<Assembly>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("ITSolution.Framework.Servers.Core")))
            {
                try
                {
                    if (assembly.ExportedTypes.Any(e => e.BaseType == typeof(ControllerBase)
                                                                          || e.BaseType == typeof(PageModel)))
                    {
                        loadedAsemblies.Add(assembly);
                    }
                }
                catch
                {
                    // ignored
                }
            }
            //AppDomain.CurrentDomain.GetAssemblies().
            //                       Where(a => a.FullName.Contains("ITSolution.Framework.Servers.Core")
            //                       && a.ExportedTypes.Any(e => e.BaseType == typeof(ControllerBase)
            //                                                        || e.BaseType == typeof(PageModel))).ToList();

            LoadedParts = new List<AppPart>();

            foreach (var p in loadedAsemblies)
            {
                foreach (var typeInfo in p.DefinedTypes.Where(d => d.BaseType == typeof(ControllerBase)
                         || d.BaseType == typeof(PageModel)))
                {
                    AppPart part = new AppPart
                    {
                        AssemblyName = p.GetName().Name,
                        TypeName = typeInfo.FullName,
                    };


                    switch (typeInfo.BaseType.Name)
                    {
                        case nameof(ControllerBase):
                            part.ApiName = typeInfo.FullName.Split('.').LastOrDefault().ToString().Replace("Controller", "");
                            part.EndPoint = $"/api/{part.ApiName}";
                            break;
                        case nameof(PageModel):
                            if (typeInfo.CustomAttributes.Any(a => a.AttributeType == typeof(RouteAttribute)))
                                part.ApiName = typeInfo.CustomAttributes.First().ConstructorArguments.First().Value.ToString();

                            part.EndPoint = $"/{part.ApiName}";
                            break;

                    }
                    LoadedParts.Add(part);
                }
            }
        }
        internal class AppPart
        {
            public string AssemblyName { get; set; }
            public string TypeName { get; set; }
            public string ApiName { get; set; }
            public string EndPoint { get; set; }
        }
    }
}