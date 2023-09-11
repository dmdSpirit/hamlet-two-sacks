#nullable enable
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HamletTwoSacks.Infrastructure.StaticData
{
    [CreateAssetMenu(menuName = "Config/Config List", fileName = "config_list", order = 0)]
    public sealed class ConfigList : ScriptableObject
    {
        [SerializeField]
        private List<GameConfig> _configs = null!;

        public IEnumerable<GameConfig> Configs => _configs;

#if UNITY_EDITOR
        public void AddConfig(GameConfig config)
        {
            if (_configs.Contains(config))
            {
                Debug.LogWarning($"{nameof(ConfigList)} already contains {config.GetType().Name}");
                return;
            }

            var description = $"Added {config.GetType().Name} to {nameof(ConfigList)}";
            Undo.RecordObject(this, description);
            _configs.Add(config);
            Debug.Log(description);
        }

        public void RemoveConfig(GameConfig config)
        {
            if (!_configs.Contains(config))
            {
                Debug.LogWarning($"{nameof(ConfigList)} already does not contain {config.GetType().Name}");
                return;
            }

            var description = $"Removed {config.GetType().Name} from {nameof(ConfigList)}";
            Undo.RecordObject(this, description);
            _configs.Remove(config);
            Debug.Log(description);
        }
#endif
    }
}