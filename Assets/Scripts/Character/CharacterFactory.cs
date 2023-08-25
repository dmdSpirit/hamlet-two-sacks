#nullable enable

using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Character
{
    public sealed class CharacterFactory : MonoBehaviour
    {
        private DiContainer _container = null!;

        [SerializeField]
        private Player _playerPrefab = null!;

        [Inject]
        private void Construct(DiContainer container)
            => _container = container;

        public Player CreatePlayer()
            => _container.InstantiatePrefabForComponent<Player>(_playerPrefab);
    }
}