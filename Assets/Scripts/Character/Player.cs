#nullable enable

using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Character
{
    public sealed class Player
    {
        private readonly ReactiveProperty<int> _crystals = new();

        public IReadOnlyReactiveProperty<int> Crystals => _crystals;

        public void AddCrystal()
        {
            _crystals.Value++;
            Debug.Log($"Crystal gained.");
            Debug.Log($"total crystals: {_crystals.Value}");
        }

        public void SpendCrystal()
        {
            if (_crystals.Value <= 0)
                return;
            _crystals.Value--;
            Debug.Log($"Crystal spent.");
            Debug.Log($"total crystals: {_crystals.Value}");
        }
    }
}