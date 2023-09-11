#nullable enable

using System;
using HamletTwoSacks.Character;
using HamletTwoSacks.Infrastructure;
using HamletTwoSacks.Input;
using HamletTwoSacks.Physics;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalPayer : MonoBehaviour
    {
        private Player _player = null!;
        private ActionButtonReader _actionButtonReader = null!;
        private TimeController _timeController = null!;

        private readonly CompositeDisposable _sub = new();

        private string _stringID = null!;
        private CrystalCostPanel? _costPanel;
        private IDisposable? _timeSub;

        [SerializeField]
        private CrystalSpawner _crystalSpawner = null!;

        [SerializeField]
        private TriggerDetector _triggerDetector = null!;

        [Inject]
        private void Construct(Player player, ActionButtonReader actionButtonReader, TimeController timeController)
        {
            _timeController = timeController;
            _actionButtonReader = actionButtonReader;
            _player = player;
            _stringID = this.ToStringID();
        }

        private void Awake()
        {
            _triggerDetector.OnTriggerEnter.Subscribe(TriggerEnter);
            _triggerDetector.OnTriggerExit.Subscribe(TriggerExit);
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
            _sub.Clear();
            _timeSub?.Dispose();
            _costPanel.ShowPanel();
            _actionButtonReader.IsActive.Subscribe(UpdateButtonReading).AddTo(_sub);
            _actionButtonReader.IsActionPressed.Subscribe(ButtonStateUpdate).AddTo(_sub);
            _actionButtonReader.SubscribeToAction(_stringID);
        }

        private void UpdateButtonReading(bool isReadingActive)
        {
            _timeSub?.Dispose();
            if (isReadingActive == false)
                return;
            _timeSub = _timeController.Update.Subscribe(OnUpdate);
        }

        private void ButtonStateUpdate(bool isButtonPressed) { }

        private void OnUpdate(float time)
        {
            Assert.IsNotNull(_costPanel);
            if (_actionButtonReader.IsActionPressed.Value)
                OnSpendCrystal(_costPanel!);
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
            _sub.Clear();
            _actionButtonReader.UnsubscribeFromAction(_stringID);
            _costPanel = null;
            _timeSub?.Dispose();
        }

        private Crystal? GetCrystal()
        {
            if (_player.Crystals.Value <= 0)
                return null;
            Crystal crystal = _crystalSpawner.SpawnCrystal();
            _player.SpendCrystal();
            return crystal;
        }

        private void OnSpendCrystal(CrystalCostPanel costPanel)
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