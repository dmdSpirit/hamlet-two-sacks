#nullable enable

using JetBrains.Annotations;
using UniRx;

namespace HamletTwoSacks.Character
{
    [UsedImplicitly]
    public sealed class Player
    {
        private readonly ReactiveProperty<int> _crystals = new();

        public IReadOnlyReactiveProperty<int> Crystals => _crystals;

        public void AddCrystal()
            => _crystals.Value++;

        public void SpendCrystal()
        {
            if (_crystals.Value <= 0)
                return;
            _crystals.Value--;
        }
    }
}