using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using TicTacToe.BL.Users.Interfaces;
using TicTacToe.BL.Users.Models.Messages;
using TicTacToe.Hubs;
using DTO = TicTacToe.DataContracts;

namespace TicTacToe
{
    public class UserCommunicationService : IUserCommunicationService
    {
        private const string GameStarted = "GameStarted";
        private const string GameStopped = "GameStopped";
        private const string PlayerAction = "PlayerAction";

        private readonly IHubContext<GameHub> _hubContext;

        public UserCommunicationService(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageToUser(string toUserId, BaseGameMessage message)
        {
            if (string.IsNullOrEmpty(toUserId))
                throw new ArgumentNullException(nameof(toUserId));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var dto = Mapper.Map<DTO.BaseGameMessage>(message);
            await SendMessage(toUserId, dto.MessageType, dto);
        }

        private async Task SendMessage(string connectionId, string method, object message)
        {
            await _hubContext.Clients.Client(connectionId).InvokeAsync(method, message);
        }
    }
}
