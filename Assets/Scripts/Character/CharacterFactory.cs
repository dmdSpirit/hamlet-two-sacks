#nullable enable

using HamletTwoSacks.Infrastructure.StaticData;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Character
{
    [UsedImplicitly]
    public sealed class CharacterFactory : IPlayerFactory
    {
        private DiContainer _container = null!;
        private PlayerBehaviour _playerBehaviourPrefab = null!;

        [Inject]
        private void Construct(DiContainer container, StaticDataProvider staticDataProvider)
        {
            _container = container;
            _playerBehaviourPrefab = staticDataProvider.GetConfig<PrefabsConfig>().GetPrefab<PlayerBehaviour>();
        }

        public PlayerBehaviour CreatePlayer()
        {
            GameObject? player = _container.InstantiatePrefab(_playerBehaviourPrefab);
            var playerBehaviour = player.GetComponent<PlayerBehaviour>();
            return playerBehaviour;
        }
    }
}