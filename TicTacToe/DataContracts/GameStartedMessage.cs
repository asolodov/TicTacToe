using System.Runtime.Serialization;
using TicTacToe.Infrastructure;

namespace TicTacToe.DataContracts
{
    [DataContract(Name = Constants.DataContracts.GameStartedMessage)]
    public class GameStartedMessage : BaseGameMessage
    {
        public override string MessageType => Constants.MessageTypes.GameStarted;

        [DataMember(Name = Constants.DataMembers.CellType)]
        public CellType CellType { get; set; }

        [DataMember(Name = Constants.DataMembers.IsActive)]
        public bool IsActive { get; set; }

    }

}