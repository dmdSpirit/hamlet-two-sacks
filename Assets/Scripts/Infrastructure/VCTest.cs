#nullable enable

using System;
using Cinemachine;
using HamletTwoSacks.Characters.PlayerControl;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Infrastructure
{
    // HACK (Stas): Temporary for testing.
    // - Stas 29 August 2023
    public sealed class VCTest : MonoBehaviour
    {
        private Transform? _target;
        private IDisposable _sub = null!;

        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera = null!;

        [Inject]
        public void Construct(PlayerManager playerManager)
            => _sub = playerManager.OnPlayerSpawned.Subscribe(player=>SetTarget(player.transform));

        private void SetTarget(Transform target)
        {
            _target = target;
            _virtualCamera.Follow = _target;
        }

        private void OnDestroy()
            => _virtualCamera.Follow = null;
    }
}