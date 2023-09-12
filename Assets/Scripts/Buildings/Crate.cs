#nullable enable
using System;
using HamletTwoSacks.Buildings.Configs;
using HamletTwoSacks.Physics;
using UnityEngine;

namespace HamletTwoSacks.Buildings
{
    public sealed class Crate : Building<CrateBuildingConfig, CrateTier>
    {
        [SerializeField]
        private TriggerDetector _triggerDetector = null!;

        protected override void OnStart()
        {
            throw new NotImplementedException();
        }

        protected override void OnUpgraded()
        {
            throw new NotImplementedException();
        }
    }
}