using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using ITSolution.Framework.Core.AspHost.Inject;
using ITSolution.Framework.Core.BaseClasses;
using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ITSolution.Framework.Core.AspHost
{
    public class Startup : ITSolution.Framework.Core.Server.BaseClasses.StartupBase
    {
        IServiceCollection serviceDescriptors;

        public override IServiceCollection ServiceDescriptors
        {
            get
            {
                if (serviceDescriptors == null)
                    serviceDescriptors = new ServiceCollection();

                return serviceDescriptors;
            }
            set
            {
                serviceDescriptors = value;
            }
        }

        public Startup(IConfiguration configuration) : base(configuration)
        {
            ServiceDescriptors.Add(new ServiceDescriptor(typeof(IMyCalc), typeof(MyCalc), ServiceLifetime.Scoped));
            ServiceDescriptors.Add(new ServiceDescriptor(typeof(ITSDbContextOptions), typeof(ITSDbContextOptions), ServiceLifetime.Scoped));
        }
    }
}
