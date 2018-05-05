using System.Runtime.Serialization;
using TicTacToe.Infrastructure;

namespace TicTacToe.DataContracts
{
    [DataContract(Name = Constants.DataContracts.CellPosition)]
    public class CellPosition
    {
        [DataMember(Name = Constants.DataMembers.X)]
        public int X { get; set; }

        [DataMember(Name = Constants.DataMembers.Y)]
        public int Y { get; set; }
    }
}
