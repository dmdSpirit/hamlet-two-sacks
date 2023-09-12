#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace HamletTwoSacks.Input
{
    public sealed class ButtonReader
    {
        private readonly InputAction _action;
        private readonly InputActionType _actionType;

        private readonly List<string> _receivers = new();
        private readonly Subject<InputActionType> _onStateChanged = new();

        private IDisposable? _sub;

        public bool IsPressed { get; private set; }
        public IObservable<InputActionType> OnStateChanged => _onStateChanged;
        public string? CurrentReceiver => _receivers.FirstOrDefault();

        public ButtonReader(InputAction action, InputActionType actionType)
        {
            _actionType = actionType;
            _action = action;
        }

        public void Enable()
        {
            _action.Enable();
            _onStateChanged.OnNext(_actionType);
        }

        public void Disable()
        {
            _action.Disable();
            _onStateChanged.OnNext(_actionType);
        }

        public void Update()
        {
            if (_action.IsPressed() == IsPressed)
                return;
            IsPressed = _action.IsPressed();
            _onStateChanged.OnNext(_actionType);
        }

        public void SubscribeToAction(string receiver)
        {
            Assert.IsFalse(_receivers.Contains(receiver));
            _receivers.Insert(0, receiver);
            _onStateChanged.OnNext(_actionType);
        }

        public void UnsubscribeFromAction(string receiver)
        {
            _receivers.Remove(receiver);
            _onStateChanged.OnNext(_actionType);
        }
    }
}