#nullable enable

using aether.Aether;
using aether.Aether.CommonInterfaces;
using HamletTwoSacks.Infrastructure.LifeCycle.States;
using JetBrains.Annotations;

namespace HamletTwoSacks.Infrastructure.LifeCycle
{
    [UsedImplicitly]
    public sealed class GameLifeCycle : IGameLifeCycle, ILevelControl
    {
        private readonly StateMachine _stateMachine = new("Life cycle");

        public GameLifeCycle(InitializeGameState initializeGameState, MainMenuState mainMenuState,
            NewGameState newGameState, ExitGameState exitGameState, GameState gameState, SelectLevelState selectLevelState)
        {
            _stateMachine.RegisterState<InitializeGameState>(initializeGameState);
            _stateMachine.AddTransition(initializeGameState, _stateMachine.NextState<MainMenuState>);

            _stateMachine.RegisterState<MainMenuState>(mainMenuState);
            _stateMachine.AddTransition(mainMenuState, _stateMachine.ToState<ExitGameState>);
            _stateMachine.AddTransition(mainMenuState, _stateMachine.ToState<NewGameState>);

            _stateMachine.RegisterState<NewGameState>(newGameState);
            _stateMachine.AddTransition(newGameState, _stateMachine.NextState<SelectLevelState>);
            
            _stateMachine.RegisterState<SelectLevelState>(selectLevelState);
            _stateMachine.AddTransition(selectLevelState, _stateMachine.ToState<GameState, int>);

            _stateMachine.RegisterState<GameState>(gameState);
            _stateMachine.AddTransition(gameState, _stateMachine.ToState<ExitGameState>);
            _stateMachine.AddTransition(gameState, _stateMachine.ToState<MainMenuState>);
            _stateMachine.AddTransition(gameState, _stateMachine.ToState<GameState, int>);

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

        // HACK (Stas): Index yeah..
        // - Stas 29 August 2023
        public void LoadLevel(int level)
            => _stateMachine.TriggerTransition(_stateMachine.GetState<GameState>(), level);
    }
}