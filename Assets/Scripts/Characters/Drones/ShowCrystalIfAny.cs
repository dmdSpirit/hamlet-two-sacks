#nullable enable

using System;
using HamletTwoSacks.Crystals;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Characters.Drones
{
    public sealed class ShowCrystalIfAny : MonoBehaviour
    {
        private IDisposable _sub = null!;

        [SerializeField]
        private CrystalContainer _crystalContainer = null!;

        [SerializeField]
        private GameObject _crystal = null!;

        private void Awake()
            => _sub = _crystalContainer.Crystals.Subscribe(UpdateCrystal);

        private void OnDestroy()
            => _sub.Dispose();

        private void UpdateCrystal(int crystals)
            => _crystal.SetActive(crystals > 0);
    }
}