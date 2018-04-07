using REX.Core.Services;
using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REX.API.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductTypeService _productTypeService;
        public ProductController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpGet]
        [Route("Api/ProductTypes")]

        public ICollection<ProductType> GetProductTypes()
        {
            return _productTypeService.GetProductTypes();
        }
    }
}
