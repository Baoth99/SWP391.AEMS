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

        public static string SwaggerDocTitle => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.Title);

        public static string SwaggerDocTermsOfService => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.TermsOfService);

        public static string SwaggerDocContactEmail => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.ContactEmail);

        public static string SwaggerDocContactName => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.ContactName);

        public static string SwaggerDocLicenseName => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.LicenseName);

        public static string SwaggerDocLicenseUrl => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.LicenseUrl);

        public static string SwaggerUrlDefination(string version) => ConfigurationHelper.GetConfigValue<string>(AppSettingKeys.SwaggerDocApiInfo.UrlDefination).Replace("{ver}", version);


        //Get configuration setting from Azure Key Vault 

        public static string AadDomain => ConfigurationHelper.GetKeyVaultSerectValue(AppSettingKeys.AzureKeyVault.AadDomain);

        public static string AadClientId => ConfigurationHelper.GetKeyVaultSerectValue(AppSettingKeys.AzureKeyVault.AadClientId);

        public static string AadTenantId => ConfigurationHelper.GetKeyVaultSerectValue(AppSettingKeys.AzureKeyVault.AadTenantId);

        public static string AadClientSecret => ConfigurationHelper.GetKeyVaultSerectValue(AppSettingKeys.AzureKeyVault.AadClientSecret);

        public static string AWSAccessKey => ConfigurationHelper.GetKeyVaultSerectValue(AppSettingKeys.AzureKeyVault.AWSAccessKey);

        public static string AWSSecretKey => ConfigurationHelper.GetKeyVaultSerectValue(AppSettingKeys.AzureKeyVault.AWSSecretKey);

    }
}
