#nullable enable

using UnityEngine;

namespace HamletTwoSacks.Level
{
    public sealed class LevelTransforms : MonoBehaviour
    {
        [SerializeField]
        private Transform _units = null!;

        [SerializeField]
        private Transform _crystals = null!;

        public Transform Units => _units;
        public Transform Crystals => _crystals;
    }
}