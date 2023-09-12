#nullable enable
using HamletTwoSacks.Physics;
using UnityEngine;

namespace HamletTwoSacks.Buildings
{
    public sealed class Crate : MonoBehaviour
    {
        [SerializeField]
        private TriggerDetector _triggerDetector = null!;
    }
}