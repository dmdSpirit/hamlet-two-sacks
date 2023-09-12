#nullable enable

using System;
using HamletTwoSacks.Crystals;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Character
{
    [RequireComponent(typeof(PlayerMovement))]
    public sealed class PlayerBehaviour : MonoBehaviour
    {
        private Player _player = null!;

        private IDisposable _sub = null!;

        [SerializeField]
        private CrystalCollector _crystalCollector = null!;

        [Inject]
        private void Construct(Player player)
            => _player = player;

        private void Awake()
            => _sub = _crystalCollector.OnCrystalCollected.Subscribe(OnCrystalCollected);

        private void OnDestroy()
            => _sub.Dispose();

        private void OnCrystalCollected(Unit _)
            => _player.AddCrystal();
    }
}