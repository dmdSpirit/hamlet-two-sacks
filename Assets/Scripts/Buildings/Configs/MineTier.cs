#nullable enable
using System;
using dmdspirit.Core.Attributes;

namespace HamletTwoSacks.Buildings.Configs
{
    [Serializable]
    public class MineTier : BuildingTier
    {
        [ShowIf(nameof(IsActive), true)]
        public int ProductionCooldown;
    }
}