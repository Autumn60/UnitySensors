using System;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Receiving;
using MQTTnet.Client.Disconnecting;

using UnitySensors.Attribute;
using UnitySensors.MQTT.Message;

namespace UnitySensors.MQTT.Client
{
    public class MQTTClient : MonoBehaviour
    {
        [SerializeField]
        private string _ip = "localhost";

        [SerializeField]
        private string _topic = "";

        [SerializeField, ReadOnly]
        private bool _isConnected;

        IMqttClient _client;
        IMqttClientOptions _options;

        public delegate void OnSubscribed(PayloadWithTag payloadWithTag);
        public OnSubscribed onSubscribed;

        private void Awake()
        {
            _client = new MqttFactory().CreateMqttClient();
            _options = new MqttClientOptionsBuilder().WithTcpServer(_ip, 1883).Build();

            _isConnected = false;
        }

        private async void Start()
        {
            _client.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnConnected);
            _client.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(OnAppMessage);
            _client.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnDisconnected);
            
            try
            {
                await _client.ConnectAsync(_options);
            }
            catch
            {

            }
        }

        private async void OnConnected(MqttClientConnectedEventArgs args)
        {
            _isConnected = true;
            TopicFilterBuilder builder = new TopicFilterBuilder();
            await _client.SubscribeAsync(builder.WithTopic(_topic).Build());
        }

        private void OnAppMessage(MqttApplicationMessageReceivedEventArgs e)
        {
            PayloadWithTag payloadWithTag = new PayloadWithTag(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
            if(onSubscribed != null)
                onSubscribed.Invoke(payloadWithTag);
        }

        public async void Publish(PayloadWithTag payload)
        {
            if (!_isConnected) return;
            MqttApplicationMessage message = new MqttApplicationMessageBuilder().WithTopic(_topic).WithPayload(payload.ToString()).Build();
            
            try
            {
                await _client.PublishAsync(message);
            }
            catch
            {
            
            }
        }

        private async void OnDisconnected(MqttClientDisconnectedEventArgs args)
        {
            _isConnected = false;
            if (_client == null) return;

            await Task.Delay(TimeSpan.FromSeconds(5));

            try
            {
                await _client.ConnectAsync(_options);
            }
            catch
            {

            }
        }

        private void OnDestroy()
        {
            _client.Dispose();
            _client = null;
        }
    }
}
