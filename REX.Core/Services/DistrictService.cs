using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Services
{
    public class DistrictService : IDistrictService
    {
        public void InsertDistrict(string name)
        {
            using (var dbContext = new RexDbContext())
            {
                var existing = dbContext.Districts.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
                if (existing != null)
                    return;
                dbContext.Districts.Add(new District { Name = name});
                dbContext.SaveChanges();
            }
        }

        public ICollection<District> GetDistricts()
        {
            using (var dbContext = new RexDbContext())
            {
                return dbContext.Districts.ToList();
            }
        }
    }
}
