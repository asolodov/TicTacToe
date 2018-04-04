using TicTacToe.BL.Models;

namespace TicTacToe.BL.Interfaces
{
    public interface IGameInstanceStorage
    {
        void AddGameInstance(IGameInstance instance);
        void RemoveGameInstance(IGameInstance instance);
        IGameInstance GetGameInstanceByUser(User user);
    }
}
