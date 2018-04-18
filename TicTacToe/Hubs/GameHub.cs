using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using TicTacToe.BL.GameManager.Interfaces;
using TicTacToe.BL.Users.Models.Messages;
using TicTacToe.DataContracts;

namespace TicTacToe.Hubs
{
    public class GameHub : Hub
    {
        private readonly IGameManager _gameManager;

        public GameHub(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public async Task HandlePlayerAction(DataContracts.PlayerActionMessage action)
        {
            await _gameManager.HandlePlayerActionMessage(Context.ConnectionId, Mapper.Map<BL.Users.Models.Messages.PlayerActionMessage>(action));
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
