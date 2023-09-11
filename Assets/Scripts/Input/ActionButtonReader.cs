#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using HamletTwoSacks.Infrastructure;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using Zenject;

namespace HamletTwoSacks.Character
{
    public sealed class Action
    
    public sealed class ActionButtonReader : MonoBehaviour
    {
        private readonly ReactiveProperty<bool> _isActionPressed = new();
        private readonly Subject<string?> _onReceiverChanged = new();

        private readonly List<string> _receivers = new();

        [SerializeField]
        private InputAction _actionButton = null!;

        public IReadOnlyReactiveProperty<bool> IsActionPressed => _isActionPressed;
        public IObservable<string?> OnReceiverChanged => _onReceiverChanged;

        [Inject]
        private void Construct(TimeController timeController)
            => timeController.FixedUpdate.Subscribe(OnFixedUpdate);

        private void OnEnable()
            => _actionButton.Enable();

        private void OnDisable()
            => _actionButton.Disable();

        public void SubscribeToAction(string receiver)
        {
            Assert.IsFalse(_receivers.Contains(receiver));
            _receivers.Insert(0, receiver);
            _onReceiverChanged.OnNext(_receivers[0]);
        }

        public void UnsubscribeFromAction(string receiver)
        {
            _receivers.Remove(receiver);
            _onReceiverChanged.OnNext(_receivers.FirstOrDefault());
        }

        private void OnFixedUpdate(float time)
        {
            if (_actionButton.IsPressed()
                && _actionReceivers.Count > 0)
                _actionReceivers[0].Callback.Invoke();
        }
    }
}