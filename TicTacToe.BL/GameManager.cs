using System.Threading.Tasks;
using TicTacToe.BL.Interfaces;
using TicTacToe.BL.Models;

namespace TicTacToe.BL
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

        public void DisconnectUser(string connectionId)
        {
            var user = _userStorage.GetUserById(connectionId);
            if (user != null)
            {
                var game = _gameInstanceStorage.GetGameInstanceByUser(user);
                if (game != null)
                {
                    game.StopGame();
                    _gameInstanceStorage.RemoveGameInstance(game);
                }
                _userStorage.RemoveUser(user);
            }
        }

        public void HandleUserMessage<TMessage>(string connectionId, TMessage message) where TMessage : BaseMessage
        {
            var user = _userStorage.GetUserById(connectionId);
            if (user != null)
            {
                var game = _gameInstanceStorage.GetGameInstanceByUser(user);
                if (game != null)
                {
                    game.HandleUserMessage(user, message);

                }
            }
        }
    }
}
