using Newtonsoft.Json;
using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace AEMS.Simulator.Core
{
    public class MQTTIotClient
    {
        public static void MqttSubscribe(object sender, MqttMsgSubscribedEventArgs e)
        {
            Console.WriteLine($"Successfully subscribed to the AWS IoT topic.");
        }

        public static void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            var telemetryModel = JsonConvert.DeserializeObject<TelemetryDeviceSettingModel>(Encoding.UTF8.GetString(e.Message));

            if (telemetryModel.DeviceId == AppConfiguration.DeviceCode)
            {
                Console.WriteLine("Message received: " + Encoding.UTF8.GetString(e.Message));

                switch (telemetryModel.Action)
                {
                    case RemoteControlAction.AccOff:
                        AppConfiguration.IsPublished = false;
                        break;
                    case RemoteControlAction.AccOn:
                        AppConfiguration.IsPublished = true;
                        break;
                    case RemoteControlAction.BatteryReportOn:
                        // TODO:
                        break;
                    case RemoteControlAction.BatteryReportOff:
                        // TODO:
                        break;
                    case RemoteControlAction.TemperatureReportOn:
                        // TODO:
                        break;
                    case RemoteControlAction.TemperatureReportOff:
                        // TODO:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
