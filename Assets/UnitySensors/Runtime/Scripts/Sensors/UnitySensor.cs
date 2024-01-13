using UnityEngine;

namespace UnitySensors.Sensor
{
    /// <summary>
    /// Base class for all sensor classes.
    /// </summary>
    public abstract class UnitySensor : MonoBehaviour
    {
        /// <summary>
        /// Operating frequency of the sensor.
        /// </summary>
        [SerializeField]
        [Tooltip("Operating frequency of the sensor.")]
        private float _frequency = 10.0f;

        private float _time;
        private float _dt;

        /// <summary>
        /// Callback function when sensor data is updated.
        /// </summary>
        public delegate void OnSensorUpdated();
        public OnSensorUpdated onSensorUpdated;

        private float _frequency_inv;

        public float dt { get => _frequency_inv; }
        public float time { get => _time; }

        private void Awake()
        {
            _dt = 0.0f;
            _frequency_inv = 1.0f / _frequency;

            Init();
        }

        protected virtual void Update()
        {
            _dt += Time.deltaTime;
            if (_dt < _frequency_inv) return;

            _time = Time.time;
            UpdateSensor();

            _dt -= _frequency_inv;
        }

        private void OnDestroy()
        {
            onSensorUpdated = null;
            OnSensorDestroy();
        }

        /// <summary>
        /// Sensor initialization function. Called at Awake().
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// Sensor data update function. Called based on _frequency.
        /// </summary>
        protected abstract void UpdateSensor();

        /// <summary>
        /// Called ad OnDestroy().
        /// </summary>
        protected abstract void OnSensorDestroy();
    }
}
