using REX.Data;
using System.Collections.Generic;

namespace REX.Core.Services
{
    public interface IUserService
    {
        void CreateUser(User user);
        User GetUser(string userId);
        ICollection<User> GetUsers();
    }
}