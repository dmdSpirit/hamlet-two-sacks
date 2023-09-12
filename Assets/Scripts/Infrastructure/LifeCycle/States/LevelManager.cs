#nullable enable

using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace HamletTwoSacks.Infrastructure.LifeCycle.States
{
    [UsedImplicitly]
    public sealed class LevelManager
    {
        private readonly SceneLoader _sceneLoader;

        private Scene? _loadedLevel;
        public int? CurrentLevelIndex { get; private set; }

        public LevelManager(SceneLoader sceneLoader)
            => _sceneLoader = sceneLoader;

        public async UniTask LoadLevel(int index)
        {
            _loadedLevel = await _sceneLoader.LoadSceneAdditive(index + 1);
            CurrentLevelIndex = index;
        }

        public async UniTask UnloadCurrentLevel()
        {
            if (_loadedLevel != null)
                await _sceneLoader.UnloadScene(_loadedLevel.Value);
            CurrentLevelIndex = null!;
        }
    }
}