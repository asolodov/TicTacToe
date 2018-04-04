using TicTacToe.BL.Interfaces;
using TicTacToe.BL.Models;

namespace TicTacToe.BL
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
