using TicTacToe.BL.GameInstance.Models;

namespace TicTacToe.BL.Users.Models.Messages
{
    public class GameStartedMessage 
    {
        public CellType CellType { get; set; }

        public GameStartedMessage(CellType cellType)
        {
            CellType = cellType;
        }
    }
}
