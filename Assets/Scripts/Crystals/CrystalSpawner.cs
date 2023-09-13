#nullable enable

using dmdspirit.Core.Attributes;
using HamletTwoSacks.Level;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalSpawner : MonoBehaviour
    {
        private LevelTransforms _levelTransforms = null!;
        private CrystalsManager _crystalsManager = null!;

        [SerializeField]
        private bool _useSpread = true;

        [SerializeField, ShowIf(nameof(_useSpread), true)]
        private float _spread = 0.5f;

        [SerializeField, Button(nameof(SpawnCrystal))]
        private bool _spawnCrystal;

        [Inject]
        private void Construct(CrystalsManager crystalsManager, LevelTransforms levelTransforms)
        {
            _crystalsManager = crystalsManager;
            _levelTransforms = levelTransforms;
        }

        public Crystal SpawnCrystal()
        {
            Crystal crystal = _crystalsManager.CreateCrystal();

            crystal.transform.SetParent(_levelTransforms.Crystals);
            Vector3 position = transform.position;
            if (_useSpread)
                position.x += Random.Range(-_spread, _spread);
            crystal.transform.position = position;
            return crystal;
        }
    }
}