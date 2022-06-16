using AEMS.Utilities;
using Amazon;
using Amazon.IoT;
using Amazon.IotData;
using Amazon.IotData.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEMS.AWSService
{
    public class AWSIotCoreService : IAWSIotCoreService
    {
        private readonly AmazonIoTClient AmazonIotClient;

        private readonly AmazonIotDataClient AmazonIotDataClient;

        public AWSIotCoreService()
        {
            AmazonIotClient = new AmazonIoTClient(AppSettingValues.AWSAccessKey, AppSettingValues.AWSSecretKey, RegionEndpoint.APSoutheast1);
            AmazonIotDataClient = new AmazonIotDataClient(AppSettingValues.AWSAccessKey, AppSettingValues.AWSSecretKey, AppSettingValues.AWSMqttBrokerEndpoint);
        }

        public async Task PublishMQTTMessage<T>(string topic, T model)
        {
            var message = JsonConvert.SerializeObject(model);
            var request = new PublishRequest()
            {
                Payload = new MemoryStream(Encoding.UTF8.GetBytes(message)),
                Topic = topic,
                Qos = 1
                                
            };
            await AmazonIotDataClient.PublishAsync(request);
        }
    }
}
