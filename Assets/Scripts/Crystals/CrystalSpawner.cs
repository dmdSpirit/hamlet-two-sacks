#nullable enable

using dmdspirit.Core.Attributes;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalSpawner : MonoBehaviour
    {
        private ICrystalFactory _crystalFactory = null!;
        private CrystalsTransform _crystalsTransform = null!;

        [SerializeField, Button(nameof(SpawnCrystal))]
        private bool _spawnCrystal;

        [Inject]
        private void Construct(ICrystalFactory crystalFactory, CrystalsTransform crystalsTransform)
        {
            _crystalsTransform = crystalsTransform;
            _crystalFactory = crystalFactory;
        }

        public void SpawnCrystal()
        {
            Crystal crystal = _crystalFactory.SpawnCrystal();
            crystal.transform.SetParent(_crystalsTransform.transform);
            crystal.transform.position = transform.position;
        }
    }
}