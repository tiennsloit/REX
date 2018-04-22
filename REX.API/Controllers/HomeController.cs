using REX.API.Models;
using REX.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace REX.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBarcodeService _barcodeService;
        private readonly IProductTypeService _productTypeService;
        public HomeController(IBarcodeService barcodeService, IProductTypeService productTypeService)
        {
            _barcodeService = barcodeService;
            _productTypeService = productTypeService;
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var productTypes = _productTypeService.GetProductTypes().Select(x=> new ProductTypeModel
            {
                Price = x.Price,
                 BarCode = _barcodeService.GenerateBarcode(x.Id.ToString()),
                  Id = x.Id,
                   Description = x.Description,
                    Name = x.Name
        });
            
            return View(productTypes);
        }
    }
}
