#nullable enable

using dmdspirit.Core;
using dmdspirit.Core.UI;
using dmdspirit.Core.UI.Buttons;
using HamletTwoSacks.Infrastructure.LifeCycle;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.UI
{
    public sealed class PauseMenuScreen : UIScreen
    {
        private IGameLifeCycle _gameLifeCycle = null!;

        [SerializeField]
        private ButtonWithStates _mainMenu = null!;

        [SerializeField]
        private ButtonWithStates _quit = null!;

        [SerializeField]
        private ButtonWithStates _closeButton = null!;

        [SerializeField]
        private ClickInvoker _closeInvoker = null!;

        [SerializeField]
        private LoadOtherLevelButton _loadOtherLevelButton = null!;

        [Inject]
        private void Construct(IGameLifeCycle gameLifeCycle)
        {
            _gameLifeCycle = gameLifeCycle;
        }

        protected override void OnInitialize()
        {
            gameObject.SetActive(false);

            _mainMenu.OnClick.Subscribe(MainMenu);
            _mainMenu.BindScreen(this);
            _quit.OnClick.Subscribe(Quit);
            _quit.BindScreen(this);
            _closeButton.OnClick.Subscribe(Close);
            _closeButton.BindScreen(this);
            _loadOtherLevelButton.OnClick.Subscribe(LoadOtherLevel);
            _loadOtherLevelButton.BindScreen(this);

            _closeInvoker.OnClick.Subscribe(Close);
        }

        protected override void OnShow() { }

        protected override void OnHide() { }

        private void MainMenu(Unit _)
            => _gameLifeCycle.MainMenu();

        private void Quit(Unit _)
            => _gameLifeCycle.ExitGame();

        private void Close(Unit _)
            => Hide();

        private void LoadOtherLevel(Unit _)
        {
            ((ILevelControl)_gameLifeCycle).LoadLevel(_loadOtherLevelButton.OtherLevelIndex);
            Hide();
        }
    }
}