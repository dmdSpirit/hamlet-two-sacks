#nullable enable

using HamletTwoSacks.AI;
using HamletTwoSacks.Crystals;
using TMPro;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Buildings.Crate.UI
{
    public sealed class CrystalContainerValuePanel : MonoBehaviour
    {
        private readonly CompositeDisposable _sub = new();

        [SerializeField]
        private CrystalContainer _crystalContainer = null!;

        [SerializeField]
        private bool _showIfEmpty;

        [SerializeField]
        private GameObject _panel = null!;

        [SerializeField]
        private TMP_Text _value = null!;

        private void Awake()
        {
            _crystalContainer.Capacity.Subscribe(OnUpdate).AddTo(_sub);
            _crystalContainer.Crystals.Subscribe(OnUpdate).AddTo(_sub);
        }

        private void OnDestroy()
            => _sub.Dispose();

        private void OnUpdate(int _)
        {
            if (_crystalContainer.Crystals.Value == 0
                && !_showIfEmpty)
            {
                _panel.SetActive(false);
                return;
            }

            if (!_panel.activeInHierarchy)
                _panel.SetActive(true);
            _value.text = $"{_crystalContainer.Crystals.Value}/{_crystalContainer.Capacity}";
        }
    }
}