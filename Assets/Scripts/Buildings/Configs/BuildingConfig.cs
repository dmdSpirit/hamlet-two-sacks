#nullable enable
using System.Collections.Generic;
using HamletTwoSacks.Infrastructure.StaticData;

namespace HamletTwoSacks.Buildings.Configs
{
    public abstract class BuildingConfig : GameConfig
    {
        public abstract IReadOnlyList<BuildingTier>? BuildingTiers { get; }
    }
}