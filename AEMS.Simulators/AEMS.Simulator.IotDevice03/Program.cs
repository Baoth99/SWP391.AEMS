using AEMS.Simulator.Core;
using System.Collections.Generic;
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
        public static string DeviceCode = "SMFPTSWP0056";
        public static string Topic = "equipment-log/";
        public static bool IsActive = true;

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

            var requestModel = new DeviceMessageRequestModel()
            {
                DeviceId = DeviceCode,
                CreatedAt = DateTimeCountry.DateTimeNow(),
                EventTime = DateTimeCountry.DateTimeNow(),
                EventType = 1,
                Message = $"{DeviceCode} is runing very well",
                Status = DeviceStatus.Standby,
                StatusMessage = DeviceStatusMesage.Standby,
                BatteryLevel = 48,
                Energy = (float)71.89,
                Power = (float)90.91,
                Voltage = (float)122.32,
                Temperature = (float)51.0,

                Geolocation = new Location()
                {
                    Latitude = (decimal)10.761555374953158,
                    Longitude = (decimal)106.65633435724078,
                    Altitude = (decimal)12.4
                },
                Magnetometer = new Coordinate()
                {
                    x = (decimal)41.12,
                    y = (decimal) -142.2,
                    z = (decimal)-44.21
                },
                MeterType = "3S",
                Version = 1.0F,
            };


            MqttClient.MqttPublishMessage<DeviceMessageRequestModel>(requestModel, Topic);
            System.Console.WriteLine("Done");
        }
    }
}
