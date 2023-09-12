#nullable enable

using HamletTwoSacks.Buildings.Config;
using UnityEngine;

namespace HamletTwoSacks.Buildings.Mine.Config
{
    [CreateAssetMenu(menuName = "Config/Buildings/Mine", fileName = "mine_config", order = 0)]
    public sealed class MineBuildingConfig : BuildingConfig<MineTier> { }
}