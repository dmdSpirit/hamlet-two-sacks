#nullable enable

using dmdspirit.Core.Attributes;
using HamletTwoSacks.Level;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalSpawner : MonoBehaviour
    {
        private ICrystalFactory _crystalFactory = null!;
        private LevelTransforms _levelTransforms = null!;

        [SerializeField, Button(nameof(SpawnCrystal))]
        private bool _spawnCrystal;

        [Inject]
        private void Construct(ICrystalFactory crystalFactory, LevelTransforms levelTransforms)
        {
            _levelTransforms = levelTransforms;
            _crystalFactory = crystalFactory;
        }

        public Crystal SpawnCrystal()
        {
            Crystal crystal = _crystalFactory.SpawnCrystal();

            crystal.transform.SetParent(_levelTransforms.Crystals);
            crystal.transform.position = transform.position;
            return crystal;
        }
    }
}