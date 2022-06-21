using System;
using System.Collections.Generic;
using System.Linq;
using uPLibrary.Networking.M2Mqtt;

namespace AEMS.Simulator.Core
{
    public class AppConfiguration
    {
        public static string IotEndpoint { get; set; }

        public static MqttClient MqttClient { get; set; }
        public static string Password  { get; set; }
        public static string DeviceCode { get; set; }
        public static string Topic { get; set; }
        public static string TelemetrySettingDeviceTopic { get; set; }

        public static bool IsActive { get; set; } = true;
        public static bool IsPublished { get; set; } = true;


        public static void SetAppConfiguration(string iotEndpoint, MqttClient mqttClient, string password, 
                                                string deviceCode, string topic, string telemetrySettingDeviceTopic)
        {
            IotEndpoint = iotEndpoint;
            MqttClient = mqttClient;
            Password = password;
            DeviceCode = deviceCode;
            Topic = topic;
            TelemetrySettingDeviceTopic = telemetrySettingDeviceTopic;
        }
    }
}
