using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Services
{
    public class RiceTypeService:IRiceTypeService
    {
        public ICollection<RiceType> GetRiceTypes()
        {
            using (var dbContext = new RexDbContext())
            {
                return dbContext.RiceType.ToList();
            }
        }
    }
}
