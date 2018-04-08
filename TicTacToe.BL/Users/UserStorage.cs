using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Users.Interfaces;
using TicTacToe.BL.Users.Models;

namespace TicTacToe.BL.Users
{
    public class UserStorage : IUserStorage
    {
        private readonly Dictionary<string, User> _users = new Dictionary<string, User>();

        public User CreateUser(string connectionId)
        {
            if (!_users.TryGetValue(connectionId, out User user))
            {
                user = new User();
                _users[connectionId] = user;
            }
            return user;
        }

        public User GetUserById(string connectionId)
        {
            if (_users.TryGetValue(connectionId, out User user))
            {
                return user;
            }
            return null;
        }

        public void RemoveUser(string connectionId)
        {
            _users.Remove(connectionId);
        }
    }
}
