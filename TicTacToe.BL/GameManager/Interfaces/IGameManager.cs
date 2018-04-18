using System.Threading.Tasks;
using TicTacToe.BL.Users.Models.Messages;

namespace TicTacToe.BL.GameManager.Interfaces
{
    public interface IGameManager
    {
        Task ConnectUser(string connectionId);
        void DisconnectUser(string connectionId);
        Task HandlePlayerActionMessage(string fromUserId, PlayerActionMessage action);
    }
}
