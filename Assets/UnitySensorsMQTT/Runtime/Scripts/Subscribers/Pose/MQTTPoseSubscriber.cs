using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnitySensors.MQTT.Deserializer.Pose;

namespace UnitySensors.MQTT.Subscriber.Pose
{
    public class MQTTPoseSubscriber : MQTTSubscriber<MQTTPoseDeserializer, UnityEngine.Pose>
    {
    }
}
