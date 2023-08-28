#nullable enable
namespace HamletTwoSacks.Character
{
    public interface ICharactersManager
    {
        Player? Player { get; }
        void SpawnPlayer();
    }
}