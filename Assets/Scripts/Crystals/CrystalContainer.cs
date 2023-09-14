#nullable enable

using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalContainer : MonoBehaviour
    {
        private readonly ReactiveProperty<int> _crystals = new();
        private readonly ReactiveProperty<int> _capacity = new();

        public IReadOnlyReactiveProperty<int> Capacity => _capacity;
        public IReadOnlyReactiveProperty<int> Crystals => _crystals;
        public bool IsFull => _crystals.Value == Capacity.Value;

        public void SetCapacity(int capacity)
        {
            _capacity.Value = capacity;
            if (_crystals.Value <= _capacity.Value)
                return;
            _crystals.Value = _capacity.Value;
        }

        public void AddCrystal()
        {
            Assert.IsFalse(_crystals.Value + 1 > _capacity.Value);
            _crystals.Value++;
        }

        public bool TryGetCrystal()
        {
            if (_crystals.Value == 0)
                return false;
            _crystals.Value--;
            return true;
        }
    }
}