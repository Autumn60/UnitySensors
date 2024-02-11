using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitySensors.MQTT.Deserializer
{
    public abstract class MQTTDeserializer<T>
    {
        public abstract T Deserialize(string payload);
    }
}
