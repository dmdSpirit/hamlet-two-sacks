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

        [SerializeField]
        private bool _useSpread = true;

        [SerializeField, ShowIf(nameof(_useSpread), true)]
        private float _spread = 0.5f;

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
            Vector3 position = transform.position;
            if (_useSpread)
                position.x += Random.Range(-_spread, _spread);
            crystal.transform.position = position;
            return crystal;
        }
    }
}