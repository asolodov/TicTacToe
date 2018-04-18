using TicTacToe.BL.GameInstance.Models;

namespace TicTacToe.BL.Users.Models.Messages
{
    public class PlayerActionMessage : BaseGameMessage
    {
        public CellPosition CellPosition { get; set; }

        public CellType CellType { get; set; }
    }
}
