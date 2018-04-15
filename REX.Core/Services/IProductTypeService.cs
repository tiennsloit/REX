using REX.Data;
using System.Collections.Generic;

namespace REX.Core.Services
{
    public interface IProductTypeService
    {
        ICollection<ProductType> GetProductTypes();

        ProductType GetProductType(int productTypeId);
    }
}