#nullable enable
using System;

namespace dmdspirit.Core.UI.Buttons
{
    public class SelectableButtonWithStates : ButtonWithStates
    {
        private enum ButtonStates
        {
            Default = 0,
            MouseOver = 1,
            Clicked = 2,
            Locked = 3,
            SelectedDefault = 4,
            SelectedMouseOver = 5,
            SelectedClicked = 6,
        }

        public bool IsSelected { get; private set; }

        public void Select()
        {
            IsSelected = true;
            UpdateState();
        }

        public void Deselect()
        {
            IsSelected = false;
            UpdateState();
        }

        protected override void UpdateState()
        {
            if (!IsSelected)
            {
                base.UpdateState();
                return;
            }

            if (IsLocked)
            {
                _skinController.ActivateSkin((int)ButtonStates.Locked);
                return;
            }

            if (IsClicked)
            {
                _skinController.ActivateSkin((int)ButtonStates.SelectedClicked);
                return;
            }

            if (IsMouseOver)
            {
                _skinController.ActivateSkin((int)ButtonStates.SelectedMouseOver);
                return;
            }

            _skinController.ActivateSkin((int)ButtonStates.SelectedDefault);
        }

        protected override void UpdateSkinValues()
            => _skinController.SetValues(Enum.GetNames(typeof(ButtonStates)));
    }
}