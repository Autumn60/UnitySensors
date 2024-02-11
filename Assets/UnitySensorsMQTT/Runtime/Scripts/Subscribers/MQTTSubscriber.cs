using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnitySensors.Attribute;
using UnitySensors.MQTT.Message;
using UnitySensors.MQTT.Client;
using UnitySensors.MQTT.Deserializer;

namespace UnitySensors.MQTT.Subscriber
{
    public abstract class MQTTSubscriber<T, TT> : MonoBehaviour where T : MQTTDeserializer<TT>, new()
    {
        [SerializeField]
        private MQTTClient _client;
        [SerializeField]
        protected string _tag;

        [SerializeField, ReadOnly]
        private string _subscribedPayload;

        private T _deserializer;

        public delegate void OnSubscribed(TT value);
        public OnSubscribed onSubscribed;

        private void Start()
        {
            _client.onSubscribed += OnSubscribedRaw;
            _deserializer = new T();
        }

        private void OnSubscribedRaw(PayloadWithTag payloadWithTag)
        {
            if (payloadWithTag.tag != _tag) return;
            _subscribedPayload = payloadWithTag.payload;
            if (onSubscribed != null)
                onSubscribed.Invoke(_deserializer.Deserialize(payloadWithTag.payload));
        }
    }
}
