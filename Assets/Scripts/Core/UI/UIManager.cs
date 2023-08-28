#nullable enable

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace dmdspirit.Core.UI
{
    [UsedImplicitly]
    public sealed class UIManager
    {
        private readonly Subject<IUIScreen> _onScreenShown = new();
        private readonly Subject<IUIScreen> _onScreenHidden = new();
        private readonly Dictionary<Type, IUIScreen> _screens = new();

        public IObservable<IUIScreen> OnScreenShown => _onScreenShown;
        public IObservable<IUIScreen> OnScreenHidden => _onScreenHidden;

        public void Register(IUIScreen screen)
        {
            if (_screens.TryGetValue(screen.GetType(), out IUIScreen? registered))
            {
                var registeredScreen = (MonoBehaviour)registered;
                var screenToRegister = (MonoBehaviour)screen;
                Debug.LogError($"Trying to register screen {screenToRegister.name} of type {screen.GetType().Name}, but {registeredScreen.name} of same type is already registered",
                               registeredScreen);
                return;
            }

            screen.OnScreenShown.Subscribe(OnShown);
            screen.OnScreenHidden.Subscribe(OnHidden);
            _screens[screen.GetType()] = screen;
            screen.Initialize();
        }

        public T GetScreen<T>() where T : class, IUIScreen
            => _screens[typeof(T)] as T ?? throw new InvalidOperationException();

        public void HideAll()
        {
            foreach (IUIScreen uiScreen in _screens.Values)
                uiScreen.Hide();
        }

        public void Hide<T>() where T : class, IUIScreen
        {
            var screen = GetScreen<T>();
            screen.Hide();
        }

        private void OnShown(IUIScreen screen)
            => _onScreenShown.OnNext(screen);

        private void OnHidden(IUIScreen screen)
            => _onScreenHidden.OnNext(screen);
    }
}