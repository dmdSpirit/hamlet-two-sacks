#nullable enable

using System;
using HamletTwoSacks.Character;
using HamletTwoSacks.Infrastructure;
using HamletTwoSacks.Infrastructure.Time;
using HamletTwoSacks.Input;
using HamletTwoSacks.Physics;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalPayer : MonoBehaviour
    {
        private Player _player = null!;
        private ActionButtonReader _actionButtonReader = null!;

        private IDisposable? _sub;
        private IDisposable? _timerSub;
        private string _stringID = null!;
        private CrystalCostPanel? _costPanel;
        private RepeatingTimer _timer = null!;

        [SerializeField]
        private CrystalSpawner _crystalSpawner = null!;

        [SerializeField]
        private TriggerDetector _triggerDetector = null!;

        [SerializeField]
        private float _crystalSpawnCooldown = 0.1f;

        [Inject]
        private void Construct(Player player, ActionButtonReader actionButtonReader, TimeController timeController)
        {
            _actionButtonReader = actionButtonReader;
            _player = player;
            _stringID = this.ToStringID();
            _timer = new RepeatingTimer(timeController);
        }

        private void Awake()
        {
            _triggerDetector.OnTriggerEnter.Subscribe(TriggerEnter);
            _triggerDetector.OnTriggerExit.Subscribe(TriggerExit);
            _timer.OnFire.Subscribe(OnSpendCrystal);
            _timer.SetCooldown(_crystalSpawnCooldown);
        }

        private void TriggerEnter(Collider2D target)
        {
            if (_costPanel != null)
                return;

            var systems = target.gameObject.GetComponent<SystemReferences>();
            if (systems == null)
                return;
            _costPanel = systems.GetSystem<CrystalCostPanel>();
            if (_costPanel == null
                || !_costPanel.IsEnabled)
                return;
            _sub?.Dispose();
            _costPanel.ShowPanel();
            _actionButtonReader.SubscribeToAction(_stringID);
            _sub = _actionButtonReader.OnStateChanged.Subscribe(UpdateButtonReading);
        }

        private void TriggerExit(Collider2D target)
        {
            if (_costPanel == null)
                return;

            var systems = target.gameObject.GetComponent<SystemReferences>();
            if (systems == null)
                return;
            if (_costPanel != systems.GetSystem<CrystalCostPanel>())
                return;

            _costPanel.HidePanel();
            _sub?.Dispose();
            _actionButtonReader.UnsubscribeFromAction(_stringID);
            _costPanel = null;
            _timer.Stop();
        }

        private void UpdateButtonReading(Unit _)
        {
            bool isPressed = _actionButtonReader.IsActive.Value && _actionButtonReader.IsActionPressed.Value
                             && string.Equals(_actionButtonReader.CurrentReceiver, _stringID);
            if (isPressed && !_timer.IsRunning)
            {
                _timer.Start();
            }
            else if (!isPressed
                     && _timer.IsRunning)
            {
                _timer.Stop();
                if (_costPanel != null)
                    _costPanel.DropCrystals();
            }
        }

        private Crystal? GetCrystal()
        {
            if (_player.Crystals.Value <= 0)
                return null;
            Crystal crystal = _crystalSpawner.SpawnCrystal();
            _player.SpendCrystal();
            return crystal;
        }

        private void OnSpendCrystal(RepeatingTimer _)
        {
            CrystalSlot? slot = _costPanel!.GetEmptyCrystalSlot();
            if (slot == null)
                return;
            Crystal? crystal = GetCrystal();
            if (crystal == null)
                return;
            slot.SetCrystalToSlot(crystal);
        }
    }
}