#nullable enable

using System;
using Aether.Attributes;
using HamletTwoSacks.Buildings.Config;

namespace HamletTwoSacks.Buildings.Crate.Config
{
    [Serializable]
    public class CrateTier : BuildingTier
    {
        [ShowIf(nameof(IsActive), true)]
        public int Capacity;
    }
}