using System.Security.Cryptography.X509Certificates;
using uPLibrary.Networking.M2Mqtt;

namespace AEMS.Simulator.Core
{
    public class MQTTConnectionRequestModel
    {
        public X509Certificate CaCert { get; set; }

        public X509Certificate2 ClientCert { get; set; }

        public string IotEndpoint { get; set; }

        public int BrokerPort { get; set; } = 8883;

        public MqttSslProtocols SslProtocol { get; set; } = MqttSslProtocols.TLSv1_2;
    }

    public class MQTTConnectiton
    {
        public static MqttClient GetMqttClient(MQTTConnectionRequestModel model)
        {
            var client = new MqttClient(model.IotEndpoint, model.BrokerPort, true, model.CaCert, model.ClientCert, model.SslProtocol);
            return client;
        }
    }
}
