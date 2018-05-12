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
        private readonly object _sync = new object();
        private volatile User _awaitableUser;

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

            var user = _userStorage.CreateUser(connectionId);

            User awaitable;
            lock (_sync)
            {
                awaitable = _awaitableUser;
                _awaitableUser = _awaitableUser != null ? null : user;
            }

            if (awaitable != null)
            {
                var game = _gameInstanceFactory.CreateGameInstance(awaitable, user);
                _gameInstanceStorage.AddGameInstance(game);
                await game.StartGame();
            }
        }

        public async Task DisconnectUser(string disconnectedId)
        {
            if (string.IsNullOrEmpty(disconnectedId))
                throw new ArgumentNullException(nameof(disconnectedId));

            var user = _userStorage.GetUserById(disconnectedId);
            if (user != null)
            {
                var game = _gameInstanceStorage.GetGameInstanceByUser(user);
                if (game != null)
                {
                    await game.StopGame();
                    _gameInstanceStorage.RemoveGameInstance(game);
                    foreach (var gameUser in game.UserIds)
                    {
                        if (gameUser != disconnectedId)
                        {
                            await ConnectUser(gameUser);
                        }
                    }
                }
                _userStorage.RemoveUser(disconnectedId);
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
