using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Services
{
    public class ProductTypeService:IProductTypeService
    {
        public ICollection<ProductType> GetRiceTypes()
        {
            using (var dbContext = new RexDbContext())
            {
                return dbContext.ProductType.ToList();
            }
        }
    }
}
