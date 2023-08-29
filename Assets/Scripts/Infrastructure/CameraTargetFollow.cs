#nullable enable
using HamletTwoSacks.Character;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Infrastructure
{
    public sealed class CameraTargetFollow : MonoBehaviour
    {
        private CameraController _cameraController = null!;
        private CharactersManager _charactersManager = null!;
        private TimeController _timeController = null!;

        private bool _isFollowing;

        [SerializeField]
        private float _innerBoundsSpeed = 2f;

        [SerializeField]
        private float _outerBoundsSpeed = 2f;

        [SerializeField]
        private float _boundsWidth = 0.2f;

        [SerializeField]
        private float _deadZoneWidth = 0.2f;

        [Inject]
        private void Construct(CameraController cameraController, CharactersManager charactersManager,
            TimeController timeController)
        {
            _timeController = timeController;
            _charactersManager = charactersManager;
            _cameraController = cameraController;

            timeController.LateUpdate.Subscribe(OnLateUpdate);
        }

        public void FocusImmediately()
        {
            if (_charactersManager.Player == null)
            {
                Debug.LogError($"Trying to focus on player, but {nameof(CharactersManager)} does not have player reference.");
                return;
            }

            Vector3 cameraPosition = _cameraController.Camera.transform.position;
            cameraPosition.x = _charactersManager.Player.transform.position.x;
            _cameraController.Camera.transform.position = cameraPosition;
        }

        public void StartFollow()
            => _isFollowing = true;

        public void StopFollow()
            => _isFollowing = false;

        private void OnLateUpdate(Unit _)
        {
            if (!_isFollowing
                || _charactersManager.Player == null)
                return;
            Vector3 playerPosition = _charactersManager.Player.transform.position;
            Vector3 screenPosition = _cameraController.Camera.WorldToViewportPoint(playerPosition);
            if (IsInDeadZone(screenPosition.x))
                return;
            float speed = _innerBoundsSpeed;
            if (IsOutOfBounds(screenPosition.x))
                speed = _outerBoundsSpeed;
            float direction = Mathf.Sign(screenPosition.x - 0.5f);
            _cameraController.Camera.transform.Translate(speed * direction * _timeController.DeltaTime, 0, 0);

            bool IsInDeadZone(float x)
                => x >= 0.5f - _deadZoneWidth / 2f && x <= 0.5f + _deadZoneWidth / 2;

            bool IsOutOfBounds(float x)
                => screenPosition.x <= _boundsWidth || screenPosition.x >= 1 - _boundsWidth;
        }
    }
}