using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;

namespace AEMS.Simulator.Core
{
    public static class MQTTClientExtension
    {
        public static void ConnectToMQTTServer(this MqttClient client, string deviceCode)
        {
            try
            {
                client.Connect(deviceCode);
                Console.WriteLine($"Connected to AWS IoT Core with device code (client id): {deviceCode}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connecting to AWS IoT Core has errors with message {ex.Message}");
            }

        }
        public static void MqttPublishMessage<T>(this MqttClient client, T model, string topic)
        {
            Console.WriteLine("AWS IoT Dotnet core message publisher starting");
            client.Publish(topic, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model)));
            Console.WriteLine(JsonConvert.SerializeObject(model));
            Console.WriteLine($"Successfully published message!");
        }

        public static void MqttPublishMutipleMessage<T>(this MqttClient client, List<T> models, string topic, int sleep = 3000)
        {
            Console.WriteLine("AWS IoT Dotnet core message publisher starting");
            int i = 0;
            foreach (T model in models)
            {
                client.Publish(topic, Encoding.UTF8.GetBytes($"{JsonConvert.SerializeObject(model)}"));
                Console.WriteLine($"Successfully published message {i} !");
                Thread.Sleep(sleep);
                i++;
            }
            Console.WriteLine("Publish mutiple messages done !");
        }
    }
}
