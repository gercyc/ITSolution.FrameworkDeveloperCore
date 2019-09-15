﻿using ITSolution.Framework.Core.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSolution.Framework.BaseClasses
{
    public static class EnvironmentInformation
    {
        public static int ServerPort { get { return EnvironmentManager.Configuration.ServerPort; } }
        public static string AssemblyRegisterServices { get { return EnvironmentManager.Configuration.AsmRegisterServices; } }
        public static string APIAssemblyFolder { get { return EnvironmentManager.Configuration.APIAssemblyFolder; } }
        public static string CoreAssemblyFolder { get { return EnvironmentManager.Configuration.CoreAssemblyFolder; } }
        public static string ConnectionString { get { return EnvironmentManager.Configuration.ConnectionString; } }
    }
}