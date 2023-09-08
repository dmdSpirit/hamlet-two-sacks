#nullable enable

using System;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalCostPanel : MonoBehaviour
    {
        private readonly Subject<CrystalCostPanel> _onPricePayed = new();
        private int _payed;

        [SerializeField]
        private CrystalSlot[] _slots = null!;

        public IObservable<CrystalCostPanel> OnPricePayed => _onPricePayed;
        public int Cost { get; private set; }

        private void Awake()
        {
            foreach (CrystalSlot crystalSlot in _slots)
                crystalSlot.IsFilled.Subscribe(OnSlotsUpdate);
        }

        public void SetCost(int cost)
        {
            Cost = cost;
            for (var i = 0; i < _slots.Length; i++)
                _slots[i].gameObject.SetActive(i < cost);
        }

        public void ShowPanel()
            => gameObject.SetActive(true);

        public void HidePanel()
        {
            foreach (CrystalSlot crystalSlot in _slots)
                crystalSlot.DestroyCrystal();
            gameObject.SetActive(false);
        }

        private void OnSlotsUpdate(bool _)
        {
            _payed = 0;
            foreach (CrystalSlot crystalSlot in _slots)
            {
                if (!crystalSlot.gameObject.activeInHierarchy)
                    continue;
                if (crystalSlot.IsFilled.Value)
                    _payed++;
            }

            if (_payed != Cost)
                return;

            _onPricePayed.OnNext(this);
        }

    }
}