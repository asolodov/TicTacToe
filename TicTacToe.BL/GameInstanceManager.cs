using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Interfaces;
using TicTacToe.BL.Models;

namespace TicTacToe.BL
{
    public class GameInstanceManager : IActiveGameInstanceManager
    {
        private Dictionary<string, IGameInstance> _runningGames = new Dictionary<string, IGameInstance>();

        public void AddGameInstance(IGameInstance instance)
        {
            foreach (var userId in instance.UserIds)
            {
                _runningGames[userId] = instance;
            }
        }

        public IGameInstance GetGameInstanceByUser(User user)
        {
            if (_runningGames.TryGetValue(user.ConnectionId, out IGameInstance gameInstance))
            {
                return gameInstance;
            }
            return null;
        }

        public void RemoveGameInstance(IGameInstance instance)
        {
            foreach (var userId in instance.UserIds)
            {
                _runningGames.Remove(userId);
            }
        }
    }
}
