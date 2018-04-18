using NUnit.Framework;
using AutoMapper;
using FluentAssertions;
using System.Collections.Generic;
using DTO = TicTacToe.DataContracts;
using TicTacToe.BL.Users.Models.Messages;
using TicTacToe.BL.GameInstance.Models;

namespace TicTacToe.UnitTests
{
    [TestFixture]
    public class TicTacToeAutoMappingTests
    {
        [SetUp]
        public void Initialize()
        {
            AutoMapperInitilaizer.Initialize();
        }

        [Test]
        [TestCaseSource(nameof(Get_BL_To_DTO_MessageTestCases))]
        public void MappingTests_BL_To_DTO_MessageShouldMappedCorrectly(BaseGameMessage blMessage, DTO.BaseGameMessage expectedDto)
        {
            //arrange
            //act
            var dto = Mapper.Map<DTO.BaseGameMessage>(blMessage);

            //assert
            expectedDto.Should().BeEquivalentTo(dto, options => options.IncludingAllRuntimeProperties());
        }

        [Test]
        [TestCaseSource(nameof(Get_DTO_To_BL_MessageTestCases))]
        public void MappingTests_DTO_To_BL_MessageShouldMappedCorrectly(DTO.BaseGameMessage dtoMessage, BaseGameMessage expectedBlModel)
        {
            //arrange
            //act
            var blModel = Mapper.Map<BaseGameMessage>(dtoMessage);

            //assert
            expectedBlModel.Should().BeEquivalentTo(blModel, options => options.IncludingAllRuntimeProperties());
        }

        private static IEnumerable<TestCaseData> Get_BL_To_DTO_MessageTestCases()
        {
            yield return new TestCaseData(
                new GameStartedMessage(BL.GameInstance.Models.CellType.Toe, true),
                new DTO.GameStartedMessage()
                {
                    CellType = DTO.CellType.Toe,
                    IsActive = true
                }).SetName("BL_To_DTO_GameStartedMessage");

            yield return new TestCaseData(
                new GameStoppedMessage()
                {
                    Reason = "Test"
                },
                new DTO.GameStoppedMessage()
                {
                    Reason = "Test"
                }).SetName("BL_To_DTO_GameStoppedMessage");

            yield return new TestCaseData(
                new PlayerActionMessage()
                {
                    CellPosition = new CellPosition
                    {
                        X = 4,
                        Y = 5
                    },
                    CellType = CellType.Toe
                },
                new DTO.PlayerActionMessage()
                {
                    CellPosition = new DTO.CellPosition
                    {
                        X = 4,
                        Y = 5
                    },
                    CellType = DTO.CellType.Toe
                }).SetName("BL_To_DTO_PlayerActionMessage");
        }


        private static IEnumerable<TestCaseData> Get_DTO_To_BL_MessageTestCases()
        {
            yield return new TestCaseData(
                new DTO.PlayerActionMessage()
                {
                    CellPosition = new DTO.CellPosition
                    {
                        X = 4,
                        Y = 5
                    },
                    CellType = DTO.CellType.Toe
                },
                new PlayerActionMessage()
                {
                    CellPosition = new CellPosition
                    {
                        X = 4,
                        Y = 5
                    },
                    CellType = CellType.Toe
                }).SetName("DTO_To_BL_PlayerActionMessage");
        }
    }
}