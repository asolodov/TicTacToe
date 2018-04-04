using TicTacToe.BL.Models;

namespace TicTacToe.BL.Interfaces
{
    public interface IActiveGameInstanceManager
    {
        void AddGameInstance(IGameInstance instance);
        void RemoveGameInstance(IGameInstance instance);
        IGameInstance GetGameInstanceByUser(User user);
    }
}
