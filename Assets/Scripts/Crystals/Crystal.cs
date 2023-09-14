#nullable enable

using UnityEngine;

namespace HamletTwoSacks.Crystals
{
    public sealed class Crystal : MonoBehaviour
    {
        private GameObject? _owner;

        [SerializeField]
        private Rigidbody2D _rigidbody = null!;

        [SerializeField]
        private Collider2D _collider = null!;

        public bool HasOwner => _owner != null;

        public void TurnPhysicsOn()
        {
            _rigidbody.simulated = true;
            _collider.enabled = true;
        }

        public void TurnPhysicsOff()
        {
            _rigidbody.simulated = false;
            _collider.enabled = false;
        }

        public void SetOwner(GameObject owner)
        {
            if (_owner != null
                && _owner != owner)
            {
                Debug.LogError($"{owner.name} is trying to get ownership over {nameof(Crystal)} but it already has {_owner.name} as owner.");
                return;
            }

            _owner = owner;
        }

        public void ReleaseOwnership(GameObject owner)
        {
            if (_owner != null
                && _owner != owner)
            {
                Debug.LogError($"{owner.name} is trying to release ownership over {nameof(Crystal)} but it has {_owner.name} as owner.");
                return;
            }

            _owner = null;
        }
    }
}