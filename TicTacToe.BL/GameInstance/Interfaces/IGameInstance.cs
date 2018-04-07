using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.BL.Users.Models;
using TicTacToe.BL.Users.Models.Messages;

namespace TicTacToe.BL.GameInstance.Interfaces
{
    public interface IGameInstance
    {
        IEnumerable<string> UserIds { get; }

        Task StartGame();
        Task StopGame();
        Task HandleUserMessage(User fromUser, BaseMessage message);
    }
}
