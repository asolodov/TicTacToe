using System.Threading.Tasks;
using TicTacToe.BL.Models;

namespace TicTacToe.BL.Interfaces
{
    public interface IUserCommunicationService
    {
        Task SendMessage(User toUser, BaseMessage message);
    }
}
