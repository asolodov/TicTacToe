using TicTacToe.BL.Users.Models;

namespace TicTacToe.BL.GameInstance.Models
{
    public class Player : User
    {
        public Player(User user, CellType cellType)
        {
            Name = user.Name;
            ConnectionId = user.ConnectionId;
            PlayerCell = cellType;
        }
        public CellType PlayerCell { get; set; }
    }
}
