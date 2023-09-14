#nullable enable
using HamletTwoSacks.Crystals;
using UnityEngine;

namespace HamletTwoSacks.AI
{
    public sealed class DroneSpawner: MonoBehaviour { }

    public sealed class Drone : MonoBehaviour { }

    public sealed class CollectCrystalsTask : Task
    {
        [SerializeField]
        private CrystalCollector _crystalCollector = null!;

        [SerializeField]
        private CrystalContainer _crystalContainer = null!;

        protected override void OnActivate()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnDeactivate()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnComplete()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate(float time)
        {
            throw new System.NotImplementedException();
        }

        public override void OnFixedUpdate(float time)
        {
            throw new System.NotImplementedException();
        }
    }
}