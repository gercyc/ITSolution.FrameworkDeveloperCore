using ITSolution.Framework.Common.Abstractions.Identity.DataAccess;
using ITSolution.Framework.Core.Common.BaseClasses.EnvironmentConfig;
using ITSolution.Framework.Core.Common.BaseClasses.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITSolution.Framework.Common.Abstractions.Identity
{
    public static class SetupIdentityDatabase
    {
        /// <summary>
        /// Configura o ASP.NET Identity
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddDbContext<ItsIdentityContext>(options =>
            {
                if (EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
                    options.UseSqlServer(EnvironmentInformation.ConnectionString);
                else if (EnvironmentInformation.DatabaseType == DatabaseType.SQLITE)
                    options.UseSqlite(EnvironmentInformation.ConnectionString);
            });

            //configurando o uso do ASP.NET Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    //configura as opcoes de senha. Devido a debug a segurança foi reduzida
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = false;

                })
                .AddEntityFrameworkStores<ItsIdentityContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
