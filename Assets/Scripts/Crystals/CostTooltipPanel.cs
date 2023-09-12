#nullable enable
using HamletTwoSacks.Infrastructure.StaticData;
using HamletTwoSacks.Input;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    public sealed class CostTooltipPanel : MonoBehaviour
    {
        // TODO (Stas): Localize this.
        // - Stas 12 September 2023
        [SerializeField]
        private TMP_Text _text = null!;

        [Inject]
        private void Construct(StaticDataProvider staticDataProvider)
        {
            var inputConfig = staticDataProvider.GetConfig<InputConfig>();
            InputAction payAction = inputConfig.PayAction;
            _text.text = _text.text.Replace("[value]", payAction.GetBindingDisplayString());
        }
    }
}