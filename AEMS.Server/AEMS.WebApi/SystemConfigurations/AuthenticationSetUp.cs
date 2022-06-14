using AEMS.Utilities;
using AEMS.WebApi.AuthenticationFilter;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using System;
using System.Threading.Tasks;

namespace AEMS.WebApi.SystemConfigurations
{
    internal static class AuthenticationSetUp
    {
        public static void AddAuthenticationSetUp(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentException(nameof(services));
            }
            services.AddScoped<ApiAuthenticateFilterAttribute>();

            //Adds Microsoft Identity platform(Azure AD) support to protect this Api
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(options =>
                    {
                        options.TokenValidationParameters.NameClaimType = "name";
                        options.TokenValidationParameters.ValidateAudience = false;
                        options.Events = new JwtBearerEvents()
                        {
                            OnAuthenticationFailed = (context) =>
                            {
                                var requestTelemetry = context.HttpContext.Features.Get<RequestTelemetry>();
                                if (requestTelemetry != null)
                                {
                                    var properties = requestTelemetry.Properties;
                                    properties["Authentication"] = "Logging Fail";
                                }
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = (context) =>
                            {
                                var requestTelemetry = context.HttpContext.Features.Get<RequestTelemetry>();
                                if (requestTelemetry != null)
                                {
                                    var properties = requestTelemetry.Properties;
                                    properties["Authentication"] = "Logging Passs";
                                }
                                return Task.CompletedTask;
                            },
                        };
                    },
                    options =>
                    {
                        options.Instance = AppSettingValues.AadInstance;
                        options.Domain = AppSettingValues.AadDomain;
                        options.ClientId = AppSettingValues.AadClientId;
                        options.TenantId = AppSettingValues.AadTenantId;
                        options.ClientSecret = AppSettingValues.AadClientSecret;
                    });
        }
    }
}
