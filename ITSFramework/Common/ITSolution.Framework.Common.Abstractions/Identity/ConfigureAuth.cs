using System.Security.Claims;
using System.Text;
using ITSolution.Framework.Core.Server.BaseClasses.Configurators;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ITSolution.Framework.Common.Abstractions.Identity;

public static class ConfigureAuth
{
    /// <summary>
    /// Configura autenticacao via jwt
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        if (string.IsNullOrEmpty(jwtSettings.Key))
            throw new InvalidOperationException("No Key defined in JwtSettings config.");

        byte[] key = Encoding.ASCII.GetBytes(jwtSettings.Key);

        //services.AddAuthorization();

        services.AddAuthentication()
        .AddJwtBearer(bearer =>
                       {
                           bearer.RequireHttpsMetadata = false;
                           bearer.SaveToken = true;
                           bearer.TokenValidationParameters = new TokenValidationParameters
                           {
                               ValidateIssuerSigningKey = false,
                               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                               ValidateIssuer = false,
                               ValidateLifetime = false,
                               ValidateAudience = false,
                               RoleClaimType = ClaimTypes.Role,
                               ClockSkew = TimeSpan.Zero
                           };
                           bearer.Events = new JwtBearerEvents
                           {
                               OnChallenge = context =>
                               {
                                   context.HandleResponse();
                                   if (!context.Response.HasStarted)
                                   {
                                       throw new HttpRequestException("Authentication Failed.");
                                   }

                                   return Task.CompletedTask;
                               },
                               OnForbidden = _ => throw new HttpRequestException("You are not authorized to access this resource."),
                               OnMessageReceived = context =>
                               {
                                   var accessToken = context.Request.Query["access_token"];

                                   if (!string.IsNullOrEmpty(accessToken) &&
                                       context.HttpContext.Request.Path.StartsWithSegments("/notifications"))
                                   {
                                       // Read the token out of the query string
                                       context.Token = accessToken;
                                   }

                                   return Task.CompletedTask;
                               }
                           };
                       });
        
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
        return services;
    }
}