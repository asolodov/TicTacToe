using System;
using System.Threading.Tasks;
using TicTacToe.BL.GameInstance.Interfaces;
using TicTacToe.BL.GameManager.Interfaces;
using TicTacToe.BL.Users.Interfaces;
using TicTacToe.BL.Users.Models;
using TicTacToe.BL.Users.Models.Messages;

namespace TicTacToe.BL.GameManager
{
    public class GameManager : IGameManager
    {
        private User _awaitableUser;

        private readonly IUserCommunicationService _userCommunicationService;
        private readonly IUserStorage _userStorage;
        private readonly IGameInstanceStorage _gameInstanceStorage;
        private readonly IGameInstanceFactory _gameInstanceFactory;

        public GameManager(
            IUserCommunicationService userCommunicationService,
            IUserStorage userStorage,
            IGameInstanceStorage gameInstanceStorage,
            IGameInstanceFactory gameInstanceFactory)
        {
            _userCommunicationService = userCommunicationService;
            _userStorage = userStorage;
            _gameInstanceStorage = gameInstanceStorage;
            _gameInstanceFactory = gameInstanceFactory;
        }

        public async Task ConnectUser(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
                throw new ArgumentNullException(nameof(connectionId));

            //TODO handle concurrency
            var user = _userStorage.CreateUser(connectionId);
            if (_awaitableUser == null)
            {
                _awaitableUser = user;
            }
            else
            {
                var game = _gameInstanceFactory.CreateGameInstance(_awaitableUser, user);
                _gameInstanceStorage.AddGameInstance(game);
                _awaitableUser = null;
                await game.StartGame();
            }
        }

        public async Task DisconnectUser(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
                throw new ArgumentNullException(nameof(connectionId));

            var user = _userStorage.GetUserById(connectionId);
            if (user != null)
            {
                var game = _gameInstanceStorage.GetGameInstanceByUser(user);
                if (game != null)
                {
                    await game.StopGame();
                    _gameInstanceStorage.RemoveGameInstance(game);
                }
                _userStorage.RemoveUser(connectionId);
            }
        }

        public async Task HandlePlayerActionMessage(string fromUserId, PlayerActionMessage action)
        {
            if (string.IsNullOrEmpty(fromUserId))
                throw new ArgumentNullException(nameof(fromUserId));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var user = _userStorage.GetUserById(fromUserId);
            if (user != null)
            {
                var game = _gameInstanceStorage.GetGameInstanceByUser(user);
                if (game != null)
                {
                    await game.HandlePlayerActionMessage(user, action);
                }
            }
        }
    }
}
