namespace AEMS.Utilities
{
    public class AppSettingKeys
    {
        public static class ConnectionString
        {
            public const string SQLConnectionString = "ConnectionStrings:SQLConnectionString";
        }

        public static class AzureKeyVault
        {
            public const string KeyVaultConnectionUrl = "AzKeyVault:ConnectionUri";
            // Azure
            public const string AadDomain = "AadDomain";
            public const string AadClientId = "AadClientId";
            public const string AadTenantId = "AadTenantId";
            public const string AadClientSecret = "AadClientSecret";
            // AWS
            public const string AWSAccessKey = "AWSAccessKey";
            public const string AWSSecretKey = "AWSSecretKey";
        }

        public static class AzureAd
        {
            public const string Instance = "AzureAd:Instance";
        }

        public static class AWS
        {
            public const string Region = "AWS:Region";
            public const string S3BucketName = "AWS:S3BucketName";
            public const string DurationPreSignedUrl = "AWS:DurationPreSignedUrl";
        }

        public static class SystemConfig
        {
            public const string CommandTimeOut = "SystemConfig:CommandTimeout";
            public const string ReadScaleOut = "SystemConfig:ReadScaleOut";
            public const string UseSwaggerUI = "SystemConfig:UseSwaggerUI";
            public const string TimeZoneInfo = "SystemConfig:TimeZoneInfo";
        }

        public static class SwaggerDocApiInfo
        {
            public const string Title = "SwaggerDocApiInfo:Title";
            public const string TermsOfService = "SwaggerDocApiInfo:TermsOfService";
            public const string ContactEmail = "SwaggerDocApiInfo:ContactEmail";
            public const string ContactName = "SwaggerDocApiInfo:ContactName";
            public const string LicenseName = "SwaggerDocApiInfo:LicenseName";
            public const string LicenseUrl = "SwaggerDocApiInfo:LicenseUrl";
            public const string UrlDefination = "SwaggerDocApiInfo:UrlDefination";
        }
    }
}
