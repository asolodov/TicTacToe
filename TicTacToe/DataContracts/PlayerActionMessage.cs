using System.Runtime.Serialization;
using TicTacToe.Infrastructure;

namespace TicTacToe.DataContracts
{
    [DataContract(Name = Constants.DataContracts.PlayerActionMessage)]
    public class PlayerActionMessage : BaseGameMessage
    {
        public override string MessageType => Constants.MessageTypes.PlayerAction;

        [DataMember(Name = Constants.DataMembers.CellPosition)]
        public CellPosition CellPosition { get; set; }

        [DataMember(Name = Constants.DataMembers.CellType)]
        public CellType CellType { get; set; }
    }
}
