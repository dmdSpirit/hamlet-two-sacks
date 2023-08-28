#nullable enable

using dmdspirit.Core;
using HamletTwoSacks.Infrastructure.LifeCycle.States;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle
{
    [UsedImplicitly]
    public sealed class GameLifeCycle : IGameLifeCycle
    {
        private readonly StateMachine _stateMachine = new("Life cycle");

        public GameLifeCycle(InitializeGameState initializeGameState, MainMenuState mainMenuState,
            NewGameState newGameState, ExitGameState exitGameState, LoadLevelState loadLevelState)
        {
            _stateMachine.RegisterState<InitializeGameState>(initializeGameState);
            _stateMachine.AddTransition(initializeGameState, _stateMachine.NextState<MainMenuState>);

            _stateMachine.RegisterState<MainMenuState>(mainMenuState);
            _stateMachine.AddTransition(mainMenuState, _stateMachine.ToState<ExitGameState>);
            _stateMachine.AddTransition(mainMenuState, _stateMachine.ToState<NewGameState>);

            _stateMachine.RegisterState<NewGameState>(newGameState);
            _stateMachine.AddTransition(newGameState, _stateMachine.ToState<LoadLevelState, int>);

            _stateMachine.RegisterState<LoadLevelState>(loadLevelState);
            _stateMachine.AddTransition(loadLevelState, _stateMachine.NextState<ExitGameState>);

            _stateMachine.RegisterState<ExitGameState>(exitGameState);
        }

        public void Start()
        {
            IState initialState = _stateMachine.GetState<InitializeGameState>()!;
            _stateMachine.Start(initialState);
        }

        public void NewGame()
            => _stateMachine.TriggerTransition(_stateMachine.GetState<NewGameState>());

        public void ExitGame()
            => _stateMachine.TriggerTransition(_stateMachine.GetState<ExitGameState>());

        public void MainMenu()
            => _stateMachine.TriggerTransition(_stateMachine.GetState<MainMenuState>());
    }
}