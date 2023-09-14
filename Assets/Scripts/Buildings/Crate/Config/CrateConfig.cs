#nullable enable

using HamletTwoSacks.Buildings.Config;
using UnityEngine;

namespace HamletTwoSacks.Buildings.Crate.Config
{
    [CreateAssetMenu(menuName = "Config/Buildings/Crate", fileName = "crate_config", order = 0)]
    public sealed class CrateConfig : BuildingConfig<CrateTier> { }
}