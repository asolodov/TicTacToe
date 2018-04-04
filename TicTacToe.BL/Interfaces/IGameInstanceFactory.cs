using TicTacToe.BL.Models;

namespace TicTacToe.BL.Interfaces
{
    public interface IGameInstanceFactory
    {
        IGameInstance CreateGameInstance(User userOne, User userTwo);
    }
}
