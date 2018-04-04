using System.Threading.Tasks;
using TicTacToe.BL.Models.Messages;

namespace TicTacToe.BL.Interfaces
{
    public interface IUserCommunicationService
    {
        Task SendGameStartedMessage(string connectionId, GameStartedMessage message);
    }
}
