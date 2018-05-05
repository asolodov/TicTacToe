using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TicTacToe.Infrastructure;

namespace TicTacToe.DataContracts
{
    [DataContract(Name = Constants.DataContracts.StateUpdateMessage)]
    public class StateUpdateMessage : PlayerActionMessage
    {
        public override string MessageType => Constants.MessageTypes.StateUpdate;

        [DataMember(Name = Constants.DataMembers.IsActive)]
        public bool IsActive { get; set; }
    }
}
