#nullable enable

using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace dmdspirit.Core.UI.Skins
{
    public sealed class LocalizedTextReactor : SkinReactor
    {
        [SerializeField]
        private LocalizeStringEvent _target = null!;

        [SerializeField]
        private LocalizedString[] _localizedStrings = null!;

        public override void ActivateSkin(int skinID)
        {
            if (skinID > _localizedStrings.Length)
            {
                Debug.LogError($"Not enough localized strings for {nameof(LocalizedTextReactor)}");
                return;
            }

            _target.StringReference = _localizedStrings[skinID];
        }
    }
}