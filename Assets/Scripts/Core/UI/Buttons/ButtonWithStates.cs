#nullable enable

using System;
using dmdspirit.Core.Attributes;
using dmdspirit.Core.UI.Skins;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace dmdspirit.Core.UI.Buttons
{
    public class ButtonWithStates : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
        IPointerUpHandler
    {
        private enum ButtonStates
        {
            Default = 0,
            MouseOver = 1,
            Clicked = 2,
            Locked = 3
        }

        private readonly Subject<Unit> _onClick = new();
        private readonly CompositeDisposable _subs = new();

        protected bool IsMouseOver { get; private set; }
        protected bool IsClicked { get; private set; }

        [SerializeField]
        protected SkinController _skinController = null!;

        [SerializeField]
        private Tooltip? _lockTooltip;

        [SerializeField, Button(nameof(UpdateSkinValues))]
        private bool _updateValues;

        public bool IsLocked { get; private set; }
        public IObservable<Unit> OnClick => _onClick;

        private void OnDestroy()
            => _subs.Clear();

        public void ResetState()
        {
            IsMouseOver = false;
            IsClicked = false;
            IsLocked = false;
            UpdateState();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsMouseOver = true;
            UpdateState();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsMouseOver = false;
            UpdateState();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsClicked = true;
            UpdateState();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsClicked = false;
            UpdateState();
            if (!IsMouseOver
                || eventData.dragging)
                return;

            if (!IsLocked)
                _onClick.OnNext(Unit.Default);
            else if (_lockTooltip != null)
                _lockTooltip.Show();
        }

        public void Lock()
        {
            IsLocked = true;
            UpdateState();
        }

        public void Unlock()
        {
            IsLocked = false;
            UpdateState();
        }

        public void BindScreen(IUIScreen uiScreen)
        {
            _subs.Clear();
            uiScreen.OnScreenShown.Subscribe(OnScreenShown);
            uiScreen.OnScreenHidden.Subscribe(OnScreenHidden);
        }

        protected virtual void UpdateState()
        {
            if (IsLocked)
            {
                _skinController.ActivateSkin((int)ButtonStates.Locked);
                return;
            }

            if (IsClicked)
            {
                _skinController.ActivateSkin((int)ButtonStates.Clicked);
                return;
            }

            if (IsMouseOver)
            {
                _skinController.ActivateSkin((int)ButtonStates.MouseOver);
                return;
            }

            _skinController.ActivateSkin((int)ButtonStates.Default);
        }

        protected virtual void UpdateSkinValues()
            => _skinController.SetValues(Enum.GetNames(typeof(ButtonStates)));
        
        protected virtual void OnShown(){}
        protected virtual void OnHidden(){}

        private void OnScreenShown(IUIScreen _)
        {
            ResetState();
            OnShown();
        }

        private void OnScreenHidden(IUIScreen _)
            => OnHidden();
    }
}