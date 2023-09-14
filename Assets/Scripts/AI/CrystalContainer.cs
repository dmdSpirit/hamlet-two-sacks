#nullable enable

using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HamletTwoSacks.AI
{
    public sealed class CrystalContainer : MonoBehaviour
    {
        private readonly ReactiveProperty<int> _crystals = new();

        private int _capacity;

        public IReadOnlyReactiveProperty<int> Crystals => _crystals;

        public void SetCapacity(int capacity)
        {
            _capacity = capacity;
            if (_crystals.Value <= capacity)
                return;
            _crystals.Value = capacity;
        }

        public void AddCrystal()
            => _crystals.Value++;

        public void GetCrystal()
        {
            Assert.IsTrue(_crystals.Value > 0);
            _crystals.Value--;
        }
    }
}