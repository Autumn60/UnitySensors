using System;
using UnityEngine;

using MQTTnet;
using MQTTnet.Server;

using UnitySensors.Attribute;

namespace UnitySensors.MQTT.Server
{
    public class MQTTLocalServer : MonoBehaviour
    {
        private IMqttServer _server;
        private IMqttServerOptions _options;

        [SerializeField, ReadOnly]
        private bool _isRunning;
        [SerializeField, ReadOnly]
        private int _numOfClients;

        private void Awake()
        {
            _server = new MqttFactory().CreateMqttServer();
            _options = new MqttServerOptionsBuilder().Build();
        }

        private async void Start()
        {
            _isRunning = false;
            _numOfClients = 0;

            _server.StartedHandler = new MqttServerStartedHandlerDelegate(OnStarted);
            _server.StoppedHandler = new MqttServerStoppedHandlerDelegate(OnStopped);
            _server.ClientConnectedHandler = new MqttServerClientConnectedHandlerDelegate(OnClientConnected);
            _server.ClientDisconnectedHandler = new MqttServerClientDisconnectedHandlerDelegate(OnClientDisconnected);
            try
            {
                await _server.StartAsync(_options);
            }
            catch
            {

            }
        }

        private void OnStarted(EventArgs args)
        {
            _isRunning = true;
        }

        private void OnStopped(EventArgs args)
        {
            _isRunning = false;
        }

        private void OnClientConnected(MqttServerClientConnectedEventArgs args)
        {
            _numOfClients++;
        }

        private void OnClientDisconnected(MqttServerClientDisconnectedEventArgs args)
        {
            _numOfClients--;
        }

        private async void Stop()
        {
            try
            {
                await _server.StopAsync();
            }
            catch
            {

            }
        }

        private void OnDestroy()
        {
            Stop();
            _server = null;
        }
    }
}