using TicTacToe.BL.Users.Models;

namespace TicTacToe.BL.GameInstance.Interfaces
{
    public interface IGameInstanceFactory
    {
        IGameInstance CreateGameInstance(User userOne, User userTwo);
    }
}
