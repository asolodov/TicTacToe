using TicTacToe.BL.GameInstance.Interfaces;
using TicTacToe.BL.GameInstance.Models;
using TicTacToe.BL.Users.Interfaces;
using TicTacToe.BL.Users.Models;

namespace TicTacToe.BL.GameInstance
{
    public class GameInstanceFactory : IGameInstanceFactory
    {
        private readonly IUserCommunicationService _userCommunicationService;

        public GameInstanceFactory(IUserCommunicationService userCommunicationService)
        {
            _userCommunicationService = userCommunicationService;
        }

        public IGameInstance CreateGameInstance(User userOne, User userTwo)
        {
            return new GameInstance(_userCommunicationService)
            {
                PlayerOne = new Player(userOne, CellType.Toe),
                PlayerTwo = new Player(userTwo, CellType.Tic)
            };
        }
    }
}
