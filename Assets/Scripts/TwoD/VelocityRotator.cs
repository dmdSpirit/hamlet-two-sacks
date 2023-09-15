#nullable enable

using UnityEngine;

namespace HamletTwoSacks.TwoD
{
    public sealed class VelocityRotator : MonoBehaviour
    {
        [SerializeField]
        public Transform _target = null!;

        [SerializeField]
        public float _tilt = 0.1f;

        public void UpdateRotation(float velocity)
        {
            Vector3 rotation = _target.localRotation.eulerAngles;
            var direction = (int)Mathf.Sign(velocity);
            if (velocity != 0)
            {
                int currentDirection = rotation.y == 0 ? 1 : -1;
                if (currentDirection != direction)
                    rotation.y = direction == 1 ? 0 : 180f;
            }

            rotation.z = velocity == 0 ? 0 : _tilt;
            _target.localRotation = Quaternion.Euler(rotation);
        }
    }
}