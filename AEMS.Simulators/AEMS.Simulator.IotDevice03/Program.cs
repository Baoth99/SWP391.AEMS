using AEMS.Simulator.Core;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using uPLibrary.Networking.M2Mqtt;

namespace AEMS.Simulator.IotDevice03
{
    internal class Program
    {
        public static string CurrentPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
        public static string IotEndpoint = "a2dsxgxbalx8fb-ats.iot.ap-southeast-1.amazonaws.com";

        public static MqttClient MqttClient;
        public static string Password = "123";
        public static string DeviceCode = "AEMS_DEVICE_03";
        public static string Topic = "demo";
        static void Main(string[] args)
        {
            var mqttConnectionModel = new MQTTConnectionRequestModel()
            {
                IotEndpoint = IotEndpoint,
                CaCert = X509Certificate.CreateFromCertFile(Path.Join(CurrentPath, "AmazonRootCA1.pem")),
                ClientCert = new X509Certificate2(Path.Join(CurrentPath, "Certificate.cert.pfx"), Password)

            };
            // Get MqttClient
            MqttClient = MQTTConnectiton.GetMqttClient(mqttConnectionModel);

            // Connect to AWS IOT Core
            MqttClient.ConnectToMQTTServer(DeviceCode);
            Console.WriteLine("Hello World!");
        }
    }
}
