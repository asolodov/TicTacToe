using System.Threading.Tasks;
using TicTacToe.BL.Users.Models.Messages;

namespace TicTacToe.BL.Users.Interfaces
{
    public interface IUserCommunicationService
    {
        Task SendGameStartedMessage(string connectionId, GameStartedMessage message);
    }
}
