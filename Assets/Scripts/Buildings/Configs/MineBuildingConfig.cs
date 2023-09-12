#nullable enable

using UnityEngine;

namespace HamletTwoSacks.Buildings.Configs
{
    [CreateAssetMenu(menuName = "Config/Buildings/Mine", fileName = "mine_config", order = 0)]
    public sealed class MineBuildingConfig : BuildingConfig<MineTier> { }
}