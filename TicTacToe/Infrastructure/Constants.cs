
namespace TicTacToe.Infrastructure
{
    public static class Constants
    {
        public static class DataContracts
        {
            public const string GameStartedMessage = "GameStartedMessage";
            public const string GameStoppedMessage = "GameStoppedMessage";
            public const string PlayerActionMessage = "PlayerActionMessage";
            public const string StateUpdateMessage = "StateUpdateMessage";
            public const string CellPosition = "CellPosition";

        }

        public static class DataMembers
        {
            public const string CellType = "cellType";
            public const string IsActive = "isActive";
            public const string CellPosition = "cellPosition";
            public const string Reason = "reason";
            public const string X = "x";
            public const string Y = "y";
        }

        public static class MessageTypes
        {
            public const string GameStarted = "GameStarted";
            public const string GameStopped = "GameStopped";
            public const string PlayerAction = "PlayerAction";
            public const string StateUpdate = "StateUpdate";
        }
    }
}
