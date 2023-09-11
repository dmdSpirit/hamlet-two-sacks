#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace HamletTwoSacks.Infrastructure.StaticData
{
    [CreateAssetMenu(menuName = "Config/Config List", fileName = "config_list", order = 0)]
    public sealed class ConfigList : ScriptableObject
    {
        [SerializeField]
        private ScriptableObject[] _configs = null!;

        public IReadOnlyList<ScriptableObject> Configs => _configs;
    }
}