using System.Collections.Generic;
using TicTacToe.BL.GameInstance.Interfaces;
using TicTacToe.BL.Users.Models;

namespace TicTacToe.BL.GameInstance
{
    public class GameInstanceStorage : IGameInstanceStorage
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
