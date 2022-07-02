using AEMS.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AEMS.WebApi.SystemConfigurations
{
    internal static class SwaggerGenSetUp
    {
        public static void AddSwaggerGenSetUp(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentException(nameof(services));
            }

            if (AppSettingValues.UseSwaggerUI)
            {
                services.AddSwaggerGen(c =>
                {
                    foreach (var item in SwaggerDefinition.Swagger)
                    {
                        c.SwaggerDoc(item.Version, new OpenApiInfo
                        {
                            Title = item.Title,
                            Description = item.Description,
                            Version = item.Version,
                            Contact = item.Contact,
                            License = item.License,
                            TermsOfService = item.TermsOfService
                        });
                    }

                    c.ResolveConflictingActions(a => a.First());
                    c.OperationFilter<VersionFromParameter>();
                    c.DocumentFilter<VersionWithExactValueInPath>();

                    var securityScheme = new OpenApiSecurityScheme()
                    {
                        Type = SecuritySchemeType.OAuth2,
                        //Description = "Authorization ",
                        Name = "Aad Authorization",
                        In = ParameterLocation.Header,  
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT",
                        Flows = new OpenApiOAuthFlows()
                        {
                            Implicit = new OpenApiOAuthFlow()
                            {
                                AuthorizationUrl = new Uri($"{AppSettingValues.AadInstance}{AppSettingValues.AadTenantId}/oauth2/v2.0/authorize"),
                                TokenUrl = new Uri($"{AppSettingValues.AadInstance}{AppSettingValues.AadTenantId}/oauth2/v2.0/token"),
                                Scopes = new Dictionary<string, string> {
                                    {$"api://{AppSettingValues.AadClientId}/UserAccess", "UserAccess" }
                                }
                            }
                        }
                    };
                    var securityRequirement = new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "oauth2"
                                    },
                                    Scheme = "oauth2",
                                    Name = "FU.SWP391.AEMSToken",
                                    In = ParameterLocation.Header
                                },
                                new string[] { $"api://{AppSettingValues.AadClientId}/UserAcess"}
                            }
                        };
                    //https://localhost:44356/swagger/oauth2-redirect.html
                    c.AddSecurityDefinition("oauth2", securityScheme);
                    c.AddSecurityRequirement(securityRequirement);
                });
            }
        }

        public static void UseSwaggerGenSetUp(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentException(nameof(app));
            }

            if (AppSettingValues.UseSwaggerUI)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    foreach (var item in SwaggerDefinition.Swagger)
                    {
                        c.SwaggerEndpoint(item.UrlDefination, $"{item.Title} {item.Version}");
                    }
                    c.OAuthClientId(AppSettingValues.AadClientId);
                    c.OAuthClientSecret(AppSettingValues.AadClientSecret);
                    //c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                    c.OAuthScopeSeparator(" ");
                });
            }
        }
    }

    internal class VersionFromParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters.Count > 0)
            {
                var versionParameter = operation.Parameters.Single(p => p.Name == "ver");
                operation.Parameters.Remove(versionParameter);
            }
        }
    }

    internal class VersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths;
            swaggerDoc.Paths = new OpenApiPaths();
            foreach (var path in paths)
            {
                var key = path.Key.Replace("v{ver}", swaggerDoc.Info.Version);
                var value = path.Value;
                swaggerDoc.Paths.Add(key, value);
            }
        }
    }
}
