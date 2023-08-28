#nullable enable

using UnityEditor.SceneManagement;
using UnityEngine;

namespace dmdspirit.Editor.Core.Editor
{
    public class MultiSceneSetup : ScriptableObject
    {
        public SceneSetup[]? Setups;
    }
}