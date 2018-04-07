using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.BL.GameInstance.Interfaces;
using TicTacToe.BL.GameInstance.Models;
using TicTacToe.BL.Users.Interfaces;
using TicTacToe.BL.Users.Models;
using TicTacToe.BL.Users.Models.Messages;

namespace TicTacToe.BL.GameInstance
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

        public IEnumerable<string> UserIds
        {
            get
            {
                yield return PlayerOne.ConnectionId;
                yield return PlayerTwo.ConnectionId;
            }
        }

        public async Task StartGame()
        {
            await _userCommunicationService.SendGameStartedMessage(PlayerOne.ConnectionId, new GameStartedMessage(PlayerOne.PlayerCell));
            await _userCommunicationService.SendGameStartedMessage(PlayerTwo.ConnectionId, new GameStartedMessage(PlayerTwo.PlayerCell));
        }

        public async Task StopGame()
        {
        }

        public async Task HandleUserMessage(User fromUser, BaseMessage message)
        {
        }
    }
}
