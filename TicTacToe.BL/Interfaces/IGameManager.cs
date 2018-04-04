using TicTacToe.BL.Models;

namespace TicTacToe.BL.Interfaces
{
    public interface IGameManager
    {
        void ConnectUser(string connectionId);
        void DisconnectUser(string connectionId);
        void HandleUserMessage<TMessage>(string connectionId, TMessage message) where TMessage: BaseMessage;
    }
}
