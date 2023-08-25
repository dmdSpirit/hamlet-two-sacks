#nullable enable

using System;
using UnityEngine;

namespace dmdspirit.Core.UI.Skins
{
    public sealed class CanvasSortOrderSkinReactor : SkinReactor
    {
        [Serializable]
        public sealed class SortOptions
        {
            public bool Override;
            public int SortOrder;
        }

        [SerializeField]
        private Canvas _target = null!;

        [SerializeField]
        private SortOptions[] _sortOptions = null!;

        public override void ActivateSkin(int skinID)
        {
            if (skinID > _sortOptions.Length)
            {
                Debug.LogError($"Not enough sort options for {nameof(CanvasSortOrderSkinReactor)}");
                return;
            }

            bool shouldOverride = _sortOptions[skinID].Override;
            int sortOrder = _sortOptions[skinID].SortOrder;
            if (!_target.overrideSorting
                && !shouldOverride)
                return;
            if (_target.overrideSorting != shouldOverride)
                _target.overrideSorting = shouldOverride;
            if (_target.sortingOrder != sortOrder)
                _target.sortingOrder = sortOrder;
        }
    }
}