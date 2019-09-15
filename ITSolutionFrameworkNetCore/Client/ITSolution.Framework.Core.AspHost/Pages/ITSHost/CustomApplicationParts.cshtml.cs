using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ITSolution.Framework.Core.BaseClasses;
using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace ITSolution.Framework.Core.AspHost.Pages.ITSHost
{
    public class CustomApplicationPartsModel : PageModel
    {
        public string Message { get; set; }
        public List<string> ApplicationParts { get; private set; }
        public List<AppPart> LoadedParts { get; private set; }

        public void OnGet()
        {
            string pt = EnvironmentManager.Configuration.ConnectionConfigPath;
            List<Assembly> loadedAsemblies = AppDomain.CurrentDomain.GetAssemblies().
                                       Where(a => a.FullName.Contains("ITSolution.Framework.Servers.Core")
                                       && a.ExportedTypes.Any(e => e.BaseType == typeof(Microsoft.AspNetCore.Mvc.ControllerBase))).ToList();

            LoadedParts = new List<AppPart>();

            foreach (var p in loadedAsemblies)
            {
                foreach (var typeInfo in p.DefinedTypes.Where(d => d.BaseType == typeof(ControllerBase)))
                {
                    string typeName = typeInfo.FullName;
                    string apiName = typeName.Split('.').LastOrDefault().ToString().Replace("Controller", "");
                    LoadedParts.Add(new AppPart()
                    {
                        AssemblyName = p.GetName().Name,
                        TypeName = typeName,
                        ApiName = apiName
                    });
                }
            }
        }
        public class AppPart
        {
            public AppPart()
            {

            }
            public string AssemblyName { get; set; }
            public string TypeName { get; set; }
            public string ApiName { get; set; }
        }
    }
}