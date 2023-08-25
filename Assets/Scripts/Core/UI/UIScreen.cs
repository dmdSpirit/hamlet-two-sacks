#nullable enable
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace dmdspirit.Core.UI
{
    public abstract class UIScreen : MonoBehaviour, IUIScreen
    {
        private readonly Subject<IUIScreen> _onScreenShown = new();
        private readonly Subject<IUIScreen> _onScreenHidden = new();

        private bool _isShown;
        private bool _isInitialized;
        private IShowTransitionHandler[]? _showTransitionHandlers;
        private IHideTransitionHandler[]? _hideTransitionHandlers;

        protected IUIManager UIManager = null!;

        public IObservable<IUIScreen> OnScreenShown => _onScreenShown;
        public IObservable<IUIScreen> OnScreenHidden => _onScreenHidden;

        public bool IsInitialized => _isInitialized;
        public bool IsShown => _isShown;

        [Inject]
        protected void Register(IUIManager uiManager)
        {
            UIManager = uiManager;
            _showTransitionHandlers = GetComponents<IShowTransitionHandler>();
            _hideTransitionHandlers = GetComponents<IHideTransitionHandler>();
            uiManager.Register(this);
        }

        // FIXME (Stas): May be it will be better to also use TransitionHandlers
        // - Stas 25 August 2023
        public void Initialize()
        {
            OnInitialize();
            _isInitialized = true;
        }

        public void Show()
        {
            _isShown = true;
            StopHideTransitions();
            HandleShowTransitions();
            OnShow();
            _onScreenShown.OnNext(this);
        }

        public void Hide()
        {
            _isShown = false;
            OnHide();
            StopShowTransitions();
            HandleHideTransitions();
            _onScreenHidden.OnNext(this);
        }

        protected abstract void OnInitialize();
        protected abstract void OnShow();
        protected abstract void OnHide();

        private void HandleShowTransitions()
        {
            if (_showTransitionHandlers == null)
                return;

            foreach (IShowTransitionHandler showTransitionHandler in _showTransitionHandlers)
                showTransitionHandler.OnShow();
        }

        private void HandleHideTransitions()
        {
            if (_hideTransitionHandlers == null)
                return;

            foreach (IHideTransitionHandler hideTransitionHandler in _hideTransitionHandlers)
                hideTransitionHandler.OnHide();
        }

        private void StopShowTransitions()
        {
            if (_showTransitionHandlers == null)
                return;

            foreach (IShowTransitionHandler showTransitionHandler in _showTransitionHandlers)
                showTransitionHandler.Stop();
        }

        private void StopHideTransitions()
        {
            if (_hideTransitionHandlers == null)
                return;

            foreach (IHideTransitionHandler hideTransitionHandler in _hideTransitionHandlers)
                hideTransitionHandler.Stop();
        }
    }
}