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
        public ProductType GetProductType(int productTypeId)
        {
            using (var dbContext = new RexDbContext())
            {
                return dbContext.ProductType.Where(x=>x.Id == productTypeId).FirstOrDefault();
            }
        }

        public ICollection<ProductType> GetProductTypes()
        {
            using (var dbContext = new RexDbContext())
            {
                return dbContext.ProductType.ToList();
            }
        }
    }
}
