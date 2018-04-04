using AutoMapper;
using TicTacToe.DataContracts;

namespace TicTacToe.AutoMapping
{
    public class TicTacToeProfile : Profile
    {
        public TicTacToeProfile()
        {
            CreateMap<BL.Models.Messages.GameStartedMessage, GameStartedMessage>();
        }
    }
}
