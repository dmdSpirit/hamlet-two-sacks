#nullable enable
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace HamletTwoSacks.Infrastructure
{
    [UsedImplicitly]
    public sealed class GameFactory : IPrefabFactory, IGameFactory
    {
        private ScenePrefabFactory? _sceneFactory;

        public void BindSceneFactory(ScenePrefabFactory scenePrefabFactory)
        {
            Assert.IsNull(_sceneFactory);
            _sceneFactory = scenePrefabFactory;
        }

        public void UnbindSceneFactory(ScenePrefabFactory scenePrefabFactory)
            => _sceneFactory = null;

        public T CreateObject<T>() where T : MonoBehaviour
        {
            if (_sceneFactory == null)
            {
                Debug.LogError($"No binding for scene factory.");
                return null!;
            }

            return _sceneFactory.CreateObject<T>();
        }
        
        public T CreateCopyObject<T>(T prefab) where T : MonoBehaviour
        {
            if (_sceneFactory == null)
            {
                Debug.LogError($"No binding for scene factory.");
                return null!;
            }

            return _sceneFactory.CreateCopyObject(prefab);
        }
        
    }
}