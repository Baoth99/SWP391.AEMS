﻿using System;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace AEMS.Simulator.Core
{
    public class MqttSetUpConnection
    {
        public static void SetUp() 
        {
            var MqttClient = AppConfiguration.MqttClient;
            MqttClient.ConnectToMQTTServer(AppConfiguration.DeviceCode);

            MqttClient.MqttMsgSubscribed += MQTTIotClient.MqttSubscribe;
            MqttClient.MqttMsgPublishReceived += MQTTIotClient.MqttMsgPublishReceived;
            MqttClient.Subscribe(new string[] { AppConfiguration.TelemetrySettingDeviceTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        }


        public static async Task ExecuteAsync()
        {
            //DeviceInfoUtil.GetBatteryLevel();
            while (true)
            {
                if (AppConfiguration.IsPublished)
                {
                    var requestModel = DeviceMessageRequestModel.CreateRequestModel(AppConfiguration.DeviceCode);

                    AppConfiguration.MqttClient.MqttPublishMessage<DeviceMessageRequestModel>(requestModel, AppConfiguration.Topic);

                    await Task.Delay(3000);
                }
            }
        }
    }
}
