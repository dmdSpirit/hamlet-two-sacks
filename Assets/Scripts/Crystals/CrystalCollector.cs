#nullable enable

using System;
using System.Collections.Generic;
using dmdspirit.Core.CommonInterfaces;
using HamletTwoSacks.Commands;
using HamletTwoSacks.Physics;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalCollector : MonoBehaviour, IActivatable
    {
        private CommandsFactory _commandsFactory = null!;

        private readonly Subject<Unit> _onCrystalCollected = new();
        private readonly List<ICommand> _activeCommands = new();
        private readonly CompositeDisposable _subs = new();

        private CrystalsManager _crystalsManager = null!;
        private Func<bool> _canCollect = null!;

        [SerializeField]
        private bool _isActiveFromStart;

        [SerializeField]
        private Transform _destinationTransform = null!;

        [SerializeField]
        private float _collectionSpeed;

        [SerializeField]
        private float _completionRadius;

        [SerializeField]
        private TriggerDetector _triggerDetector = null!;

        public IObservable<Unit> OnCrystalCollected => _onCrystalCollected;
        public bool IsActive { get; private set; }
        public int ActiveCommands => _activeCommands.Count;

        [Inject]
        private void Construct(CommandsFactory commandsFactory, CrystalsManager crystalsManager)
        {
            _crystalsManager = crystalsManager;
            _commandsFactory = commandsFactory;
            _canCollect = CanCollect;
        }

        private void Awake()
            => _triggerDetector.OnTriggerEnter.Subscribe(TriggerEnter);

        private void Start()
        {
            if (_isActiveFromStart)
                Activate();
        }

        private void OnDestroy()
            => StopCommands();

        public void SetCollectionCheck(Func<bool> canCollect)
            => _canCollect = canCollect;

        public void Activate()
        {
            if (IsActive)
                return;
            IsActive = true;
        }

        public void Deactivate()
        {
            StopCommands();
            IsActive = false;
        }

        private void TriggerEnter(Collider2D target)
        {
            if (!IsActive
                || !_canCollect.Invoke())
                return;
            var crystal = target.gameObject.GetComponent<Crystal>();
            if (crystal == null)
                return;
            crystal.TurnPhysicsOff();
            FlyObjectToCommand flyCommand =
                _commandsFactory.GetFlyObjectToCommand(target.transform, _destinationTransform, _collectionSpeed,
                                                       _completionRadius);
            flyCommand.OnCompleted.Subscribe(OnFlyEnded).AddTo(_subs);
            flyCommand.Start();
            _activeCommands.Add(flyCommand);
        }

        private void OnFlyEnded(ICommand command)
        {
            var crystal = ((FlyObjectToCommand)command).Target.GetComponent<Crystal>();
            _crystalsManager.DestroyCrystal(crystal);
            _activeCommands.Remove(command);
            _onCrystalCollected.OnNext(Unit.Default);
        }

        private void StopCommands()
        {
            foreach (ICommand command in _activeCommands)
                command.Interrupt();
            _activeCommands.Clear();
            _subs.Clear();
        }

        private bool CanCollect()
            => true;
    }
}