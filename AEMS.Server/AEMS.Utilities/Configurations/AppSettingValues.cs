using System;

namespace AEMS.Utilities
{
    public class AppSettingValues
    {
        // Get configuration setting from appsettings.json
        public static string SqlConnectionString => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.ConnectionString.SQLConnectionString);

        public static bool UseSwaggerUI => ConfigurationHelper.GetConfigValue<bool>(AppSettingKeys.SystemConfig.UseSwaggerUI);

        public static bool ReadScaleOut => ConfigurationHelper.GetConfigValue<bool>(AppSettingKeys.SystemConfig.ReadScaleOut);

        public static int CommandTimeout => ConfigurationHelper.GetConfigValue<int>(AppSettingKeys.SystemConfig.CommandTimeOut);

        public static string TimeZoneInfo => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SystemConfig.TimeZoneInfo);

        public static string AadInstance => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AzureAd.Instance);

        public static string AWSRegion => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AWS.Region);

        public static string AWSS3BucketName => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AWS.S3BucketName);

        public static int AWSDurationPreSignedUrl => ConfigurationHelper.GetConfigValue<int>(AppSettingKeys.AWS.DurationPreSignedUrl);

        public static string AWSApiGatewayAccessKey => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AWS.ApiGatewayAccessKey);

        public static string AWSApiGatewaySecretKey => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AWS.ApiGatewaySecretKey);

        public static string AWSApiGatewayS3InvokeURL => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AWS.ApiGatewayS3InvokeURL);

        public static string AWSMqttBrokerEndpoint => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AWS.MqttBrokerEndpoint);

        public static string AWSTelemetrySettingDeviceTopic => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AWS.TelemetrySettingDeviceTopic);


        public static string SwaggerDocTitle => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.Title);

        public static string SwaggerDocTermsOfService => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.TermsOfService);

        public static string SwaggerDocContactEmail => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.ContactEmail);

        public static string SwaggerDocContactName => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.ContactName);

        public static string SwaggerDocLicenseName => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.LicenseName);

        public static string SwaggerDocLicenseUrl => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.LicenseUrl);

        public static string SwaggerUrlDefination(string version) => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.UrlDefination).Replace("{ver}", version);

        public static string AzureSignalRConnection => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AzureSignalR.ConnectionString);

        public static string ApplicationInsightsConnectionString => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.ApplicationInsights.ConnectionString);

        public static string ApplicationInsightsInstrumentationKey => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.ApplicationInsights.InstrumentationKey);

        public static string PowerBIApiUrl => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.PowerBI.ApiUrl);

        public static string PowerBIGroupId => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.PowerBI.GroupId);

        public static string PowerBICustomerInfoReport => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.PowerBI.CustomerInfoReport);

        public static string PowerBIDeviceReport => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.PowerBI.DeviceReport);

        public static string CosmosDbConnectionString => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.CosmosDbConnection.ConnectionString);

        public static string CosmosDbDeviceLogContainer => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.CosmosDbConnection.DeviceLogContainer);

        public static string CosmosDbDeviceInfoContainer => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.CosmosDbConnection.DeviceInfoContainer);

        public static string CosmosDbAemsDb => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.CosmosDbConnection.AemsDb);

        //Get from Azure Key Vault
        public static string AadDomain => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AzureAd.Domain);

        public static string AadClientId => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AzureAd.ClientId);

        public static string AadTenantId => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AzureAd.TenantId);

        public static string AadClientSecret => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AzureAd.ClientSecret);

        public static string AWSAccessKey => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AWS.AccessKey);

        public static string AWSSecretKey => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.AWS.SecretKey);

        //Get configuration setting from Azure Key Vault 

        //public static string AadDomain => ConfigurationHelper.GetKeyVaultSerectValue(AppSettingKeys.AzureKeyVault.AadDomain);

        //public static string AadClientId => ConfigurationHelper.GetKeyVaultSerectValue(AppSettingKeys.AzureKeyVault.AadClientId);

        //public static string AadTenantId => ConfigurationHelper.GetKeyVaultSerectValue(AppSettingKeys.AzureKeyVault.AadTenantId);

        //public static string AadClientSecret => ConfigurationHelper.GetKeyVaultSerectValue(AppSettingKeys.AzureKeyVault.AadClientSecret);

        //public static string AWSAccessKey => ConfigurationHelper.GetKeyVaultSerectValue(AppSettingKeys.AzureKeyVault.AWSAccessKey);

        //public static string AWSSecretKey => ConfigurationHelper.GetKeyVaultSerectValue(AppSettingKeys.AzureKeyVault.AWSSecretKey);

    }
}
