using UnitySensors.Sensor;

namespace UnitySensors.MQTT.Serializer
{
    [System.Serializable]
    public abstract class MQTTSerializer<T> where T : UnitySensor
    {
        private T _sensor;
        protected T sensor { get => _sensor; }

        public virtual void Init(T sensor)
        {
            _sensor = sensor;
        }

        public abstract string Serialize();
    }
}
