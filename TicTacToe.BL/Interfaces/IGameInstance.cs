using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.BL.Models;

namespace TicTacToe.BL.Interfaces
{
    public interface IGameInstance
    {
        IEnumerable<string> UserIds { get; }

        Task StartGame();
        Task StopGame();
        Task HandleUserMessage(User fromUser, BaseMessage message);
    }
}
