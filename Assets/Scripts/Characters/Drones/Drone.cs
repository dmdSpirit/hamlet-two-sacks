#nullable enable

using HamletTwoSacks.AI;
using UnityEngine;

namespace HamletTwoSacks.Characters.Drones
{
    public sealed class Drone : MonoBehaviour
    {
        [SerializeField]
        private CrystalContainer _crystalContainer = null!;

        [SerializeField]
        private int _crystalCapacity = 2;

        private void Awake()
            => _crystalContainer.SetCapacity(_crystalCapacity);
    }
}