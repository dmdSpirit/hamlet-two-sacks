﻿#nullable enable

using System;
using Aether.Attributes;
using HamletTwoSacks.Buildings.Config;

namespace HamletTwoSacks.Buildings.Mine.Config
{
    [Serializable]
    public class MineTier : BuildingTier
    {
        [ShowIf(nameof(IsActive), true)]
        public int Work;

        [ShowIf(nameof(IsActive), true)]
        public int Drones;
    }
}