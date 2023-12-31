﻿#nullable enable

using System;
using Aether.Attributes;
using HamletTwoSacks.Buildings.Config;

namespace HamletTwoSacks.Buildings.DroneStation.Config
{
    [Serializable]
    public class DroneStationTier : BuildingTier
    {
        [ShowIf(nameof(IsActive), true)]
        public int Drones;
    }
}