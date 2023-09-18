namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string brokerAddress = "broker.hivemq.com";
            MqttClientWrapper mqttClient = new MqttClientWrapper(brokerAddress);
            mqttClient.Connect();

            MqttMessage message = new MqttMessage
            {
                Topic = "topic",
                Message = "Hello, MQTT! \n My nawe Dmytro Shevchuk"
            };

            mqttClient.Publish(message);

            mqttClient.Disconnect();
        }
    }
}