using UnityEngine;

using UnitySensors.Sensor.GroundTruth;
using UnitySensors.MQTT.Serializer.Pose;

namespace UnitySensors.MQTT.Publisher
{
    [RequireComponent(typeof(GroundTruth))]
    public class MQTTGroundTruthPublisher : MQTTPublisher<GroundTruth, MQTTPoseSerializer<GroundTruth>>
    {
    }
}
