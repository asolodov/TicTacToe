using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.GameInstance.Interfaces;
using TicTacToe.BL.GameManager.Interfaces;
using TicTacToe.BL.Users.Interfaces;
using TicTacToe.BL.Users.Models;

namespace TicTacToe.BL.UnitTests
{
    [TestFixture]
    public class GameManagerTests
    {
        private IGameManager _gameManager;

        private readonly Mock<IUserCommunicationService> _userCommunicationServiceMock = new Mock<IUserCommunicationService>();
        private readonly Mock<IUserStorage> _userStorageMock = new Mock<IUserStorage>();
        private readonly Mock<IGameInstanceStorage> _gameInstanceStorageMock = new Mock<IGameInstanceStorage>();
        private readonly Mock<IGameInstanceFactory> _gameInstanceFactoryMock = new Mock<IGameInstanceFactory>();
        private readonly Mock<IGameInstance> _gameInstanceMock = new Mock<IGameInstance>();

        [SetUp]
        public void SetUp()
        {
            _gameManager = new GameManager.GameManager(
                _userCommunicationServiceMock.Object,
                _userStorageMock.Object,
                _gameInstanceStorageMock.Object,
                _gameInstanceFactoryMock.Object);
        }

        [Test]
        public void ConnectUser_ParallelUserConnects_CorrectGamesCountCreated()
        {
            //arrange
            const int usersCount = 10000;
            const int expectedGamesCount = usersCount / 2;
            _gameInstanceMock.Setup(t => t.StartGame()).Returns(Task.CompletedTask);
            _gameInstanceFactoryMock.Setup(t => t.CreateGameInstance(It.IsAny<User>(), It.IsAny<User>())).Returns(_gameInstanceMock.Object);
            _userStorageMock.Setup(t => t.CreateUser(It.IsAny<string>())).Returns(new User());

            //act
            Parallel.For(0, usersCount, async (i) =>
             {
                 await _gameManager.ConnectUser(Guid.NewGuid().ToString());
             });

            //assert
            _gameInstanceStorageMock.Verify(t => t.AddGameInstance(It.IsAny<IGameInstance>()), Times.Exactly(expectedGamesCount));
        }
    }
}
