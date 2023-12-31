﻿#nullable enable

using System;
using HamletTwoSacks.Characters.PlayerControl;
using HamletTwoSacks.Crystals.UI;
using HamletTwoSacks.Infrastructure;
using HamletTwoSacks.Input;
using HamletTwoSacks.Physics;
using HamletTwoSacks.Time;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalPayer : MonoBehaviour
    {
        private const InputActionType ACTION_TYPE = InputActionType.Pay;

        private Player _player = null!;
        private IActionButtonsReader _actionButtonsReader = null!;

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
        private void Construct(Player player, IActionButtonsReader actionButtonsReader, TimeController timeController)
        {
            _actionButtonsReader = actionButtonsReader;
            _player = player;
            _stringID = this.ToStringID(true);
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
            _actionButtonsReader.SubscribeToAction(_stringID, ACTION_TYPE);
            _sub = _actionButtonsReader.OnStateChanged.Where(type => type == ACTION_TYPE)
                .Subscribe(UpdateButtonReading);
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
            _actionButtonsReader.UnsubscribeFromAction(_stringID, ACTION_TYPE);
            _costPanel = null;
            _timer.Stop();
        }

        private void UpdateButtonReading(InputActionType _)
        {
            bool isPressed = _actionButtonsReader.IsActive.Value && _actionButtonsReader.IsPressed(ACTION_TYPE)
                             && string.Equals(_actionButtonsReader.CurrentReceiver(ACTION_TYPE), _stringID);
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
            Crystal crystal = _crystalSpawner.Spawn();
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