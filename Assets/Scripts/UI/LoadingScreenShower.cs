#nullable enable

using dmdspirit.Core.UI;
using JetBrains.Annotations;
using Zenject;

namespace HamletTwoSacks.UI
{
    [UsedImplicitly]
    public sealed class LoadingScreenShower : IInitializable
    {
        private readonly UIManager _uiManager;

        private LoadingScreen _loadingScreen = null!;

        public LoadingScreenShower(UIManager uiManager)
            => _uiManager = uiManager;

        public void Initialize()
            => _loadingScreen = _uiManager.GetScreen<LoadingScreen>();

        public void ShowLoadingScreen(float? progress = default)
        {
            _loadingScreen.Show();
            if (progress.HasValue)
                _loadingScreen.SetProgress(progress.Value);
        }

        public void HideLoadingScreen()
            => _loadingScreen.Hide();
    }
}