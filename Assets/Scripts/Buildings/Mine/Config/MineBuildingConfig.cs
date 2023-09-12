#nullable enable

using HamletTwoSacks.Buildings.Config;
using UnityEngine;

namespace HamletTwoSacks.Buildings.Mine.Config
{
    [CreateAssetMenu(menuName = "Config/Buildings/Mine", fileName = "mine_config", order = 0)]
    public sealed class MineBuildingConfig : BuildingConfig<MineTier>
    {
        [SerializeField]
        private float _crystalProductionTime;

        [SerializeField]
        private float _playerWork;

        public float CrystalProductionTime => _crystalProductionTime;
        public float PlayerWork => _playerWork;
    }
}