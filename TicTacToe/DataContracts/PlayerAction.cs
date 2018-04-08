using System.Runtime.Serialization;
using TicTacToe.Infrastructure;

namespace TicTacToe.DataContracts
{
    [DataContract(Name = Constants.DataContracts.PlayerAction)]
    public class PlayerAction
    {
        [DataMember(Name = Constants.DataMembers.CellPosition)]
        public CellPosition CellPosition { get; set; }

        [DataMember(Name = Constants.DataMembers.CellType)]
        public CellType CellType { get; set; }
    }
}
