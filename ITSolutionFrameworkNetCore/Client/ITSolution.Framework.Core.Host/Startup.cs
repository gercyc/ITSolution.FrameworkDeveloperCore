using ITSolution.Framework.BaseClasses;
using ITSolution.Framework.Core.BaseClasses;
using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using ITSolution.Framework.Core.Server.BaseClasses.Repository;
using ITSolution.Framework.Core.Server.BaseClasses;

namespace ITSolution.Framework.Core.Host
{
    public class Startup : ITSolution.Framework.Core.Server.BaseClasses.StartupBase
    {
        IServiceCollection _serviceDescriptors;

        protected sealed override IServiceCollection ServiceDescriptors
        {
            get => _serviceDescriptors ?? (_serviceDescriptors = new ServiceCollection());
            set => _serviceDescriptors = value;
        }
        
        public Startup(IConfiguration configuration) : base(configuration)
        {
            ServiceDescriptors.Add(new ServiceDescriptor(typeof(ItsDbContextOptions), typeof(ItsDbContextOptions), ServiceLifetime.Scoped));
            
        }
    }
}