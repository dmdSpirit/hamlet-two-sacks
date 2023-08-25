#nullable enable

using UnityEngine;

namespace dmdspirit.Core.UI.Skins
{
    public abstract class SkinReactor : MonoBehaviour
    {
        public abstract void ActivateSkin(int skinID);
    }
}