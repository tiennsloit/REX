namespace REX.Core
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Globalization;
    using OfficeOpenXml;

    public class ExcelService : IExcelService
    {
        private ExcelContext excelContext;

        public ExcelService()
        {
            this.excelContext = ExcelContext.Instance;
        }

        public void Save(List<ExcelDataSheet> excelSheetData, string filePath, CultureInfo culture = null)
        {
            using (FileStream file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                this.SaveAs(file, excelSheetData, culture);
            }
        }

        public void Save(List<ExcelDataSheet> excelSheetData, string filePath, string templatePath, CultureInfo culture = null)
        {
            using (FileStream file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                this.SaveAs(file, excelSheetData, templatePath, culture);
            }            
        }

        public FileStream SaveAs(FileStream ouput, List<ExcelDataSheet> excelSheetData, CultureInfo culture = null)
        {
            if (culture != null)
            {
                this.excelContext.Culture = culture;
            }
           
            this.excelContext.DataSheets = excelSheetData;
            var memoryStream = this.excelContext.WriteFile();
            memoryStream.WriteTo(ouput);
            return ouput;
        }

        public MemoryStream SaveAs(MemoryStream ouput, List<ExcelDataSheet> excelSheetData, CultureInfo culture = null)
        {
            if (culture != null)
            {
                this.excelContext.Culture = culture;
            }

            this.excelContext.DataSheets = excelSheetData;
            return this.excelContext.WriteFile();
        }

        public FileStream SaveAs(FileStream ouput, List<ExcelDataSheet> excelSheetData, string templatePath, CultureInfo culture = null)
        {
            if (culture != null)
            {
                this.excelContext.Culture = culture;
            }

            this.excelContext.Template = templatePath;
            if (this.excelContext.Template == null)
            {
                throw new Exception("The excel template file is not found.");
            }
            this.excelContext.DataSheets = excelSheetData;
            var memoryStream = this.excelContext.WriteFile();
            memoryStream.WriteTo(ouput);
            return ouput;
        }

        public MemoryStream SaveAs(MemoryStream ouput, List<ExcelDataSheet> excelSheetData, string templatePath, CultureInfo culture = null)
        {
            if (culture != null)
            {
                this.excelContext.Culture = culture;
            }

            this.excelContext.Template = templatePath;
            if (this.excelContext.Template == null)
            {
                throw new Exception("The excel template file is not found.");
            }
            this.excelContext.DataSheets = excelSheetData;
            return this.excelContext.WriteFile();
        }

        public Dictionary<string, object> ReadExcel(string filePath)
        {
            var fields = new Dictionary<string, object>();
            var package = new ExcelPackage(new FileInfo(filePath));
            if (package.Workbook.Worksheets.Count > 0)
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[StringHelper.GetAppSettingValueOrDefault("SheetName", "")];
                for (int i = workSheet.Dimension.Start.Column;
                    i <= workSheet.Dimension.End.Column;
                    i++)
                {
                    for (int j = workSheet.Dimension.Start.Row;
                            j <= workSheet.Dimension.End.Row;
                            j++)
                    {
                        //fields.Add(workSheet.Cells[i, j].co)
                        var cell = workSheet.Cells[i, j];
                        fields.Add(cell.Address, cell.Value);
                    }
                }
            
            }
            return fields;
        }
    }
}