#nullable enable

using dmdspirit.Core.UI.Buttons;
using HamletTwoSacks.Infrastructure.LifeCycle.States;
using TMPro;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.UI
{
    public sealed class LoadOtherLevelButton : ButtonWithStates
    {
        private LevelManager _levelManager = null!;

        [SerializeField]
        private TMP_Text _levelIndex = null!;

        public int OtherLevelIndex { get; private set; }

        [Inject]
        private void Construct(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        protected override void OnShown()
        {
            base.OnShown();
            if (_levelManager.CurrentLevelIndex == null)
                return;
            OtherLevelIndex = 3 - _levelManager.CurrentLevelIndex.Value;
            _levelIndex.text = OtherLevelIndex.ToString();
        }
    }
}