using REX.Data;

namespace REX.Core.Services
{
    public interface IUserService
    {
        void CreateUser(User user);
        User GetUser(string userId);
    }
}