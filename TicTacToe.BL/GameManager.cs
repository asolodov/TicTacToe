using System.Collections.Generic;
using System.Linq;
using TicTacToe.BL.Interfaces;
using TicTacToe.BL.Models;

namespace TicTacToe.BL
{
    public class GameManager : IGameManager
    {
        private User _awaitableUser;

        private readonly IUserCommunicationService _userCommunicationService;
        private readonly IUserStorage _userStorage;
        private readonly IActiveGameInstanceManager _gameInstanceManager;
        private readonly IGameInstanceFactory _gameInstanceFactory;

        public GameManager(
            IUserCommunicationService userCommunicationService,
            IUserStorage userStorage,
            IActiveGameInstanceManager gameInstanceManager,
            IGameInstanceFactory gameInstanceFactory)
        {
            _userCommunicationService = userCommunicationService;
            _userStorage = userStorage;
            _gameInstanceManager = gameInstanceManager;
            _gameInstanceFactory = gameInstanceFactory;
        }

        public void ConnectUser(string connectionId)
        {
            //TODO handle concurrency
            var user = _userStorage.CreateUser(connectionId);
            if (_awaitableUser == null)
            {
                _awaitableUser = user;
            }
            else
            {
                var game = _gameInstanceFactory.CreateGameInstance(_awaitableUser, user);
                _gameInstanceManager.AddGameInstance(game);
                _awaitableUser = null;
                game.StartGame();
            }
        }

        public void DisconnectUser(string connectionId)
        {
            var user = _userStorage.GetUserById(connectionId);
            if (user != null)
            {
                var game = _gameInstanceManager.GetGameInstanceByUser(user);
                if (game != null)
                {
                    game.StopGame();
                    _gameInstanceManager.RemoveGameInstance(game);
                }
                _userStorage.RemoveUser(user);
            }

        }

        public void HandleUserMessage<TMessage>(string connectionId, TMessage message) where TMessage : BaseMessage
        {
            var user = _userStorage.GetUserById(connectionId);
            if (user != null)
            {
                var game = _gameInstanceManager.GetGameInstanceByUser(user);
                if (game != null)
                {
                    game.HandleUserMessage(user, message);

                }
            }
        }
    }
}
