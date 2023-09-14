#nullable enable

using dmdspirit.Core.CommonInterfaces;
using HamletTwoSacks.AI;
using UnityEngine;

namespace HamletTwoSacks.Characters.Drones
{
    public sealed class Drone : MonoBehaviour, IActivatable
    {
        [SerializeField]
        private CrystalContainer _crystalContainer = null!;

        [SerializeField]
        private int _crystalCapacity = 2;

        [SerializeField]
        private Brain? _brain;

        public bool IsActive { get; private set; }

        private void Awake()
            => _crystalContainer.SetCapacity(_crystalCapacity);

        public void Activate()
        {
            if (IsActive)
                return;
            if (_brain == null)
            {
                Debug.LogError($"Trying to activate drone without a brain");
                return;
            }

            if (_brain.IsActive && IsActive)
                return;
            _brain.Activate();
            IsActive = true;
        }

        public void Deactivate()
        {
            if (_brain != null)
                _brain.Deactivate();
            IsActive = false;
        }

        public void SetBrain(Brain brain, bool destroyOldBrain = true, bool reactivateBrain = false)
        {
            bool wasActive = IsActive;
            if (_brain != null)
            {
                wasActive = _brain.IsActive;
                _brain.Deactivate();
                if (destroyOldBrain)
                    Destroy(_brain);
            }

            _brain = brain;
            if (reactivateBrain && wasActive)
                _brain.Activate();
        }
    }
}