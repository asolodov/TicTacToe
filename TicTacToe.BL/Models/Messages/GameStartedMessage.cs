namespace TicTacToe.BL.Models.Messages
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
