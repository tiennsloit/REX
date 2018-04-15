using Microsoft.VisualStudio.TestTools.UnitTesting;
using REX.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.UnitTest
{
    [TestClass]
    public class BarcodeTest
    {
        [TestMethod]
        public void Test_GenerateBarcode()
        {
            var bc = new BarCodeGenerator();
            var str = bc.GetBarCodeFromString("1");
        }
    }
}
