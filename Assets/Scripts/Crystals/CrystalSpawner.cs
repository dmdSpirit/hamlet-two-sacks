#nullable enable

using dmdspirit.Core.Attributes;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalSpawner : MonoBehaviour
    {
        private CrystalFactory _crystalFactory = null!;

        [SerializeField, Button(nameof(SpawnCrystal))]
        private bool _spawnCrystal;

        [Inject]
        private void Construct(CrystalFactory crystalFactory)
        {
            _crystalFactory = crystalFactory;
        }

        public void SpawnCrystal()
        {
            _crystalFactory.SpawnCrystalAt(transform);
        }
    }
}