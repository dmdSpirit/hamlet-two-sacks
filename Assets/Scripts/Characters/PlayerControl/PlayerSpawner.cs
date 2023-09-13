#nullable enable

using HamletTwoSacks.Infrastructure;
using HamletTwoSacks.Level;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Characters.PlayerControl
{
    public sealed class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {
        private CharactersManager _charactersManager = null!;
        private LevelTransforms _levelTransforms = null!;
        private IPrefabFactory _prefabFactory = null!;

        // HACK (Stas): Temporary for testing.
        // - Stas 29 August 2023
        private VCTest _vcTest = null!;

        [SerializeField]
        private Transform _spawnPosition = null!;

        [Inject]
        private void Construct(CharactersManager charactersManager, VCTest vcTest, LevelTransforms levelTransforms,
            IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
            _levelTransforms = levelTransforms;
            _vcTest = vcTest;
            _charactersManager = charactersManager;
        }

        private void Awake()
            => _charactersManager.RegisterPlayerSpawner(this);

        private void OnDestroy()
        {
            if (_charactersManager != null!)
                _charactersManager.UnregisterPlayerSpawner(this);
        }

        public PlayerBehaviour SpawnPlayer()
        {
            var playerBehaviour = _prefabFactory.CreateObject<PlayerBehaviour>();
            playerBehaviour.transform.SetParent(_levelTransforms.Units);
            playerBehaviour.transform.position = _spawnPosition.position;
            _vcTest.SetTarget(playerBehaviour.transform);
            return playerBehaviour;
        }
    }
}