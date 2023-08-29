#nullable enable
using Cinemachine;
using UnityEngine;

namespace HamletTwoSacks.Infrastructure
{
    public sealed class VCTest : MonoBehaviour
    {
        private Transform? _target;
        
        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera = null!;

        public void SetTarget(Transform target)
        {
            _target = target;
            _virtualCamera.Follow = _target;
        }

        private void OnDestroy()
            => _virtualCamera.Follow = null;
    }
}