using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using TicTacToe.BL.GameManager.Interfaces;

namespace TicTacToe.Hubs
{
    public class GameHub : Hub
    {
        private readonly IGameManager _gameManager;

        public GameHub(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }
        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("Send", message + "2222");
        }

        public override async Task OnConnectedAsync()
        {
            await _gameManager.ConnectUser(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _gameManager.DisconnectUser(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
