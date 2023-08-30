#nullable enable

using UnityEngine;

namespace HamletTwoSacks.Crystals
{
    public interface ICrystalFactory
    {
        Crystal SpawnCrystalAt(Transform spawnPoint);
    }
}