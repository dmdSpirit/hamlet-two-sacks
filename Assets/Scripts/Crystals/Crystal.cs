#nullable enable

using UnityEngine;

namespace HamletTwoSacks.Crystals
{
    public sealed class Crystal : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody = null!;

        [SerializeField]
        private Collider2D _collider = null!;

        public void TurnPhysicsOn()
            => _rigidbody.simulated = true;

        public void TurnPhysicsOff()
        {
            _rigidbody.simulated = true;
            _collider.enabled = false;
        }
    }
}