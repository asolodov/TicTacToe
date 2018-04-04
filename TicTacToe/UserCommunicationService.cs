using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using TicTacToe.BL.Interfaces;
using TicTacToe.BL.Models;
using TicTacToe.BL.Models.Messages;
using TicTacToe.Hubs;
using DTO = TicTacToe.DataContracts;

namespace TicTacToe
{
    public class UserCommunicationService : IUserCommunicationService
    {
        private const string GameStarted = "GameStarted";

        private readonly IHubContext<GameHub> _hubContext;

        public UserCommunicationService(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendGameStartedMessage(string connectionId, GameStartedMessage message)
        {
            if (string.IsNullOrEmpty(connectionId))
                throw new ArgumentNullException(nameof(connectionId));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var dto = Mapper.Map<DTO.GameStartedMessage>(message);
            await SendMessage(connectionId, GameStarted, dto);
        }

        private async Task SendMessage(string connectionId, string method, object message)
        {
            await _hubContext.Clients.Client(connectionId).InvokeAsync(method, message);
        }
    }
}
