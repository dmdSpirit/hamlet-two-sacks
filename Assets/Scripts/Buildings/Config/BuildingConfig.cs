#nullable enable

using System.Collections.Generic;
using HamletTwoSacks.Infrastructure.StaticData;
using UnityEngine;

namespace HamletTwoSacks.Buildings.Config
{
    public abstract class BuildingConfig<T> : GameConfig where T : BuildingTier
    {
        [SerializeField]
        private List<T>? _tiers;

        public IReadOnlyList<T>? BuildingTiers => _tiers;
    }
}