using AEMS.AWSService;
using AEMS.Data.EF.UnitOfWork;
using AEMS.MSAzureService;
using AEMS.ORM.Dapper;
using AEMS.Utilities;
using Amazon;
using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;

namespace AEMS.WebApi.SystemConfigurations
{
    internal static class DependencyInjectionSetUp
    {
        public static void AddDependencyInjectionSetUp(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentException(nameof(services));
            }

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IDapperService, DapperService>();
            services.AddScoped<IAuthSession, AuthSession>();
            
            #region MS Azure

            var confidential = ConfidentialClientApplicationBuilder.Create(AppSettingValues.AadClientId)
                                                                    .WithTenantId(AppSettingValues.AadTenantId)
                                                                    .WithClientSecret(AppSettingValues.AadClientSecret)
                                                                    .WithAuthority($"{AppSettingValues.AadInstance}{AppSettingValues.AadTenantId}/")
                                                                    .Build();
            services.AddSingleton<IConfidentialClientApplication>(confidential);

            services.AddScoped<IAadService, AadService>();
            services.AddScoped<IPowerBIService, PowerBIService>();

            #endregion

            #region Amazon Web Service

            services.AddScoped<IAWSS3Service, AWSS3Service>();
            services.AddScoped<IAWSIotCoreService, AWSIotCoreService>();

            #endregion

        }
    }
}
