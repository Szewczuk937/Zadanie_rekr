using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class MqttClientWrapper
    {
        private MqttClient mqttClient;

        public MqttClientWrapper(string brokerAddress)
        {
            mqttClient = new MqttClient(brokerAddress);
            mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
        }

        public event Action<string, string> MessageReceived;

        public void Connect()
        {
            mqttClient.Connect(Guid.NewGuid().ToString());
        }

        public void Disconnect()
        {
            mqttClient.Disconnect();
        }

        public void Publish(MqttMessage mqttMessage)
        {
            if (mqttClient.IsConnected)
            {
                mqttClient.Publish(mqttMessage.Topic, Encoding.UTF8.GetBytes(mqttMessage.Message), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
            }
            else
            { 
                Console.WriteLine("Błąd: Klient MQTT nie jest połączony z brokerem MQTT.");
            }
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string topic = e.Topic;
            string message = Encoding.UTF8.GetString(e.Message);
            MessageReceived?.Invoke(topic, message);
        }
    }

}
