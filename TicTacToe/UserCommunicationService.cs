using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using TicTacToe.BL.Interfaces;
using TicTacToe.BL.Models;
using TicTacToe.Hubs;

namespace TicTacToe
{
    public class UserCommunicationService : IUserCommunicationService
    {
        private readonly IHubContext<GameHub> _hubContext;

        public UserCommunicationService(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessage(User to, BaseMessage message)
        {
            if (to == null)
                throw new ArgumentNullException(nameof(to));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

           await _hubContext.Clients.Client(to.ConnectionId).InvokeAsync(message.MessageName, message.GetMessageData());
        }
    }
}
