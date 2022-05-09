using ITSolution.Framework.Common.Abstractions.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StartupBase = ITSolution.Framework.Core.Server.BaseClasses.StartupBase;

namespace ITSolution.Framework.Core.Host
{
    public class Startup : StartupBase
    {
        IServiceCollection _serviceDescriptors;

        protected sealed override IServiceCollection ServiceDescriptors
        {
            get => _serviceDescriptors ??= new ServiceCollection();
            set => _serviceDescriptors = value;
        }

        public Startup(IConfiguration configuration) : base(configuration)
        {
            ServiceDescriptors.Add(new ServiceDescriptor(typeof(ItsDbContextOptions), typeof(ItsDbContextOptions), ServiceLifetime.Scoped));
        }
    }
}