using TicTacToe.BL.Users.Models;

namespace TicTacToe.BL.Users.Interfaces
{
    public interface IUserStorage
    {
        User GetUserById(string connectionId);
        void RemoveUser(string connectionId);
        User CreateUser(string connectionId);
    }
}
