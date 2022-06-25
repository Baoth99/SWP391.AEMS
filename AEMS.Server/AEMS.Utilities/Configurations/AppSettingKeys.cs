namespace AEMS.Utilities
{
    public class AppSettingKeys
    {
        public static class ConnectionString
        {
            public const string SQLConnectionString = "ConnectionStrings:SQLConnectionString";
        }

        public static class ApplicationInsights
        {
            public const string ConnectionString = "ApplicationInsights:ConnectionString";
            public const string InstrumentationKey = "ApplicationInsights:InstrumentationKey";
        } 


        public static class CosmosDbConnection
        {
            public const string ConnectionString = "CosmosDbConnection:ConnectionString";
            public const string AemsDb = "CosmosDbConnection:AemsDb";
            public const string DeviceLogContainer = "CosmosDbConnection:DeviceLogContainer";
            public const string DeviceInfoContainer = "CosmosDbConnection:DeviceInfoContainer";
        }


        public static class PowerBI
        {
            public const string ApiUrl = "PowerBI:ApiUrl";
            public const string GroupId = "PowerBI:GroupId";
            public const string CustomerInfoReport = "PowerBI:CustomerInfoReport";
            public const string DeviceReport = "PowerBI:DeviceReport";
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

        public static class AzureSignalR
        {
            public const string ConnectionString = "AzureSignalR:ConnectionString";
        }

        public static class AzureAd
        {
            public const string Instance = "AzureAd:Instance";
            public const string Domain = "AzureAd:Domain";
            public const string ClientId = "AzureAd:ClientId";
            public const string TenantId = "AzureAd:TenantId";
            public const string ClientSecret = "AzureAd:ClientSecret";
        }

        public static class AWS
        {
            public const string AccessKey = "AWS:AccessKey";
            public const string SecretKey = "AWS:SecretKey";
            public const string Region = "AWS:Region";
            public const string S3BucketName = "AWS:S3BucketName";
            public const string DurationPreSignedUrl = "AWS:DurationPreSignedUrl";
            public const string ApiGatewayAccessKey = "AWS:ApiGatewayAccessKey";
            public const string ApiGatewaySecretKey = "AWS:ApiGatewaySecretKey";
            public const string ApiGatewayS3InvokeURL = "AWS:ApiGatewayS3InvokeURL";
            public const string MqttBrokerEndpoint = "AWS:MqttBrokerEndpoint";
            public const string TelemetrySettingDeviceTopic = "AWS:TelemetrySettingDeviceTopic";
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
