using TicTacToe.BL.GameInstance.Models;

namespace TicTacToe.BL.Users.Models.Messages
{
    public class PlayerActionMessage
    {
        public CellType CellType { get; set; }
    }
}
