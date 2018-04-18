using System.Runtime.Serialization;
using TicTacToe.Infrastructure;

namespace TicTacToe.DataContracts
{
    [DataContract(Name = Constants.DataContracts.GameStoppedMessage)]
    public class GameStoppedMessage : BaseGameMessage
    {
        public override string MessageType => Constants.MessageTypes.GameStopped;

        [DataMember(Name = Constants.DataMembers.Reason)]
        public string Reason { get; set; }
    }
}
