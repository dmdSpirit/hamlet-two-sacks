#nullable enable

namespace HamletTwoSacks.Characters.PlayerControl
{
    public interface IPlayerFactory
    {
        PlayerBehaviour CreatePlayer();
    }
}