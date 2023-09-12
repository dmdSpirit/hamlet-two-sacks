#nullable enable

using System.Collections.Generic;
using UnityEngine;

namespace HamletTwoSacks.Buildings.Configs
{
    [CreateAssetMenu(menuName = "Config/Buildings/Crate", fileName = "crate_config", order = 0)]
    public sealed class CrateBuildingConfig : BuildingConfig
    {
        [SerializeField]
        private CrateTier[]? _tierInfos;

        public IReadOnlyList<CrateTier>? TierInfos => _tierInfos;
        public override IReadOnlyList<BuildingTier>? BuildingTiers => _tierInfos;
    }
}