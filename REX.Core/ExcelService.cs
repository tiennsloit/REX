namespace REX.Core
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Globalization;
    using OfficeOpenXml;
    using REX.Core.Model;
    using System.Reflection;
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

        public Dictionary<string, string> GetFieldMappers()
        {
            var mappers2 = new Dictionary<string, string>();
            var mappers = StringHelper.GetAppSettingValueOrDefault("FieldMappers", "");
            var mappers1 = mappers.Split(',');
            foreach (var item in mappers1)
            {
                var kv = item.Split(':');
                mappers2.Add(kv[0], kv[1]);
            }

            return mappers2;
        }

        public Dictionary<string, object> ReadExcel(string filePath)
        {
            var mappers = GetFieldMappers();
            var fields = new Dictionary<string, object>();
            var contacts = new List<ContactModel>();
            var package = new ExcelPackage(new FileInfo(filePath));
            if (package.Workbook.Worksheets.Count > 0)
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[StringHelper.GetAppSettingValueOrDefault("SheetName", "")];
                for (int i = workSheet.Dimension.Start.Row;
                    i <= workSheet.Dimension.End.Row;
                    i++)
                {
                    var contact = new ContactModel();
                    foreach (var mapperColumn in mappers)
                    {
                        var cell = workSheet.Cells[mapperColumn.Key + i.ToString()];
                        UpdateModelValue(contact, mapperColumn.Value, cell.Value);
                        fields.Add(cell.Address, cell.Value);
                    }
                    contacts.Add(contact);
                }
            
            }
            return fields;
        }

        public void UpdateModelValue(ContactModel model, string propName, object value)
        {
            Type type = model.GetType();

            PropertyInfo prop = type.GetProperty("propertyName");

            prop.SetValue(model, value, null);
        }
    }
}