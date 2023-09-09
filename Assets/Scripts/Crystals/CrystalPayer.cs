#nullable enable

using HamletTwoSacks.Character;
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

        private void OnCollisionEnter2D(Collision2D target)
        {
            if (_costPanel != null)
                return;

            _costPanel = target.gameObject.GetComponent<CrystalCostPanel>();
            if (_costPanel == null)
                return;

            _costPanel.ShowPanel();
            if (_actionReceiver != null)
                _actionButtonReader.UnsubscribeFromAction(_actionReceiver);
            _actionReceiver = new ActionReceiver(() => OnSpendCrystal(_costPanel), _callToAction);
            _actionButtonReader.SubscribeToAction(_actionReceiver);
        }

        private void OnCollisionExit2D(Collision2D target)
        {
            if (_costPanel == null)
                return;

            _costPanel.HidePanel();

            if (_actionReceiver != null)
                _actionButtonReader.UnsubscribeFromAction(_actionReceiver);
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