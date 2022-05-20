using AEMS.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
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
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT" // Optional
                    };
                    var securityRequirement = new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "bearerAuth"
                                    }
                                },
                                new string[] {}
                            }
                        };

                    c.AddSecurityDefinition("bearerAuth", securityScheme);
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
