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
        private Player _currentActivePlayer;

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
            UpdateCurrentActivePlayer();

            await _userCommunicationService.SendMessageToUser(PlayerOne.ConnectionId, new GameStartedMessage(PlayerOne.PlayerCell, IsPlayerActive(PlayerOne)));
            await _userCommunicationService.SendMessageToUser(PlayerTwo.ConnectionId, new GameStartedMessage(PlayerTwo.PlayerCell, IsPlayerActive(PlayerTwo)));
        }

        public async Task StopGame()
        {
            await _userCommunicationService.SendMessageToUser(PlayerOne.ConnectionId, new GameStoppedMessage());
            await _userCommunicationService.SendMessageToUser(PlayerTwo.ConnectionId, new GameStoppedMessage());
        }

        public async Task HandlePlayerActionMessage(User fromUser, PlayerActionMessage action)
        {
            if (fromUser == null)
                throw new ArgumentNullException(nameof(fromUser));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if ((IsPlayerActive(PlayerOne) && PlayerOne.ConnectionId == fromUser.ConnectionId) ||
                (IsPlayerActive(PlayerTwo) && PlayerTwo.ConnectionId == fromUser.ConnectionId))
            {
                UpdateCurrentActivePlayer();

                await _userCommunicationService.SendMessageToUser(PlayerOne.ConnectionId, new StateUpdateMessage(action, IsPlayerActive(PlayerOne)));
                await _userCommunicationService.SendMessageToUser(PlayerTwo.ConnectionId, new StateUpdateMessage(action, IsPlayerActive(PlayerTwo)));
            }
        }

        private void UpdateCurrentActivePlayer()
        {
            _currentActivePlayer = _currentActivePlayer == PlayerOne ? PlayerTwo : PlayerOne;
        }

        private bool IsPlayerActive(Player player)
        {
            return _currentActivePlayer == player;
        }
    }
}
