using AEMS.Simulator.Core;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace AEMS.Simulator.IotDevice01
{
    internal class Program
    {
        static string CurrentPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
        const string IotEndpoint = "a2dsxgxbalx8fb-ats.iot.ap-southeast-1.amazonaws.com";
        const string Password = "123";
        const string DeviceCode = "SMFPTSWP0012";
        const string Topic = "equipment-log/";
        const string TelemetrySettingDeviceTopic = "/telemetry-setting-device";

        static async Task Main(string[] args)
        {
            var mqttConnectionModel = new MQTTConnectionRequestModel()
            {
                IotEndpoint = IotEndpoint,
                CaCert = X509Certificate.CreateFromCertFile(Path.Join(CurrentPath, "AmazonRootCA1.pem")),
                ClientCert = new X509Certificate2(Path.Join(CurrentPath, "Certificate.cert.pfx"), Password)

            };
            AppConfiguration.SetAppConfiguration(IotEndpoint, MQTTConnectiton.GetMqttClient(mqttConnectionModel), 
                                                Password, DeviceCode, Topic, TelemetrySettingDeviceTopic);

            MqttSetUpConnection.SetUp();

            await MqttSetUpConnection.ExecuteAsync();
        }
    }
}
