#nullable enable

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace HamletTwoSacks.Infrastructure.StaticData
{
    [UsedImplicitly]
    public sealed class StaticDataProvider
    {
        private readonly List<ScriptableObject> _configs = new();

        private const string PREFABS_CONFIG_PATH = "Common/prefabs_config";

        public StaticDataProvider()
        {
            _configs.Add(LoadScriptable<PrefabsConfig>(PREFABS_CONFIG_PATH));
        }

        public T GetConfig<T>() where T : ScriptableObject
        {
            ScriptableObject? config = _configs.FirstOrDefault(c => c is T);
            if (config != null)
                return (T)config;

            Debug.LogError($"Could not find config of type {typeof(T).Name}");
            return null!;
        }

        private T LoadScriptable<T>(string path) where T : ScriptableObject
        {
            var result = Resources.Load<T>(path);
            if (result == null)
                Debug.LogError($"Could not load {typeof(T)} at path {path}.");

            return result!;
        }
    }
}