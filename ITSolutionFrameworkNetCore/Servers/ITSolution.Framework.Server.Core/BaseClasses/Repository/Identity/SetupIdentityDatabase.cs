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
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository.Identity
{
    public static class SetupIdentityDatabase
    {
        /// <summary>
        /// Configura o ASP.NET Identity
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var signInConfiguration = new SignInConfiguration();
            services.AddSingleton(signInConfiguration);
            var tokenConfigurations = new TokenConfigurations();
            services.AddSingleton(tokenConfigurations);

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

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signInConfiguration.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddTransient<IEmailSender, MailSender>();

            //configura os cookies que armazenarão os dados do usuário logado
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "HUNTERINVESTIMENTOS";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Identity/Account/Login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
        }
    }
}
