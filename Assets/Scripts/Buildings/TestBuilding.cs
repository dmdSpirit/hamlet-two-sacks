#nullable enable
using HamletTwoSacks.Crystals;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Buildings
{
    public sealed class TestBuilding : MonoBehaviour
    {
        [SerializeField]
        private CrystalCostPanel _crystalCostPanel = null!;

        [SerializeField]
        private int _price = 3;

        private void Awake()
        {
            _crystalCostPanel.OnPricePayed.Subscribe(OnPricePayed);
            _crystalCostPanel.SetCost(_price);
        }

        private void Start()
            => _crystalCostPanel.Enable();

        private void OnPricePayed(CrystalCostPanel _)
        {
            Debug.Log($"Price for {name} was payed");
            _crystalCostPanel.HidePanel();
        }
    }
}