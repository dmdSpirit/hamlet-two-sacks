#nullable enable

using dmdspirit.Core;
using dmdspirit.Core.UI;
using dmdspirit.Core.UI.Buttons;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.UI
{
    public sealed class MainMenuScreen : UIScreen
    {
        private IGameLifeCycle _gameLifeCycle = null!;

        [SerializeField]
        private ButtonWithStates _newGame = null!;

        [SerializeField]
        private ButtonWithStates _quit = null!;

        [Inject]
        private void Construct(IGameLifeCycle gameLifeCycle)
        {
            _gameLifeCycle = gameLifeCycle;
        }

        protected override void OnInitialize()
        {
            gameObject.SetActive(false);
            _newGame.OnClick.Subscribe(NewGame);
            _quit.OnClick.Subscribe(Quit);
        }

        protected override void OnShow() { }

        protected override void OnHide() { }

        private void NewGame(Unit _)
            => _gameLifeCycle.NewGame();

        private void Quit(Unit _)
            => _gameLifeCycle.ExitGame();
    }
}