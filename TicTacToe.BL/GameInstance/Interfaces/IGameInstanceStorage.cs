using TicTacToe.BL.Users.Models;

namespace TicTacToe.BL.GameInstance.Interfaces
{
    public interface IGameInstanceStorage
    {
        void AddGameInstance(IGameInstance instance);
        void RemoveGameInstance(IGameInstance instance);
        IGameInstance GetGameInstanceByUser(User user);
    }
}
