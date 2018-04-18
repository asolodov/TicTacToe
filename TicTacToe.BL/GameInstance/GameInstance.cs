using System;
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
        public CurrentActivePlayer CurrentActivePlayer { get; private set; }

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
            CurrentActivePlayer = CurrentActivePlayer.PlayerOne;
            await _userCommunicationService.SendMessageToUser(PlayerOne.ConnectionId, new GameStartedMessage(PlayerOne.PlayerCell, true));
            await _userCommunicationService.SendMessageToUser(PlayerTwo.ConnectionId, new GameStartedMessage(PlayerTwo.PlayerCell, false));
        }

        public async Task StopGame()
        {

        }

        public async Task HandlePlayerActionMessage(User fromUser, PlayerActionMessage action)
        {
            if (fromUser == null)
                throw new ArgumentNullException(nameof(fromUser));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            switch (CurrentActivePlayer)
            {
                case CurrentActivePlayer.PlayerOne:
                    if (PlayerOne.ConnectionId == fromUser.ConnectionId)
                    {
                        await _userCommunicationService.SendMessageToUser(PlayerTwo.ConnectionId, action);
                        CurrentActivePlayer = CurrentActivePlayer.PlayerTwo;
                    }
                    break;
                case CurrentActivePlayer.PlayerTwo:
                    if (PlayerTwo.ConnectionId == fromUser.ConnectionId)
                    {
                        await _userCommunicationService.SendMessageToUser(PlayerOne.ConnectionId, action);
                        CurrentActivePlayer = CurrentActivePlayer.PlayerOne;
                    }
                    break;
            }
        }
    }
}
