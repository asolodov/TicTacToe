using AutoMapper;
using TicTacToe.DataContracts;

namespace TicTacToe.AutoMapping
{
    public class TicTacToeProfile : Profile
    {
        public TicTacToeProfile()
        {
            CreateMap<BL.Users.Models.Messages.GameStartedMessage, GameStartedMessage>();
            CreateMap<BL.Users.Models.Messages.PlayerActionMessage, PlayerAction>();

            CreateMap<BL.GameInstance.Models.CellPosition, CellPosition>();
            CreateMap<CellPosition, BL.GameInstance.Models.CellPosition>();

            CreateMap<BL.GameInstance.Models.CellType, CellType>();
            CreateMap<CellType, BL.GameInstance.Models.CellType>();
        }
    }
}
