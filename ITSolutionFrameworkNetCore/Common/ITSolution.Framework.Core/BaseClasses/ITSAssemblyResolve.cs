using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using ITSolution.Framework.Common.BaseClasses.EnvironmentConfig;
using Microsoft.Extensions.DependencyModel;

namespace ITSolution.Framework.Common.BaseClasses
{
    public class ItsAssemblyResolve : AssemblyLoadContext
    {
        #region Singleton
        static ItsAssemblyResolve _loader;
        public static ItsAssemblyResolve ItsLoader
        {
            get
            {
                if (_loader == null)
                {
                    _loader = new ItsAssemblyResolve();
                }
                return _loader;
            }
        }
        #endregion


        protected override Assembly Load(AssemblyName assemblyName)
        {
            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFile(Path.Combine(EnvironmentInformation.CoreAssemblyFolder, assemblyName.Name + ".dll"));
                DependencyContext.Load(assembly);
                return assembly;
            }
            catch (FileNotFoundException fileNotFound)
            {
                try
                {
                    assembly = Assembly.LoadFile(Path.Combine(EnvironmentInformation.ApiAssemblyFolder,
                        assemblyName.Name + ".dll"));
                    DependencyContext.Load(assembly);
                    return assembly;
                }
                catch (FileNotFoundException)
                {
                    assembly = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        assemblyName.Name + ".dll"));
                    DependencyContext.Load(assembly);
                    return assembly;
                }
                catch (Exception exApi)
                {
                    Utils.ShowExceptionStack(exApi);
                    Utils.ShowExceptionStack(fileNotFound);
                }

            }
            catch (Exception exAsmNotFound)
            {
                Utils.ShowExceptionStack(exAsmNotFound);
            }
            return assembly;
        }

        public Assembly Load(string assemblyPath)
        {
            AssemblyName assemblyName = InternalLoad(assemblyPath).GetName();
            return this.Load(assemblyName);
        }

        /// <summary>
        /// Get server assemblies for add parts on application
        /// </summary>
        /// <returns></returns>
        public string[] GetServerAssemblies()
        {
            return InternalGetServerAssemblies();
        }

        internal Assembly InternalLoad(string assemblyPath)
        {
            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFile(assemblyPath);
                var dps = DependencyContext.Default;

                //try resolve dependencies
                if (assembly != null)
                {
                    DependencyContext.Load(assembly);
                    var refAssemblies = assembly.GetReferencedAssemblies();

                    foreach (var asm in refAssemblies.Where(n => n.FullName.Contains("ITSolution.Framework.Core")
                    || n.FullName.Contains("ITSolution.Framework.Server.Core") || n.FullName.Contains("Hunter.API")))
                    {
                        this.Load(asm);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possível carregar o assembly especificado: '{0}' \n Exception: '{1}' \n '{2}'", assemblyPath, ex.Message, ex.StackTrace);
            }

            return assembly;
        }

        private string[] InternalGetServerAssemblies()
        {
            string[] files = null;
            try
            {
                string exec = AppDomain.CurrentDomain.FriendlyName;

                files = Directory.GetFiles(EnvironmentInformation.ApiAssemblyFolder, "*.dll")
                    .Where(f => f.Contains("ITSolution.Framework.Servers.Core") || f.Contains("Hunter.API")).ToArray();

            }
            catch (Exception ex)
            {
                Utils.ShowExceptionStack(ex);
                throw;
            }
            return files;
        }
    }
}
