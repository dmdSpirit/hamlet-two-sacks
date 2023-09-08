#nullable enable

namespace HamletTwoSacks.Character
{
    public interface IPlayerFactory
    {
        PlayerBehaviour CreatePlayer();
    }
}