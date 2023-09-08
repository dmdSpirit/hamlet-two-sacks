#nullable enable

using UnityEngine;

namespace HamletTwoSacks.Crystals
{
    public sealed class Crystal : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody = null!;

        public void TurnPhysicsOn()
            => _rigidbody.simulated = true;

        public void TurnPhysicsOff()
            => _rigidbody.simulated = true;
    }
}