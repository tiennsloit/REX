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
        public HomeController(IBarcodeService barcodeService)
        {
            _barcodeService = barcodeService;
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var barCode = _barcodeService.GenerateBarcode("1");
            return View(new BarcodeModel {  Image = barCode, Text = "1"});
        }
    }
}
