using REX.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REX.API.Controllers
{
    public class BarcodeController : ApiController
    {
        private readonly IBarcodeService _barcodeService;
        public BarcodeController(IBarcodeService barcodeService)
        {
            _barcodeService = barcodeService;
        }

        [Route("Api/Barcode/text/{text}")]
        public string GetBarcode(string text)
        {
            return _barcodeService.GenerateBarcode(text);
        }
    }
}
