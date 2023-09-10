#nullable enable

namespace HamletTwoSacks.Crystals
{
    public interface ICrystalFactory
    {
        Crystal SpawnCrystal();
        void DestroyCrystal(Crystal crystal);
    }
}