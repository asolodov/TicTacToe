using System.Collections.Generic;
using TicTacToe.BL.Interfaces;

namespace TicTacToe.BL.Models
{
    public class GameInstance : IGameInstance
    {
        private readonly IUserCommunicationService _userCommunicationService;

        public GameInstance(IUserCommunicationService userCommunicationService)
        {
            _userCommunicationService = userCommunicationService;
        }

        public Player PlayerOne { get; internal set; }

        public Player PlayerTwo { get; internal set; }

        public GameState GameState { get; private set; }

        public IEnumerable<string> UserIds
        {
            get
            {
                yield return PlayerOne.ConnectionId;
                yield return PlayerTwo.ConnectionId;
            }
        }

        public void StartGame()
        {
        }

        public void StopGame()
        {
        }

        public void HandleUserMessage(User fromUser, BaseMessage message)
        {
        }
    }
}
