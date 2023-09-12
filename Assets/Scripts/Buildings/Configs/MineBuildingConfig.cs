#nullable enable

using System.Collections.Generic;
using UnityEngine;

namespace HamletTwoSacks.Buildings.Configs
{
    [CreateAssetMenu(menuName = "Config/Buildings/Mine", fileName = "mine_config", order = 0)]
    public sealed class MineBuildingConfig : BuildingConfig
    {
        [SerializeField]
        private MineTier[]? _tierInfos;

        public IReadOnlyList<MineTier>? TierInfos => _tierInfos;
        public override IReadOnlyList<BuildingTier>? BuildingTiers => _tierInfos;
    }
}