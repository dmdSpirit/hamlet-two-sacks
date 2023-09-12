#nullable enable

using System;
using System.Collections.Generic;
using HamletTwoSacks.Commands;
using HamletTwoSacks.Physics;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalCollector : MonoBehaviour
    {
        private CommandsFactory _commandsFactory = null!;
        private ICrystalFactory _crystalFactory = null!;

        private readonly Subject<Unit> _onCrystalCollected = new();
        private readonly List<ICommand> _activeCommands = new();
        private readonly CompositeDisposable _subs = new();

        [SerializeField]
        private Transform _bagTransform = null!;

        [SerializeField]
        private float _collectionSpeed;

        [SerializeField]
        private float _completionRadius;

        [SerializeField]
        private TriggerDetector _triggerDetector = null!;

        public IObservable<Unit> OnCrystalCollected => _onCrystalCollected;

        [Inject]
        private void Construct(CommandsFactory commandsFactory, ICrystalFactory crystalFactory)
        {
            _crystalFactory = crystalFactory;
            _commandsFactory = commandsFactory;
        }

        private void Awake()
            => _triggerDetector.OnTriggerEnter.Subscribe(TriggerEnter);

        private void TriggerEnter(Collider2D target)
        {
            var crystal = target.gameObject.GetComponent<Crystal>();
            if (crystal == null)
                return;
            crystal.TurnPhysicsOff();
            FlyObjectToCommand flyCommand =
                _commandsFactory.GetFlyObjectToCommand(target.transform, _bagTransform, _collectionSpeed,
                                                       _completionRadius);
            flyCommand.OnCompleted.Subscribe(OnFlyEnded).AddTo(_subs);
            flyCommand.Start();
            _activeCommands.Add(flyCommand);
        }

        private void OnDestroy()
        {
            foreach (ICommand command in _activeCommands)
                command.Interrupt();
            _activeCommands.Clear();
            _subs.Clear();
        }

        private void OnFlyEnded(ICommand command)
        {
            var crystal = ((FlyObjectToCommand)command).Target.GetComponent<Crystal>();
            _crystalFactory.DestroyCrystal(crystal);
            _activeCommands.Remove(command);
            _onCrystalCollected.OnNext(Unit.Default);
        }
    }
}