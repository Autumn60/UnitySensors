using UnityEngine;
using UnitySensors.Sensor;

namespace UnitySensors.Visualization
{
    /// <summary>
    /// Base class for sensor data visualization class.
    /// </summary>
    /// <typeparam name="T"> Sensor for visualization target. </typeparam>
    public abstract class Visualizer<T> : MonoBehaviour where T : UnitySensor
    {
        private T _sensor;
        public T sensor { get => _sensor; }

        private void Start()
        {
            _sensor = GetComponent<T>();
            _sensor.onSensorUpdated += Visualize;

            Init();
        }

        /// <summary>
        /// Initialization function. Called at Start().
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// Called when sensor is updated.
        /// </summary>
        protected abstract void Visualize();
    }
}
