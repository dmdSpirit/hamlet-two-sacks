#nullable enable

using System;
using HamletTwoSacks.Characters;
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
        private EntityManager _entityManager = null!;

        private readonly ReactiveProperty<bool> _isFilled = new();

        private ICommand? _activeCommand;
        private IDisposable? _sub;
        private Crystal? _crystal;

        [SerializeField]
        private float _flySpeed = 5f;

        [SerializeField]
        private float _completionRadius = .1f;

        public IReadOnlyReactiveProperty<bool> IsFilled => _isFilled;
        public bool IsCrystalFlying => _activeCommand != null;

        [Inject]
        private void Construct(CommandsFactory commandsFactory, EntityManager entityManager)
        {
            _entityManager = entityManager;
            _commandsFactory = commandsFactory;
        }

        private void OnDisable()
            => DropCrystal();

        public void SetCrystalToSlot(Crystal crystal)
        {
            Assert.IsNull(_activeCommand);
            crystal.TurnPhysicsOff();
            _activeCommand =
                _commandsFactory.GetFlyObjectToCommand(crystal.transform, transform, _flySpeed, _completionRadius);
            _sub = _activeCommand.OnCompleted.Subscribe(OnCrystalArrive);
            _crystal = crystal;
            _activeCommand.Start();
        }

        public void DropCrystal()
        {
            if (_crystal == null)
                return;
            if (_activeCommand != null)
            {
                _activeCommand.Interrupt();
                _sub!.Dispose();
                _activeCommand = null;
            }

            _crystal.TurnPhysicsOn();
            _crystal = null!;
            _isFilled.Value = false;
        }

        public void DestroyCrystal()
        {
            if (_crystal == null)
                return;
            _entityManager.DestroyObject(_crystal);
            if (_activeCommand != null)
            {
                _activeCommand.Interrupt();
                _sub!.Dispose();
                _activeCommand = null;
            }

            _isFilled.Value = false;
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