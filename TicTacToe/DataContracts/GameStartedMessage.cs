using System.Runtime.Serialization;
using TicTacToe.Infrastructure;

namespace TicTacToe.DataContracts
{
    [DataContract(Name = Constants.DataContracts.GameStartedMessage)]
    public class GameStartedMessage
    {
        [DataMember(Name = Constants.DataMembers.CellType)]
        public CellType CellType { get; set; }
    }

}