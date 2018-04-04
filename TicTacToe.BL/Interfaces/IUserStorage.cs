using TicTacToe.BL.Models;

namespace TicTacToe.BL.Interfaces
{
    public interface IUserStorage
    {
        User GetUserById(string connectionId);
        void RemoveUser(User user);
        User CreateUser(string connectionId);
    }
}
