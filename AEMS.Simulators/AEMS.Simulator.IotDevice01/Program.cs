using AEMS.Simulator.Core;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;

namespace AEMS.Simulator.IotDevice01
{
    internal class Program
    {
        public static string CurrentPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
        public static string IotEndpoint = "a2dsxgxbalx8fb-ats.iot.ap-southeast-1.amazonaws.com";

        public static MqttClient MqttClient;
        public static string Password = "123";
        public static string DeviceCode = "SMFPTSWP0012";
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


            //Connected=0  //Standby=1 //Disconnected=2

            // Model to test
            var requestModel = new DeviceMessageRequestModel()
            {
                DeviceId = DeviceCode,
                CreatedAt = DateTimeCountry.DateTimeNow(),
                EventTime = DateTimeCountry.DateTimeNow(),
                EventType = 1,
                Message = $"{DeviceCode} is runing very well",
                Status = DeviceStatus.Standby,
                StatusMessage = DeviceStatusMesage.Standby,
                BatteryLevel = 89,
                Energy = (float)63.44,
                Power = (float)85.68,
                Voltage = (float)121.67,
                Temperature = (float)40.0,

                Geolocation = new Location()
                {
                    Latitude = (decimal)10.770345728051746,
                    Longitude = (decimal)106.63607831476031,
                    Altitude = (decimal)7.90
                },
                Magnetometer = new Coordinate()
                {
                    x = (decimal)43.44,
                    y = (decimal)-90.6,
                    z = (decimal)-28.31
                },
                MeterType = "3S",
                Version = 1.0F,
            };
            
            MqttClient.MqttPublishMessage<DeviceMessageRequestModel>(requestModel, Topic);
            System.Console.WriteLine("Done");
            // Run to check message
            //while (true)
            //{
            //    MqttClient.MqttPublishMessage(requestModel, Topic);
            //    Thread.Sleep(5000);
            //}
        }
    }
}
