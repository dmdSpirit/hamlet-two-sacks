#nullable enable

using dmdspirit.Core.Attributes;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalSpawner : MonoBehaviour
    {
        private ICrystalFactory _crystalFactory = null!;

        // private CrystalsTransform _crystalsTransform = null!;

        [SerializeField, Button(nameof(SpawnCrystal))]
        private bool _spawnCrystal;

        [Inject]

        // private void Construct(ICrystalFactory crystalFactory, CrystalsTransform crystalsTransform)
        private void Construct(ICrystalFactory crystalFactory)
        {
            // _crystalsTransform = crystalsTransform;
            _crystalFactory = crystalFactory;
        }

        public Crystal SpawnCrystal()
        {
            Crystal crystal = _crystalFactory.SpawnCrystal();

            // crystal.transform.SetParent(_crystalsTransform.transform);
            crystal.transform.position = transform.position;
            return crystal;
        }
    }
}