using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;


namespace AEMS.Utilities
{
    public class ConfigurationHelper
    {
        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is development.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is development; otherwise, <c>false</c>.
        /// </value>
        public static bool IsDevelopment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is testing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is testing; otherwise, <c>false</c>.
        /// </value>
        public static bool IsTesting { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is production.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is production; otherwise, <c>false</c>.
        /// </value>
        public static bool IsProduction { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            string textValue = Configuration[key];
            if (string.IsNullOrEmpty(textValue))
            {
                return string.Empty;
            }
            return textValue.Trim();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static T GetConfigValue<T>(string key)
        {
            string strValue = GetValue(key);
            try
            {
                return (T)Convert.ChangeType(strValue, typeof(T));
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static string GetKeyVaultSerectValue(string name)
        {
            var connectionUri = GetConfigValue<string>(AppSettingKeys.AzureKeyVault.KeyVaultConnectionUrl);
            var client = new SecretClient(vaultUri: new Uri(connectionUri), credential: new DefaultAzureCredential());
            var serect = client.GetSecret(name).Value;
            return serect.Value;
        }

        public static string GetKeyVaultKeyValue(string name)
        {
            var connectionUri = GetConfigValue<string>(AppSettingKeys.AzureKeyVault.KeyVaultConnectionUrl);
            var client = new KeyClient(vaultUri: new Uri(connectionUri), credential: new DefaultAzureCredential());
            var key = client.GetKey(name).Value;
            return key.Name;
        }
    }
}
