#nullable enable

using System;
using System.Collections.Generic;
using HamletTwoSacks.Infrastructure.StaticData;
using HamletTwoSacks.Time;
using JetBrains.Annotations;
using UniRx;
using Zenject;

namespace HamletTwoSacks.Input
{
    [UsedImplicitly]
    public sealed class ActionButtonsReader : IActionButtonsReader, IInitializable
    {
        private TimeController _timeController = null!;

        private readonly ReactiveProperty<bool> _isActive = new();
        private readonly Dictionary<InputActionType, ButtonReader> _buttonReaders = new();

        private IDisposable? _sub;

        private ButtonReader _interactButtonReader = null!;
        private ButtonReader _payButtonReader = null!;

        public IReadOnlyReactiveProperty<bool> IsActive => _isActive;
        public IObservable<InputActionType> OnStateChanged
            => _interactButtonReader.OnStateChanged.Merge(_payButtonReader.OnStateChanged);

        [Inject]
        private void Construct(TimeController timeController, StaticDataProvider staticDataProvider)
        {
            _timeController = timeController;
            var config = staticDataProvider.GetConfig<InputConfig>();
            _interactButtonReader = new ButtonReader(config.InteractAction, InputActionType.Interact);
            _payButtonReader = new ButtonReader(config.PayAction, InputActionType.Pay);
        }

        public void Initialize()
        {
            _buttonReaders.Add(InputActionType.Interact, _interactButtonReader);
            _buttonReaders.Add(InputActionType.Pay, _payButtonReader);
            _interactButtonReader.Disable();
            _payButtonReader.Disable();
        }

        public void Activate()
        {
            if (_isActive.Value)
                return;
            _sub = _timeController.Update.Subscribe(OnUpdate);
            _interactButtonReader.Enable();
            _payButtonReader.Enable();
            _isActive.Value = true;
        }

        public void Deactivate()
        {
            _sub?.Dispose();
            _interactButtonReader.Disable();
            _payButtonReader.Disable();
            _isActive.Value = false;
        }

        public void SubscribeToAction(string receiver, InputActionType actionType)
            => _buttonReaders[actionType].SubscribeToAction(receiver);

        public void UnsubscribeFromAction(string receiver, InputActionType actionType)
            => _buttonReaders[actionType].UnsubscribeFromAction(receiver);

        public bool IsPressed(InputActionType actionType)
            => _buttonReaders[actionType].IsPressed;

        public string? CurrentReceiver(InputActionType actionType)
            => _buttonReaders[actionType].CurrentReceiver;

        private void OnUpdate(float time)
        {
            _interactButtonReader.Update();
            _payButtonReader.Update();
        }
    }
}