#nullable enable

using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HamletTwoSacks.Infrastructure
{
    [UsedImplicitly]
    public sealed class SceneLoader
    {
        private readonly List<Scene> _loadedScenes = new();

        public async UniTask LoadSceneAdditive(int index)
        {
            await SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
            Scene loadedScene = SceneManager.GetSceneByBuildIndex(index);
            _loadedScenes.Add(loadedScene);
        }

        public async UniTask UnloadScene(int index)
        {
            Scene scene = _loadedScenes.FirstOrDefault(scene => scene.buildIndex == index);
            if (scene == null)
            {
                Debug.LogError($"Trying to unload scene with index {index}, but it was not loaded using {nameof(SceneLoader)}.");
                await SceneManager.UnloadSceneAsync(index);
                return;
            }

            _loadedScenes.Remove(scene);
            await SceneManager.UnloadSceneAsync(scene);
        }
    }
}