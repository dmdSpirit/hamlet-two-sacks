#nullable enable

using System;
using HamletTwoSacks.Commands;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalSlot : MonoBehaviour
    {
        private CommandsFactory _commandsFactory = null!;
        private CrystalFactory _crystalFactory = null!;

        private readonly ReactiveProperty<bool> _isFilled = new();

        private ICommand? _activeCommand;
        private IDisposable? _sub;
        private Crystal? _crystal;

        [SerializeField]
        private float _flySpeed = 5f;

        [SerializeField]
        private float _completionRadius = .1f;

        public IReadOnlyReactiveProperty<bool> IsFilled => _isFilled;

        [Inject]
        private void Construct(CommandsFactory commandsFactory, CrystalFactory crystalFactory)
        {
            _crystalFactory = crystalFactory;
            _commandsFactory = commandsFactory;
        }

        private void OnDisable()
            => DropCrystal();

        public void SetCrystalToSlot(Crystal crystal)
        {
            crystal.TurnPhysicsOff();
            FlyObjectToCommand flyCrystal =
                _commandsFactory.GetFlyObjectToCommand(crystal.transform, transform, _flySpeed, _completionRadius);
            _sub = flyCrystal.OnCompleted.Subscribe(OnCrystalArrive);
        }

        public void DropCrystal()
        {
            if (_crystal == null)
                return;
            if (_activeCommand != null)
            {
                _activeCommand.Interrupt();
                _sub!.Dispose();
            }

            _crystal.TurnPhysicsOn();
            _crystal = null!;
        }

        public void DestroyCrystal()
        {
            if (_crystal == null)
                return;
            _crystalFactory.DestroyCrystal(_crystal);
        }

        private void OnCrystalArrive(ICommand command)
        {
            Assert.IsTrue(command == _activeCommand);
            _isFilled.Value = true;
            _sub!.Dispose();
            _activeCommand = null;
        }
    }
}