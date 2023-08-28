#nullable enable

using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Character
{
    public sealed class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {
        private CharactersManager _charactersManager = null!;
        private IPlayerFactory _playerFactory = null!;
        private UnitsTransform _unitsTransform = null!;

        [SerializeField]
        private Transform _spawnPosition = null!;

        [Inject]
        private void Construct(CharactersManager charactersManager, IPlayerFactory playerFactory,
            UnitsTransform unitsTransform)
        {
            _unitsTransform = unitsTransform;
            _playerFactory = playerFactory;
            _charactersManager = charactersManager;
        }

        private void Awake()
            => _charactersManager.RegisterPlayerSpawner(this);

        private void OnDestroy()
            => _charactersManager.UnregisterPlayerSpawner(this);

        public Player SpawnPlayer()
        {
            Player player = _playerFactory.CreatePlayer();
            player.transform.SetParent(_unitsTransform.transform, false);
            player.transform.position = _spawnPosition.position;
            return player;
        }
    }
}