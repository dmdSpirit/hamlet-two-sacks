#nullable enable
using HamletTwoSacks.Infrastructure.StaticData;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HamletTwoSacks.Input
{
    [CreateAssetMenu(menuName = "Config/Input", fileName = "input config", order = 0)]
    public sealed class InputConfig : GameConfig
    {
        [SerializeField]
        private InputAction _action = null!;

        public InputAction Action => _action;
    }
}