using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema.Generation.TypeMappers;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

namespace ITSolution.Framework.Common.Abstractions.OpenApi;

public static class SwaggerConfigurator
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services, IConfiguration config)
    {
        services.AddOpenApiDocument((document, serviceProvider) =>
        {
            document.PostProcess = doc =>
            {
                doc.Schemes.Clear();
                doc.Info.Title = "IT Solution Framework";
                doc.Info.Version = "v1";
                doc.Info.Description = "Framework IT Solution";
                doc.Info.Contact = new()
                {
                    Name = "IT Solution",
                    Url = "https://www.itsolutioninformatica.com.br",
                    Email = "admin@itsolutioninformatica.com.br"
                };
                doc.Info.License = new()
                {
                    Name = "Global",
                    Url = "https://www.itsolutioninformatica.com.br"
                };
            };

            if (config["SecuritySettings:Provider"] != null && config["SecuritySettings:Provider"].Equals("AzureAd", StringComparison.OrdinalIgnoreCase))
            {
                document.AddSecurity(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Flow = OpenApiOAuth2Flow.AccessCode,
                    Description = "OAuth2.0 Auth Code with PKCE",
                    Flows = new()
                    {
                        AuthorizationCode = new()
                        {
                            AuthorizationUrl = config["SecuritySettings:Swagger:AuthorizationUrl"],
                            TokenUrl = config["SecuritySettings:Swagger:TokenUrl"],
                            Scopes = new Dictionary<string, string>
                                {
                                    { config["SecuritySettings:Swagger:ApiScope"], "access the api" }
                                }
                        }
                    }
                });
            }
            else
            {
                document.AddSecurity("basic", new OpenApiSecurityScheme
                {
                    Description = "Input the JWT like: Bearer {your token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.ApiKey
                });
            }

            document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor());
            document.OperationProcessors.Add(new SwaggerGlobalAuthProcessor());

            document.TypeMappers.Add(new PrimitiveTypeMapper(typeof(TimeSpan), schema =>
            {
                schema.Type = NJsonSchema.JsonObjectType.String;
                schema.IsNullableRaw = true;
                schema.Pattern = @"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$";
                schema.Example = "02:00:00";
            }));

            document.OperationProcessors.Add(new SwaggerHeaderAttributeProcessor());
        });
        return services;
    }

    public static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app, IConfiguration config)
    {
        app.UseOpenApi();
        app.UseSwaggerUi3(options =>
        {
            options.DefaultModelsExpandDepth = -1;
            options.DocExpansion = "none";
            options.TagsSorter = "alpha";
            if (config["SecuritySettings:Provider"] != null && config["SecuritySettings:Provider"].Equals("AzureAd", StringComparison.OrdinalIgnoreCase))
            {
                options.OAuth2Client = new OAuth2ClientSettings
                {
                    AppName = "Full Stack Hero Api Client",
                    ClientId = config["SecuritySettings:Swagger:OpenIdClientId"],
                    UsePkceWithAuthorizationCodeGrant = true,
                    ScopeSeparator = " "
                };
                options.OAuth2Client.Scopes.Add(config["SecuritySettings:Swagger:ApiScope"]);
            }
        });

        return app;
    }
}