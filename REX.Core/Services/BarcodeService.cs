using REX.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Services
{
    public class BarcodeService : IBarcodeService
    {
        public string GenerateBarcode(string text)
        {
            var bc = new BarCodeGenerator();
            return bc.GetBarCodeFromString(text);
        }
    }
}
