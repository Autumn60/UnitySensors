using UnityEngine;

namespace UnitySensors.MQTT.Message
{
    public struct PayloadWithTag
    {
        public string tag;
        public string payload;

        public PayloadWithTag(string json)
        {
            this = JsonUtility.FromJson<PayloadWithTag>(json);
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
