#nullable enable

using Aether.UI;
using Aether.UI.Buttons;
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

        protected override void OnShow()
        {
            UIManager.GetScreen<ResourcesPanel>().Show();
        }

        protected override void OnHide()
        {
            UIManager.GetScreen<ResourcesPanel>().Hide();
        }

        private void Menu(Unit _)
            => UIManager.GetScreen<PauseMenuScreen>().Show();
    }
}