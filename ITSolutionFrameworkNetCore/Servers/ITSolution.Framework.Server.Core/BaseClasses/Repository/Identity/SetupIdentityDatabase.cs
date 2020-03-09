using ITSolution.Framework.BaseClasses;
using ITSolution.Framework.Core.BaseClasses;
using ITSolution.Framework.Core.BaseClasses.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository.Identity
{
    public static class SetupIdentityDatabase
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddDbContext<ITSIdentityContext>(options =>
            {
                if (EnvironmentInformation.DatabaseType == DatabaseType.MSSQL)
                    options.UseSqlServer(EnvironmentManager.Configuration.ConnectionString);
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

            }).AddEntityFrameworkStores<ITSIdentityContext>().AddDefaultTokenProviders();

            //configura os cookies que armazenarão os dados do usuário logado
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "UNAADSAUTH";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Identity/Account/Login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
        }
    }
}
