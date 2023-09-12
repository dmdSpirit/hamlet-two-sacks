#nullable enable

using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Crystals.UI
{
    public sealed class CrystalCostPanel : MonoBehaviour
    {
        private readonly Subject<CrystalCostPanel> _onPricePayed = new();

        private int _payed;
        private int _cost;

        [SerializeField]
        private GameObject _panel = null!;

        [SerializeField]
        private GameObject _tooltipPanel = null!;

        [SerializeField]
        private CrystalSlot[] _slots = null!;

        public IObservable<CrystalCostPanel> OnPricePayed => _onPricePayed;
        public bool IsEnabled { get; private set; }

        private void Awake()
        {
            foreach (CrystalSlot crystalSlot in _slots)
                crystalSlot.IsFilled.Subscribe(OnSlotsUpdate);
            _panel.SetActive(false);
            _tooltipPanel.SetActive(false);
        }

        public void SetCost(int cost)
        {
            _cost = cost;
            for (var i = 0; i < _slots.Length; i++)
                _slots[i].gameObject.SetActive(i < cost);
        }

        public void ShowPanel()
        {
            if (!IsEnabled)
                return;
            _panel.SetActive(true);
            _tooltipPanel.SetActive(true);
        }

        public void HidePanel()
        {
            DropCrystals();
            _panel.SetActive(false);
            _tooltipPanel.SetActive(false);
        }

        public void DropCrystals()
        {
            foreach (CrystalSlot crystalSlot in _slots)
                crystalSlot.DropCrystal();
        }

        public CrystalSlot? GetEmptyCrystalSlot()
            => _slots.FirstOrDefault(slot => slot.gameObject.activeInHierarchy && !slot.IsFilled.Value
                                             && !slot.IsCrystalFlying);

        public void Enable()
            => IsEnabled = true;

        public void Disable()
        {
            HidePanel();
            IsEnabled = false;
        }

        private void OnSlotsUpdate(bool _)
        {
            _payed = _slots.Count(slot => slot.gameObject.activeInHierarchy && slot.IsFilled.Value);

            if (_payed != _cost)
                return;

            foreach (CrystalSlot crystalSlot in _slots)
                crystalSlot.DestroyCrystal();

            _onPricePayed.OnNext(this);
        }
    }
}