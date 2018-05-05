using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TicTacToe.BL.GameInstance;
using TicTacToe.BL.GameInstance.Interfaces;
using TicTacToe.BL.GameInstance.Models;
using TicTacToe.BL.Users.Interfaces;
using TicTacToe.BL.Users.Models;
using TicTacToe.BL.Users.Models.Messages;

namespace TicTacToe.Bl.UnitTests
{
    [TestFixture]
    public class GameInstanceTests
    {
        private readonly Mock<IUserCommunicationService> _userCommunicationService = new Mock<IUserCommunicationService>();
        private Player _playerOne;
        private Player _playerTwo;
        private IGameInstance _gameInstance;

        [SetUp]
        public void SetUp()
        {
            _playerOne = new Player(new User()
            {
                ConnectionId = Guid.NewGuid().ToString(),
                Name = "PlayerOne"
            }, CellType.Tic);
            _playerTwo = new Player(new User()
            {
                ConnectionId = Guid.NewGuid().ToString(),
                Name = "PlayerTwo"
            }, CellType.Toe);

            _gameInstance = new GameInstance(_userCommunicationService.Object)
            {
                PlayerOne = _playerOne,
                PlayerTwo = _playerTwo
            };
        }

        [Test]
        public async Task StartGame_InitialStateSetCorrectly()
        {
            //act
            await _gameInstance.StartGame();

            //assert
            _userCommunicationService.Verify(t => t.SendMessageToUser(_playerOne.ConnectionId, It.Is<GameStartedMessage>(m => m.IsActive == true && m.CellType == CellType.Tic)), Times.Once);
            _userCommunicationService.Verify(t => t.SendMessageToUser(_playerTwo.ConnectionId, It.Is<GameStartedMessage>(m => m.IsActive == false && m.CellType == CellType.Toe)), Times.Once);
        }

        [Test]
        public async Task HandlePlayerActionMessage_TwoCorrectActionsProcessed_ActivePlayerChangedCorrectly()
        {
            //arrange
            await _gameInstance.StartGame();

            //act
            await _gameInstance.HandlePlayerActionMessage(_playerOne, new PlayerActionMessage());

            //assert
            _userCommunicationService.Verify(t => t.SendMessageToUser(_playerOne.ConnectionId, It.Is<StateUpdateMessage>(m => m.IsActive == false)), Times.Once);
            _userCommunicationService.Verify(t => t.SendMessageToUser(_playerTwo.ConnectionId, It.Is<StateUpdateMessage>(m => m.IsActive == true)), Times.Once);


            //arrange
            _userCommunicationService.ResetCalls();

            //act
            await _gameInstance.HandlePlayerActionMessage(_playerTwo, new PlayerActionMessage());

            //assert
            _userCommunicationService.Verify(t => t.SendMessageToUser(_playerOne.ConnectionId, It.Is<StateUpdateMessage>(m => m.IsActive == true)), Times.Once);
            _userCommunicationService.Verify(t => t.SendMessageToUser(_playerTwo.ConnectionId, It.Is<StateUpdateMessage>(m => m.IsActive == false)), Times.Once);
        }
    }
}
