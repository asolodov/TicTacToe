using System;
using TicTacToe.BL.Users.Models;

namespace TicTacToe.BL.GameInstance.Models
{
    public class Player : User
    {
        public Player(User user, CellType cellType)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            Name = user.Name;
            ConnectionId = user.ConnectionId;
            PlayerCell = cellType;
        }
        public CellType PlayerCell { get; set; }

        public bool IsActive { get; set; }
    }
}
