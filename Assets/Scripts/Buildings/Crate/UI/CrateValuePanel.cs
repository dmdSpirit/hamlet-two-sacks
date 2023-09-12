#nullable enable

using TMPro;
using UniRx;
using UnityEngine;

namespace HamletTwoSacks.Buildings.Crate.UI
{
    public sealed class CrateValuePanel : MonoBehaviour
    {
        private readonly CompositeDisposable _sub = new();

        [SerializeField]
        private Crate _crate = null!;

        [SerializeField]
        private GameObject _panel = null!;

        [SerializeField]
        private TMP_Text _value = null!;

        private void Awake()
        {
            _crate.OnBuildingUpgraded.Subscribe(_ => OnUpdate()).AddTo(_sub);
            _crate.Crystals.Subscribe(_ => OnUpdate()).AddTo(_sub);
        }

        private void OnDestroy()
            => _sub.Dispose();

        private void OnUpdate()
        {
            if (!_crate.IsActive)
            {
                _panel.SetActive(false);
                return;
            }

            if (!_panel.activeInHierarchy)
                _panel.SetActive(true);
            _value.text = $"{_crate.Crystals.Value}/{_crate.Capacity}";
        }
    }
}