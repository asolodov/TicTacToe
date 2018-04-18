namespace TicTacToe.BL.Users.Models.Messages
{
    public class GameStoppedMessage : BaseGameMessage
    {
        public string Reason { get; set; }
    }
}
