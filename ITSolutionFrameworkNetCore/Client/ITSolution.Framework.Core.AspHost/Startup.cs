using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITSolution.Framework.Core.AspHost
{
    public class Startup : ITSolution.Framework.Core.Server.BaseClasses.StartupBase
    {
        IServiceCollection _serviceDescriptors;

        protected sealed override IServiceCollection ServiceDescriptors
        {
            get => _serviceDescriptors ??= new ServiceCollection();
            set => _serviceDescriptors = value;
        }

        public Startup(IConfiguration configuration) : base(configuration)
        {
            //ServiceDescriptors.Add(new ServiceDescriptor(typeof(ItsDbContextOptions), typeof(ItsDbContextOptions), ServiceLifetime.Scoped));
        }
    }
}
