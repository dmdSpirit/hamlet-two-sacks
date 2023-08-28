#nullable enable

using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace HamletTwoSacks.Infrastructure
{
    [RequireComponent(typeof(Camera))]
    public sealed class CameraController : MonoBehaviour
    {
        private UniversalAdditionalCameraData _universalAdditionalCameraData = null!;
        private Camera? _uiCamera;

        public Camera Camera { get; private set; } = null!;

        private void Awake()
        {
            Camera = GetComponent<Camera>();
            _universalAdditionalCameraData = Camera.GetUniversalAdditionalCameraData();
        }

        public void AddUICamera(Camera uiCamera)
        {
            if (_uiCamera != null)
            {
                Debug.LogError($"Trying to add ui camera to camera stack second time.");
                return;
            }

            _universalAdditionalCameraData.cameraStack.Add(uiCamera);
            _uiCamera = uiCamera;
        }

        public void RemoveUICamera()
        {
            if (_uiCamera == null)
                return;
            _universalAdditionalCameraData.cameraStack.Remove(_uiCamera);
            _uiCamera = null;
        }
    }
}