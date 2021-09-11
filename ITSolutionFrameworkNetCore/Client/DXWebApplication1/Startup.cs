using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Hosting;
using ITSolution.Framework.Core.Server.BaseClasses.Repository;

namespace ITSolution.Framework.Client.App
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
            ServiceDescriptors.Add(new ServiceDescriptor(typeof(ItsDbContextOptions), typeof(ItsDbContextOptions), ServiceLifetime.Singleton));
        }
    }
}
