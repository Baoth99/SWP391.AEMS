using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;


namespace AEMS.Utilities
{
    public class SwaggerDefinition
    {
        public static readonly List<SwaggerInfo> Swagger = new List<SwaggerInfo>()
        {
            new SwaggerInfo()
            {
                Title = AppSettingValues.SwaggerDocTitle,
                Version = "v1",
                TermsOfService = new Uri(AppSettingValues.SwaggerDocTermsOfService),
                Contact = new OpenApiContact()
                {
                    Email = AppSettingValues.SwaggerDocContactEmail,
                    Name = AppSettingValues.SwaggerDocContactName
                },
                License = new OpenApiLicense()
                {
                    Name = AppSettingValues.SwaggerDocLicenseName,
                    Url = new Uri(AppSettingValues.SwaggerDocLicenseUrl),
                },
                Description = "AEMS.WebApi",
                UrlDefination = AppSettingValues.SwaggerUrlDefination("v1")
            }
        }; 
    }

    public class SwaggerInfo : OpenApiInfo
    {
        public string UrlDefination { get; set; }
    }


}
