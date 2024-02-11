using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnitySensors.Sensor;
using UnitySensors.MQTT.Message;
using UnitySensors.MQTT.Client;
using UnitySensors.MQTT.Serializer;

namespace UnitySensors.MQTT.Publisher
{
    public abstract class MQTTPublisher<T, TT> : MonoBehaviour where T : UnitySensor where TT : MQTTSerializer<T>
    {
        [SerializeField]
        private MQTTClient _client;

        [SerializeField]
        private float _frequency = 10.0f;

        [SerializeField]
        private string _tag;

        [SerializeField]
        protected TT _serializer;

        private float _time;
        private float _dt;

        private float _frequency_inv;

        protected virtual void Start()
        {
            _dt = 0.0f;
            _frequency_inv = 1.0f / _frequency;

            _serializer.Init(GetComponent<T>());
        }

        protected virtual void Update()
        {
            _dt += Time.deltaTime;
            if (_dt < _frequency_inv) return;

            PayloadWithTag payloadWithTag = new PayloadWithTag()
            {
                tag = _tag,
                payload = _serializer.Serialize()
            };

            _client.Publish(payloadWithTag);

            _dt -= _frequency_inv;
        }
    }
}
