#nullable enable
using System;
using dmdspirit.Core.CommonInterfaces;

namespace HamletTwoSacks.Input
{
    public interface IActionButtonsReader : IActivatableReactive
    {
        IObservable<InputActionType> OnStateChanged { get; }
        void SubscribeToAction(string receiver, InputActionType actionType);
        void UnsubscribeFromAction(string receiver, InputActionType actionType);
        bool IsPressed(InputActionType actionType);
        string? CurrentReceiver(InputActionType actionType);
    }
}