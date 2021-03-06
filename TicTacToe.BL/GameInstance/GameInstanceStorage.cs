﻿using System;
using System.Collections.Generic;
using TicTacToe.BL.GameInstance.Interfaces;
using TicTacToe.BL.Users.Models;

namespace TicTacToe.BL.GameInstance
{
    public class GameInstanceStorage : IGameInstanceStorage
    {
        private readonly Dictionary<string, IGameInstance> _runningGames = new Dictionary<string, IGameInstance>();

        public void AddGameInstance(IGameInstance instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            foreach (var userId in instance.UserIds)
            {
                _runningGames[userId] = instance;
            }
        }

        public IGameInstance GetGameInstanceByUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (_runningGames.TryGetValue(user.ConnectionId, out IGameInstance gameInstance))
            {
                return gameInstance;
            }
            return null;
        }

        public void RemoveGameInstance(IGameInstance instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            foreach (var userId in instance.UserIds)
            {
                _runningGames.Remove(userId);
            }
        }
    }
}
