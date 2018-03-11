using Microsoft.VisualStudio.TestTools.UnitTesting;
using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.UnitTest
{
    [TestClass]
    public class DatabaseIniter
    {
        [TestMethod]
        public void CleanDatabase()
        {
            using (var dbContext = new RexDbContext())
            {
                dbContext.Database.ExecuteSqlCommand(@"
                    drop table orders
                    go
                    drop table favourites
                    go
                    drop table producttypes
                    go
                    drop table contacts
                    go
                    drop table districts
                    go
                    drop table timeadays
                    go
                    drop table users
                    go
                    drop table __migrationhistory");
            }
        }

        [TestMethod]
        public void StartDatabase()
        {
            using (var dbContext = new RexDbContext())
            {
                dbContext.Districts.Add(new District { Name = "Quận 1" });
                dbContext.Districts.Add(new District { Name = "Quận 2" });
                dbContext.Districts.Add(new District { Name = "Quận 3" });

                dbContext.TimeADays.Add(new TimeADay { TimeInDay = "Buổi sáng" });
                dbContext.TimeADays.Add(new TimeADay { TimeInDay = "Buổi trưa" });
                dbContext.TimeADays.Add(new TimeADay { TimeInDay = "Buổi tối" });

                dbContext.ProductType.Add(new ProductType { Name = "Gạo Đài Loan"});
                dbContext.ProductType.Add(new ProductType { Name = "Gạo Nở" });

                dbContext.Users.Add(new User { Email = "tiennsloit@gmail.com", IsActived=true, IsDeleted=false, UserName="tiennsloit" });

                dbContext.SaveChanges();
                
            }
        }
    }
}
