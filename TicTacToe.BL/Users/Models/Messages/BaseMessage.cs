namespace TicTacToe.BL.Users.Models.Messages
{
    public abstract class BaseMessage
    {
        public abstract string MessageName { get; }

        public abstract object GetMessageData();
    }
}
