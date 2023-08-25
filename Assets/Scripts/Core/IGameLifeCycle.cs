#nullable enable

namespace dmdspirit.Core
{
    public interface IGameLifeCycle
    {
        void Start();
        void NewGame();
        void ExitGame();
        void MainMenu();
    }
}