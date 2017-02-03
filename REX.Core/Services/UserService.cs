using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Services
{
    public class UserService:IUserService
    {
        public void CreateUser(User user)
        {
            using (var dbContext = new RexDbContext())
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
        }

        public User GetUser(string userName)
        {
            var user = new User();
            using (var dbContext = new RexDbContext())
            {
                user = dbContext.Users.Where(x => x.UserName == userName).FirstOrDefault();
            }
            return user;
        }
    }
}
