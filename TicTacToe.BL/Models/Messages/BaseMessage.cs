namespace TicTacToe.BL.Models
{
    public abstract class BaseMessage
    {
        public abstract string MessageName { get; }

        public abstract object GetMessageData();
    }
}
