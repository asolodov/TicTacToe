using AutoMapper;
using TicTacToe.DataContracts;

namespace TicTacToe.AutoMapping
{
    public class TicTacToeProfile : Profile
    {
        public TicTacToeProfile()
        {
            CreateMap<BL.Users.Models.Messages.GameStartedMessage, GameStartedMessage>();
        }
    }
}
