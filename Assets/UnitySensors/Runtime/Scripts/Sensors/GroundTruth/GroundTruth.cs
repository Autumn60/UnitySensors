using UnityEngine;
using UnitySensors.Data.Pose;

namespace UnitySensors.Sensor.GroundTruth
{
    public class GroundTruth : UnitySensor, IPoseInterface
    {
        private Transform _transform;

        public Pose pose { get => new Pose(_transform.position, _transform.rotation); }

        protected override void Init()
        {
            _transform = this.transform;
        }

        protected override void UpdateSensor()
        {
        }

        protected override void OnSensorDestroy()
        {
        }
    }
}
