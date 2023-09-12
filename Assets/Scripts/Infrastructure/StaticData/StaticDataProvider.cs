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

        public const string CONFIG_LIST_PATH = "config_list";

        public StaticDataProvider()
        {
            var configList = Resources.Load<ConfigList>(CONFIG_LIST_PATH);
            if (configList == null)
            {
                Debug.LogError($"Could not load {typeof(ConfigList)} at path {CONFIG_LIST_PATH}.");
                return;
            }

            _configs.AddRange(configList.Configs);
        }

        public T GetConfig<T>() where T : ScriptableObject
        {
            ScriptableObject? config = _configs.FirstOrDefault(c => c is T);
            if (config != null)
                return (T)config;

            Debug.LogError($"Could not find config of type {typeof(T).Name}");
            return null!;
        }
    }
}