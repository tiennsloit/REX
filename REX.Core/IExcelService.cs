using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace REX.Core
{
    public interface IExcelService
    {
        void Save(List<ExcelDataSheet> excelSheetData, string filePath, CultureInfo culture = null);

        void Save(List<ExcelDataSheet> excelSheetData, string filePath, string templatePath, CultureInfo culture = null);

        FileStream SaveAs(FileStream ouput, List<ExcelDataSheet> excelSheetData, CultureInfo culture = null);

        MemoryStream SaveAs(MemoryStream ouput, List<ExcelDataSheet> excelSheetData, CultureInfo culture = null);

        FileStream SaveAs(FileStream ouput, List<ExcelDataSheet> excelSheetData, string templatePath, CultureInfo culture = null);

        MemoryStream SaveAs(MemoryStream ouput, List<ExcelDataSheet> excelSheetData, string templatePath, CultureInfo culture = null);
    }
}
