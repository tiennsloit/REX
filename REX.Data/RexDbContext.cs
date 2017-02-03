using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Data
{
    public class RexDbContext: DbContext
    {
        public RexDbContext():base("DefaultConnection") {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<TimeADay> TimeADays { get; set; }

        public DbSet<RiceType> RiceType { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
