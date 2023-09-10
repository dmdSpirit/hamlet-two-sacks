#nullable enable

using System.Collections.Generic;
using HamletTwoSacks.Infrastructure;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace HamletTwoSacks.Character
{
    public sealed class ActionButtonReader : MonoBehaviour
    {
        private readonly List<ActionReceiver> _actionReceivers = new();

        [SerializeField]
        private InputAction _actionButton = null!;

        [SerializeField]
        private GameObject _promptPanel = null!;

        [SerializeField]
        private TMP_Text _promptText = null!;

        [Inject]
        private void Construct(TimeController timeController)
            => timeController.FixedUpdate.Subscribe(OnFixedUpdate);

        private void OnEnable()
            => _actionButton.Enable();

        private void OnDisable()
            => _actionButton.Disable();

        public void SubscribeToAction(ActionReceiver actionReceiver)
        {
            _actionReceivers.Insert(0, actionReceiver);
            UpdatePrompt();
        }

        public void UnsubscribeFromAction(ActionReceiver actionReceiver)
        {
            _actionReceivers.Remove(actionReceiver);
            if (_promptPanel != null)
                UpdatePrompt();
        }

        private void OnFixedUpdate(float time)
        {
            if (_actionButton.IsPressed()
                && _actionReceivers.Count > 0)
                _actionReceivers[0].Callback.Invoke();
        }

        private void UpdatePrompt()
        {
            if (_actionReceivers.Count <= 0)
            {
                _promptPanel.SetActive(false);
                return;
            }

            _promptPanel.SetActive(true);
            _promptText.text = _actionReceivers[0].CallToAction;
        }
    }
}