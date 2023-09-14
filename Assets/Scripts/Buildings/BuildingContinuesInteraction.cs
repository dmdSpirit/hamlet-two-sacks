#nullable enable

using System;
using dmdspirit.Core.CommonInterfaces;
using HamletTwoSacks.Characters.PlayerControl;
using HamletTwoSacks.Crystals;
using HamletTwoSacks.Crystals.UI;
using HamletTwoSacks.Infrastructure;
using HamletTwoSacks.Input;
using HamletTwoSacks.Physics;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Buildings
{
    public sealed class BuildingContinuesInteraction : MonoBehaviour
    {
        private const InputActionType ACTION_TYPE = InputActionType.Interact;

        private IActionButtonsReader _actionButtonsReader = null!;

        private readonly ReactiveProperty<bool> _isButtonPressed = new();

        private IDisposable? _sub;
        private IDisposable? _timerSub;
        private string _stringID = null!;
        private CrystalCostPanel? _costPanel;
        private bool _playerIsInside;

        [SerializeField]
        private TriggerDetector _triggerDetector = null!;

        [SerializeField]
        private GameObject _tooltipPanel = null!;

        public bool IsActive { get; private set; }
        public IReadOnlyReactiveProperty<bool> IsButtonPressed => _isButtonPressed;

        [Inject]
        private void Construct(IActionButtonsReader actionButtonsReader)
        {
            _actionButtonsReader = actionButtonsReader;
            _stringID = this.ToStringID(true);
        }

        private void Awake()
        {
            _triggerDetector.OnTriggerEnter.Subscribe(TriggerEnter);
            _triggerDetector.OnTriggerExit.Subscribe(TriggerExit);
            _tooltipPanel.SetActive(false);
        }

        public void Activate()
        {
            IsActive = true;
            if (_playerIsInside)
                SubscribeOnInteraction();
        }

        public void Deactivate()
        {
            IsActive = false;
            StopInteraction();
        }

        private void TriggerEnter(Collider2D target)
        {
            var player = target.gameObject.GetComponent<PlayerBehaviour>();
            if (player == null)
                return;
            _playerIsInside = true;
            if (!IsActive)
                return;
            SubscribeOnInteraction();
        }

        private void TriggerExit(Collider2D target)
        {
            if (!_playerIsInside)
                return;

            var player = target.gameObject.GetComponent<PlayerBehaviour>();
            if (player == null)
                return;
            _playerIsInside = false;
            StopInteraction();
        }

        private void UpdateButtonReading(InputActionType _)
        {
            bool isPressed = _actionButtonsReader.IsActive.Value && _actionButtonsReader.IsPressed(ACTION_TYPE)
                             && string.Equals(_actionButtonsReader.CurrentReceiver(ACTION_TYPE), _stringID);
            if (isPressed && !_isButtonPressed.Value)
                _isButtonPressed.Value = true;
            else if (!isPressed
                     && _isButtonPressed.Value)
                _isButtonPressed.Value = false;
        }

        private void StopInteraction()
        {
            _tooltipPanel.SetActive(false);
            _sub?.Dispose();
            _actionButtonsReader.UnsubscribeFromAction(_stringID, ACTION_TYPE);
            _isButtonPressed.Value = false;
        }

        private void SubscribeOnInteraction()
        {
            _sub?.Dispose();
            _tooltipPanel.gameObject.SetActive(true);
            _actionButtonsReader.SubscribeToAction(_stringID, ACTION_TYPE);
            _sub = _actionButtonsReader.OnStateChanged.Where(type => type == ACTION_TYPE)
                .Subscribe(UpdateButtonReading);
        }
    }
}