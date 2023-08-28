#nullable enable

using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Infrastructure
{
    [RequireComponent(typeof(Camera))]
    public sealed class UICameraController : MonoBehaviour, IInitializable
    {
        private CameraController _cameraController = null!;
        public Camera Camera { get; private set; } = null!;

        [Inject]
        private void Construct(CameraController cameraController)
        {
            _cameraController = cameraController;
        }

        public void Initialize()
        {
            Camera = GetComponent<Camera>();
            _cameraController.AddUICamera(Camera);
        }

        public void OnDestroy()
            => _cameraController.RemoveUICamera();
    }
}