using UnityEngine;
using UnitySensors.Attribute;

namespace UnitySensors.Sensor.IMU
{
    public class IMUSensor : UnitySensor
    {
        private Transform _transform;

        [SerializeField, ReadOnly]
        private Vector3 _position;
        [SerializeField, ReadOnly]
        private Vector3 _velocity;
        [SerializeField, ReadOnly]
        private Vector3 _acceleration;
        [SerializeField, ReadOnly]
        private Quaternion _rotation;
        [SerializeField, ReadOnly]
        private Vector3 _angularVelocity;

        private Vector3 _position_tmp;
        private Vector3 _velocity_tmp;
        private Vector3 _acceleration_tmp;
        private Quaternion _rotation_tmp;
        private Vector3 _angularVelocity_tmp;

        private Vector3 _position_last;
        private Vector3 _velocity_last;
        private Quaternion _rotation_last;

        public Vector3 position { get => _position; }
        public Vector3 velocity { get => _velocity; }
        public Vector3 acceleration { get => _acceleration; }
        public Quaternion rotation { get => _rotation; }
        public Vector3 angularVelocity { get => _angularVelocity; }

        public Vector3 localVelocity { get => _transform.InverseTransformDirection(_velocity); }
        public Vector3 localAcceleration { get => _transform.InverseTransformDirection(_acceleration.normalized) * _acceleration.magnitude; }

        private Vector3 _gravity;
        private Quaternion _rotation_init;

        protected override void Init()
        {
            _transform = this.transform;
            _gravity = Physics.gravity;
            _rotation_init = _transform.rotation;
        }

        private void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;
            _position_tmp = transform.position;
            _velocity_tmp = (_position_tmp - _position_last) / dt;
            _acceleration_tmp = (_velocity_tmp - _velocity_last) / dt + _gravity;

            _rotation_tmp = _transform.rotation;
            Quaternion rotation_delta = Quaternion.Inverse(_rotation_last) * _rotation_tmp;
            rotation_delta.ToAngleAxis(out float angle_delta, out Vector3 axis);
            float angularSpeed = (angle_delta * Mathf.Deg2Rad) / dt;
            _angularVelocity_tmp = axis * angularSpeed;

            _position_last = _position_tmp;
            _velocity_last = _velocity_tmp;
            _rotation_last = _rotation_tmp;

            base.Update();
        }

        protected override void UpdateSensor()
        {
            Quaternion rotation_tmp_inv = Quaternion.Inverse(_rotation_tmp);
            _position = rotation_tmp_inv * _position_tmp;
            _velocity = rotation_tmp_inv * _velocity_tmp;
            _acceleration = rotation_tmp_inv * _acceleration_tmp;

            _rotation = Quaternion.Inverse(_rotation_init) * _rotation_tmp;
            _angularVelocity = rotation_tmp_inv * _angularVelocity_tmp;

            if (onSensorUpdated != null)
                onSensorUpdated.Invoke();
        }

        protected override void OnSensorDestroy()
        {
        }
    }
}
