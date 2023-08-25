#nullable enable

using JetBrains.Annotations;

namespace dmdspirit.Core
{
    [UsedImplicitly]
    public sealed class GameQuitter : IGameQuitter
    {
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}