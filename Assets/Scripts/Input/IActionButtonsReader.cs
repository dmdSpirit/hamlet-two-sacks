#nullable enable

using System;
using dmdspirit.Core.CommonInterfaces;
using UniRx;

namespace HamletTwoSacks.Input
{
    public interface IActionButtonsReader
    {
        void Activate();
        void Deactivate();
        IReadOnlyReactiveProperty<bool> IsActive { get; }
        IObservable<InputActionType> OnStateChanged { get; }
        void SubscribeToAction(string receiver, InputActionType actionType);
        void UnsubscribeFromAction(string receiver, InputActionType actionType);
        bool IsPressed(InputActionType actionType);
        string? CurrentReceiver(InputActionType actionType);
    }
}