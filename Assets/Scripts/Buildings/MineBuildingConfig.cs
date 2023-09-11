#nullable enable

using System.Collections.Generic;
using UnityEngine;

namespace HamletTwoSacks.Buildings
{
    [CreateAssetMenu(menuName = "Config/Buildings/Mine", fileName = "mine_config", order = 0)]
    public sealed class MineBuildingConfig : ScriptableObject
    {
        [SerializeField]
        private MineTierInfo[]? _tierInfos;

        public IReadOnlyList<MineTierInfo>? TierInfos => _tierInfos;
    }
}