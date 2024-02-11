using UnityEngine;

namespace UnitySensors.MQTT.Deserializer.Pose
{
    public class MQTTPoseDeserializer : MQTTDeserializer<UnityEngine.Pose>
    {
        public override UnityEngine.Pose Deserialize(string payload)
        {
            return JsonUtility.FromJson<UnityEngine.Pose>(payload);
        }
    }
}