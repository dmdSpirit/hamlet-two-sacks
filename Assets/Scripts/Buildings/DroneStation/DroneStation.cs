#nullable enable

using System.Collections.Generic;
using HamletTwoSacks.AI;
using HamletTwoSacks.AI.SimpleBrain;
using HamletTwoSacks.Buildings.DroneStation.Config;
using HamletTwoSacks.Characters.Drones;
using HamletTwoSacks.Commands;
using HamletTwoSacks.Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Buildings.DroneStation
{
    public sealed class DroneStation : Building<DroneStationConfig, DroneStationTier>
    {
        private CommandsFactory _commandsFactory = null!;
        private IPrefabFactory _prefabFactory = null!;

        private readonly List<Drone> _drones = new();
        private readonly List<ICommand> _activeCommands = new();
        private readonly CompositeDisposable _commandSubs = new();

        [SerializeField]
        private DroneSpawner _droneSpawner = null!;

        [SerializeField]
        private Brain _brainTemplate = null!;

        [SerializeField]
        private Transform _releasePoint = null!;

        [SerializeField]
        private float _droneTransportSpeed = 1f;

        [Inject]
        private void Construct(CommandsFactory commandsFactory, IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
            _commandsFactory = commandsFactory;
        }

        protected override void OnStart()
        {
            if (!CurrentTier.IsActive)
                return;
            SpawnDrones();
        }

        protected override void OnUpgraded()
        {
            if (!CurrentTier.IsActive)
                return;
            SpawnDrone();
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            foreach (ICommand activeCommand in _activeCommands)
                activeCommand.Interrupt();
            _commandSubs.Dispose();
        }

        private void SpawnDrones()
        {
            for (int i = _drones.Count; i < CurrentTier.Drones; i++)
                SpawnDrone();
        }

        private void SpawnDrone()
        {
            Drone drone = _droneSpawner.Spawn();
            Brain brain = _prefabFactory.CreateCopyObject(_brainTemplate);
            brain.transform.SetParent(drone.transform);
            drone.SetBrain(brain);
            _drones.Add(drone);
            FlyObjectToCommand command =
                _commandsFactory.GetFlyObjectToCommand(drone.transform, _releasePoint, _droneTransportSpeed, 0.01f);
            command.OnCompleted.Subscribe(ActivateDrone).AddTo(_commandSubs);
            _activeCommands.Add(command);
            command.Start();
        }

        private void ActivateDrone(ICommand command)
        {
            _activeCommands.Remove(command);
            var drone = ((FlyObjectToCommand)command).Target.GetComponent<Drone>();
            drone.InitializeBrain();
            drone.Activate();
        }
    }
}