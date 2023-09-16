#nullable enable

using dmdspirit.Core.UI;
using dmdspirit.Core.UI.Buttons;
using HamletTwoSacks.Infrastructure.LifeCycle;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.GameMap
{
    public sealed class MapScreen : UIScreen
    {
        private ILevelControl _levelControl = null!;

        [SerializeField]
        private ButtonWithStates _back = null!;

        [SerializeField]
        private ButtonWithStates _level1 = null!;

        [SerializeField]
        private ButtonWithStates _level2 = null!;

        [SerializeField]
        private ButtonWithStates _level3 = null!;

        [Inject]
        private void Construct(ILevelControl levelControl)
            => _levelControl = levelControl;

        protected override void OnInitialize()
        {
            _back.OnClick.Subscribe(Back);
            _level1.OnClick.Subscribe(_ => LoadLevel(1));
            _level2.OnClick.Subscribe(_ => LoadLevel(2));
            _level3.OnClick.Subscribe(_ => LoadLevel(3));

            gameObject.SetActive(false);
        }

        protected override void OnShow() { }

        protected override void OnHide() { }

        private void LoadLevel(int levelIndex)
        {
            _levelControl.LoadLevel(levelIndex);
            Hide();
        }

        private void Back(Unit _)
            => Hide();
    }
}