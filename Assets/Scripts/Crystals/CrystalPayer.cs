#nullable enable

using System;
using HamletTwoSacks.Character;
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

        private CrystalCostPanel? _costPanel;
        private ActionReceiver? _actionReceiver;

        [SerializeField]
        private CrystalSpawner _crystalSpawner = null!;

        [SerializeField]
        private TriggerDetector _triggerDetector = null!;

        // TODO (Stas): Move to localization system.
        // - Stas 09 September 2023
        [SerializeField]
        private string _callToAction = null!;

        [Inject]
        private void Construct(Player player, ActionButtonReader actionButtonReader)
        {
            _actionButtonReader = actionButtonReader;
            _player = player;
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

            _costPanel.ShowPanel();
            if (_actionReceiver != null)
                _actionButtonReader.UnsubscribeFromAction(_actionReceiver);
            _actionReceiver = new ActionReceiver(() => OnSpendCrystal(_costPanel), _callToAction);
            _actionButtonReader.SubscribeToAction(_actionReceiver);
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

            if (_actionReceiver != null)
                _actionButtonReader.UnsubscribeFromAction(_actionReceiver);
            _costPanel = null;
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