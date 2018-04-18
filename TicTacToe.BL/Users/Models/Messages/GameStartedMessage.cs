using TicTacToe.BL.GameInstance.Models;

namespace TicTacToe.BL.Users.Models.Messages
{
    public class GameStartedMessage : BaseGameMessage
    {
        public CellType CellType { get; set; }
        public bool IsActive { get; set; }

        public GameStartedMessage(CellType cellType, bool isActive)
        {
            CellType = cellType;
            IsActive = isActive;
        }
    }
}
