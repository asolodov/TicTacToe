namespace TicTacToe.BL.Users.Models.Messages
{
    public class StateUpdateMessage : PlayerActionMessage
    {
        public bool IsActive { get; set; }

        public StateUpdateMessage()
        {
        }

        public StateUpdateMessage(PlayerActionMessage playerAction, bool isActive)
        {
            CellPosition = playerAction.CellPosition;
            CellType = playerAction.CellType;
            IsActive = isActive;
        }

    }
}
