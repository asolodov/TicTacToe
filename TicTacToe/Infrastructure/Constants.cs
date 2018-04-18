
namespace TicTacToe.Infrastructure
{
    public static class Constants
    {
        public static class DataContracts
        {
            public const string GameStartedMessage = "GameStartedMessage";
            public const string GameStoppedMessage = "GameStoppedMessage";
            public const string PlayerActionMessage = "PlayerActionMessage";
            
        }

        public static class DataMembers
        {
            public const string CellType = "cellType";
            public const string IsActive = "isActive";
            public const string CellPosition = "cellPosition";
            public const string Reason = "reason";
        }

        public static class MessageTypes
        {
            public const string GameStarted = "GameStarted";
            public const string GameStopped = "GameStopped";
            public const string PlayerAction = "PlayerAction";
        }
    }
}
