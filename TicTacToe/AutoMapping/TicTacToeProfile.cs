﻿using AutoMapper;
using TicTacToe.DataContracts;

namespace TicTacToe.AutoMapping
{
    public class TicTacToeProfile : Profile
    {
        public TicTacToeProfile()
        {
            CreateMap<BL.Users.Models.Messages.BaseGameMessage, BaseGameMessage>()
                .Include<BL.Users.Models.Messages.GameStartedMessage, GameStartedMessage>()
                .Include<BL.Users.Models.Messages.PlayerActionMessage, PlayerActionMessage>()
                .Include<BL.Users.Models.Messages.GameStoppedMessage, GameStoppedMessage>();

            CreateMap<BL.Users.Models.Messages.GameStartedMessage, GameStartedMessage>();
            CreateMap<BL.Users.Models.Messages.PlayerActionMessage, PlayerActionMessage>();
            CreateMap<BL.Users.Models.Messages.GameStoppedMessage, GameStoppedMessage>();


            CreateMap<BaseGameMessage, BL.Users.Models.Messages.BaseGameMessage>()
                .Include<PlayerActionMessage, BL.Users.Models.Messages.PlayerActionMessage>();

            CreateMap<PlayerActionMessage, BL.Users.Models.Messages.PlayerActionMessage>();

            CreateMap<BL.GameInstance.Models.CellPosition, CellPosition>();
            CreateMap<CellPosition, BL.GameInstance.Models.CellPosition>();

            CreateMap<BL.GameInstance.Models.CellType, CellType>();
            CreateMap<CellType, BL.GameInstance.Models.CellType>();
        }
    }
}
