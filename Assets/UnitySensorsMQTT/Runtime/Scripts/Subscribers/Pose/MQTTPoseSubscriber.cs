using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnitySensors.MQTT.Deserializer.Pose;

namespace UnitySensors.MQTT.Subscriber
{
    public class MQTTPoseSubscriber : MQTTSubscriber<MQTTPoseDeserializer, Pose>
    {
    }
}
