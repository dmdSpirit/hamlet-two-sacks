#nullable enable

using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Infrastructure
{
    public sealed class BackgroundMover : MonoBehaviour
    {
        private CameraController _cameraController = null!;

        private float _minX;
        private float _maxX;
        private float _horizontalCameraSize;
        private CinemachineBrain _brain;

        [SerializeField]
        private float _safeBorder = 1f;

        [SerializeField]
        private SpriteRenderer _background = null!;

        [SerializeField]
        private PolygonCollider2D _levelBounds = null!;

        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera = null!;

        [Inject]
        private void Construct(CameraController cameraController)
        {
            _cameraController = cameraController;
            _brain = _cameraController.Camera.GetComponent<CinemachineBrain>();
        }

        private void Awake()
        {
            List<float> points = _levelBounds.points.Select(point => point.x).Distinct().ToList();
            _horizontalCameraSize = _virtualCamera.m_Lens.OrthographicSize * Screen.width / Screen.height;

            _minX = points.Min() + _horizontalCameraSize;
            _maxX = points.Max() - _horizontalCameraSize;
        }

        private void FixedUpdate()
        {
            Vector3 cameraPos = _brain.CurrentCameraState.FinalPosition;
            float p = (cameraPos.x - _minX) / (_maxX - _minX);
            var position = _background.transform.position;
            position.x = (_background.transform.localScale.x - 1 - 2 * _horizontalCameraSize) * (2 * p - 1) / 2f
                         - cameraPos.x;
            position.x *= -1f;
            _background.transform.position = position;
        }
    }
}