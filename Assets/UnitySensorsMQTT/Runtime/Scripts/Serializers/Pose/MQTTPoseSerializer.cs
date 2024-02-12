using UnityEngine;
using UnitySensors.Data.Pose;
using UnitySensors.Sensor;

namespace UnitySensors.MQTT.Serializer.Pose
{
    public class MQTTPoseSerializer<T> : MQTTSerializer<T> where T : UnitySensor, IPoseInterface
    {
        public override string Serialize()
        {
            return JsonUtility.ToJson(sensor.pose);
        }
    }
}