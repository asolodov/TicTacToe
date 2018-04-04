using System.Collections.Generic;
using TicTacToe.BL.Models;

namespace TicTacToe.BL.Interfaces
{
    public interface IGameInstance
    {
        IEnumerable<string> UserIds { get; }

        void StartGame();
        void StopGame();
        void HandleUserMessage(User fromUser, BaseMessage message);
    }
}
