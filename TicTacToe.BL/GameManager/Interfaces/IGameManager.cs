using System.Threading.Tasks;
using TicTacToe.BL.Users.Models.Messages;

namespace TicTacToe.BL.GameManager.Interfaces
{
    public interface IGameManager
    {
        Task ConnectUser(string connectionId);
        void DisconnectUser(string connectionId);
        void HandleUserMessage<TMessage>(string connectionId, TMessage message) where TMessage: BaseMessage;
    }
}
