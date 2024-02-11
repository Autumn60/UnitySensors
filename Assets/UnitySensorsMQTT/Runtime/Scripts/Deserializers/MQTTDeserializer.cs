namespace UnitySensors.MQTT.Deserializer
{
    public abstract class MQTTDeserializer<T>
    {
        public abstract T Deserialize(string payload);
    }
}
