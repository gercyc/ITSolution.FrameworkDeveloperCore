using ITSolution.Framework.Common.Abstractions.Identity.DataAccess;
using ITSolution.Framework.Common.BaseClasses.EnvironmentConfig;
using ITSolution.Framework.Common.BaseClasses.Identity;
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
        /// <param name="useDefaultUI">Utilizar o UI padrão do EF</param>
        public static IServiceCollection AddIdentity(this IServiceCollection services, bool useDefaultUI)
        {
            services.AddDbContext<ItsIdentityContext>(options =>
            {
                if (EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
                    options.UseSqlServer(EnvironmentInformation.ConnectionString);
                else if (EnvironmentInformation.DatabaseType == DatabaseType.SQLITE)
                    options.UseSqlite(EnvironmentInformation.ConnectionString);
            });

            //configurando o uso do ASP.NET Identity
            //ja adiciona o padrao do authentication
            //options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            //options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            //options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            IdentityBuilder identityBuilder = services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
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

                if(useDefaultUI)
                    identityBuilder.AddDefaultUI();

            return services;
        }
    }
}
