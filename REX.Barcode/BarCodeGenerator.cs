using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zen.Barcode;

namespace REX.Barcode
{
    public class BarCodeGenerator
    {
        public string GetBarCodeFromString(string text)
        {
            BarcodeSymbology s = BarcodeSymbology.Code39C;
            BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(s);
            var metrics = drawObject.GetDefaultMetrics(60);
            metrics.Scale = 2;
            var barcodeImage = drawObject.Draw(text, metrics);

            using (MemoryStream ms = new MemoryStream())
            {
                barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}
