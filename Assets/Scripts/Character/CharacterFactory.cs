#nullable enable

using HamletTwoSacks.Infrastructure.StaticData;
using JetBrains.Annotations;
using Zenject;

namespace HamletTwoSacks.Character
{
    [UsedImplicitly]
    public sealed class CharacterFactory : IPlayerFactory
    {
        private DiContainer _container = null!;
        private Player _playerPrefab = null!;

        [Inject]
        private void Construct(DiContainer container, StaticDataProvider staticDataProvider)
        {
            _container = container;
            _playerPrefab = staticDataProvider.GetConfig<PrefabsConfig>().GetPrefab<Player>();
        }

        public Player CreatePlayer()
            => _container.InstantiatePrefabForComponent<Player>(_playerPrefab);
    }
}