#nullable enable

using HamletTwoSacks.Buildings.Config;
using UnityEngine;

namespace HamletTwoSacks.Buildings.DroneStation.Config
{
    [CreateAssetMenu(menuName = "Config/Buildings/Drone Station", fileName = "drone_station_config", order = 0)]
    public sealed class DroneStationConfig : BuildingConfig<DroneStationTier> { }
}