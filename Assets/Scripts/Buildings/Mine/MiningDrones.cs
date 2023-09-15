#nullable enable
using UnityEngine;

namespace HamletTwoSacks.Buildings.Mine
{
    public sealed class MiningDrones : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _drones = null!;

        public void ShowDrones(int dronesNumber)
        {
            for (var i = 0; i < _drones.Length; i++)
                _drones[i].SetActive(i < dronesNumber);
        }

        public void HideAll()
        {
            foreach (GameObject drone in _drones)
                drone.SetActive(false);
        }
    }
}