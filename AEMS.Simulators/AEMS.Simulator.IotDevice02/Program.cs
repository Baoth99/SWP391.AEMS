using AEMS.Simulator.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using uPLibrary.Networking.M2Mqtt;

namespace AEMS.Simulator.IotDevice02
{
    internal class Program
    {
        public static string CurrentPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
        public static string IotEndpoint = "a2dsxgxbalx8fb-ats.iot.ap-southeast-1.amazonaws.com";

        public static MqttClient MqttClient;
        public static string Password = "123";
        public static string DeviceCode = "SMFPTSWP0033";
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
                BatteryLevel = 80,
                Energy = (float)81.63,
                Power = (float)68.32,
                Voltage = (float)128.51,
                Temperature = (float)40.0,

                Geolocation = new Location()
                {
                    Latitude = (decimal)10.777786761721552,
                    Longitude = (decimal)106.62406201837359,
                    Altitude = (decimal)6.5
                },
                Magnetometer = new Coordinate()
                {
                    x = (decimal)40.21,
                    y = (decimal)-112.67,
                    z = (decimal)-40.24
                },
                MeterType = "3S",
                Version = 1.0F,
            };


            MqttClient.MqttPublishMessage<DeviceMessageRequestModel>(requestModel, Topic);
            System.Console.WriteLine("Done");
        }
    }
}
