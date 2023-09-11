#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using dmdspirit.Core.CommonInterfaces;
using HamletTwoSacks.Infrastructure;
using HamletTwoSacks.Infrastructure.StaticData;
using JetBrains.Annotations;
using UniRx;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using Zenject;

namespace HamletTwoSacks.Input
{
    [UsedImplicitly]
    public sealed class ActionButtonReader : IActivatableReactive
    {
        private TimeController _timeController = null!;

        private readonly ReactiveProperty<bool> _isActionPressed = new();
        private readonly Subject<string?> _onReceiverChanged = new();
        private readonly List<string> _receivers = new();
        private readonly ReactiveProperty<bool> _isActive = new();

        private InputAction _action = null!;
        private IDisposable? _sub;

        public IReadOnlyReactiveProperty<bool> IsActionPressed => _isActionPressed;
        public IObservable<string?> OnReceiverChanged => _onReceiverChanged;
        public IReadOnlyReactiveProperty<bool> IsActive => _isActive;

        [Inject]
        private void Construct(TimeController timeController, StaticDataProvider staticDataProvider)
        {
            _timeController = timeController;
            _action = staticDataProvider.GetConfig<InputConfig>().Action;
            _action.Disable();
        }
        
        public void Activate()
        {
            if (_isActive.Value)
                return;
            _sub = _timeController.Update.Subscribe(OnUpdate);
            _action.Enable();
            _isActive.Value = true;
        }

        public void Deactivate()
        {
            _sub?.Dispose();
            _action.Disable();
            _isActive.Value = false;
        }

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

        private void OnUpdate(float time)
        {
            if (_action.IsPressed() == _isActionPressed.Value)
                return;
            _isActionPressed.Value = _action.IsPressed();
        }
    }
}