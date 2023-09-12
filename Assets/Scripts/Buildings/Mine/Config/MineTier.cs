#nullable enable
using System;
using dmdspirit.Core.Attributes;
using HamletTwoSacks.Buildings.Config;

namespace HamletTwoSacks.Buildings.Mine.Config
{
    [Serializable]
    public class MineTier : BuildingTier
    {
        [ShowIf(nameof(IsActive), true)]
        public int ProductionCooldown;
    }
}