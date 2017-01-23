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

        public virtual ICollection<ContactModel> ReadExcel(string filePath)
        {
            var headerHeight = Convert.ToInt16(StringHelper.GetAppSettingValueOrDefault("HeaderHeight", "2"));
            var mappers = GetFieldMappers();
            var contacts = new List<ContactModel>();
            var package = new ExcelPackage(new FileInfo(filePath));
            if (package.Workbook.Worksheets.Count > 0)
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[StringHelper.GetAppSettingValueOrDefault("SheetName", "")];
                for (int i = workSheet.Dimension.Start.Row + headerHeight;
                    i <= workSheet.Dimension.End.Row;
                    i++)
                {
                    var contact = new ContactModel();
                    foreach (var mapperColumn in mappers)
                    {
                        var cell = workSheet.Cells[mapperColumn.Key + i.ToString()];
                        UpdateModelValue(contact, mapperColumn.Value, cell.Value);
                    }
                    contacts.Add(contact);
                }
            
            }
            return contacts;
        }

        
        public void UpdateModelValue(ContactModel model, string propName, object value)
        {
            Type type = model.GetType();

            PropertyInfo prop = type.GetProperty(propName);
            Type t = model.GetType().GetProperty(propName).PropertyType;
            if (value != null)
            {
                if (t == typeof(DateTime))
                {
                    var d = double.Parse(value.ToString());
                    DateTime conv = DateTime.FromOADate(d);
                    prop.SetValue(model, conv);
                }
                else
                {
                    prop.SetValue(model, Convert.ChangeType(value, t, null));
                }
            }
            else
                prop.SetValue(model, null);
        }
    }
}