#nullable enable

using UnityEngine;

namespace HamletTwoSacks.Crystals
{
    public interface ICrystalFactory
    {
        Crystal SpawnCrystal();
    }
}