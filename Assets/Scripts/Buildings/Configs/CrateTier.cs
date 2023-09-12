#nullable enable
using System;
using dmdspirit.Core.Attributes;

namespace HamletTwoSacks.Buildings.Configs
{
    [Serializable]
    public class CrateTier : BuildingTier
    {
        [ShowIf(nameof(IsActive), true)]
        public int Capacity;
    }
}