#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using HamletTwoSacks.Infrastructure;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HamletTwoSacks.Characters
{
    [UsedImplicitly]
    public sealed class EntityManager
    {
        private readonly IPrefabFactory _gameFactory;
        
        private readonly Dictionary<Type, List<MonoBehaviour>> _allSpawners = new();

        public EntityManager(IPrefabFactory gameFactory)
            => _gameFactory = gameFactory;

        public void RegisterSpawner<T>(Spawner<T> spawner) where T : MonoBehaviour
        {
            if (!_allSpawners.ContainsKey(typeof(T)))
                _allSpawners.Add(typeof(T), new List<MonoBehaviour>());
            _allSpawners[typeof(T)].Add(spawner);
        }

        public void UnregisterSpawner<T>(Spawner<T> spawner) where T : MonoBehaviour
            => _allSpawners[typeof(T)].Remove(spawner);

        public IEnumerable<Spawner<T>> GetSpawners<T>() where T : MonoBehaviour
        {
            if (!_allSpawners.ContainsKey(typeof(T)))
                return Enumerable.Empty<Spawner<T>>();
            return _allSpawners[typeof(T)].Cast<Spawner<T>>();
        }

        public T CreateObject<T>() where T : MonoBehaviour
        {
            return _gameFactory.CreateObject<T>();
        }

        public void DestroyObject<T>(T obj) where T : MonoBehaviour
        {
            Object.Destroy(obj.gameObject);
        }
    }
}