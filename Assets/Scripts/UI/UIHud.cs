#nullable enable

using dmdspirit.Core.UI;
using dmdspirit.Core.UI.Buttons;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.UI
{
    public sealed class UIHud : UIScreen
    {
        [SerializeField]
        private ButtonWithStates _menuButton = null!;

        protected override void OnInitialize()
        {
            _menuButton.OnClick.Subscribe(Menu);
            _menuButton.BindScreen(this);
        }

        protected override void OnShow() { }

        protected override void OnHide() { }

        private void Menu(Unit _)
            => UIManager.GetScreen<PauseMenuScreen>().Show();
    }
}