#nullable enable

using UnityEngine;

namespace HamletTwoSacks.Buildings.Configs
{
    [CreateAssetMenu(menuName = "Config/Buildings/Crate", fileName = "crate_config", order = 0)]
    public sealed class CrateBuildingConfig : BuildingConfig<CrateTier> { }
}