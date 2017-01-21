using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace REX.Core
{
    public class ExcelContext
    {
        public CultureInfo Culture { get; set; }
        public string Template { get; set; }
        public List<ExcelDataSheet> DataSheets { get; set; }
        public static ExcelContext Instance { get { return new ExcelContext(); } }

        public ExcelContext()
        {
            this.Culture = new CultureInfo("en-US");
        }

        public MemoryStream WriteFile()
        {
            if (this.Template == null)
            {
                return this.WriteFileWithoutTemplate();
            }

            MemoryStream output = null;
            var template = new FileInfo(this.Template);
            using (ExcelPackage package = new ExcelPackage(template))
            {
                output = new MemoryStream();
                ExcelWorksheets sheets = package.Workbook.Worksheets;
                foreach (var sheetData in this.DataSheets)
                {
                    var sheet = sheets.FirstOrDefault(c => c.Name == sheetData.Name);
                    if (sheet != null)
                    {
                        if (sheetData.DictionaryData != null && sheetData.DictionaryData.Count > 0)
                        {
                            foreach (var key in sheetData.DictionaryData.Keys)
                            {
                                this.WriteContentByKey(sheet, key, sheetData.DictionaryData[key]);
                            }
                        }
                        else
                        {
                            this.WriteContent(sheet, sheetData.Data);
                        }
                    }
                    else
                    {
                        package.Workbook.Worksheets.Add(sheetData.Name);
                        sheet = package.Workbook.Worksheets.FirstOrDefault(c => c.Name == sheetData.Name);
                        if (sheet != null)
                        {
                            if (sheetData.DictionaryData != null && sheetData.DictionaryData.Count > 0)
                            {
                                foreach (var key in sheetData.DictionaryData.Keys)
                                {
                                    this.WriteContent(sheet, sheetData.DictionaryData[key]);
                                }
                            }
                            else
                            {
                                this.WriteContent(sheet, sheetData.Data);
                            }
                        }
                    }
                }

                package.SaveAs(output);
            }

            return output;
        }

        private MemoryStream WriteFileWithoutTemplate()
        {
            MemoryStream output = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                output = new MemoryStream();
                foreach (var sheetData in this.DataSheets)
                {
                    package.Workbook.Worksheets.Add(sheetData.Name);
                    var sheet = package.Workbook.Worksheets.FirstOrDefault(c => c.Name == sheetData.Name);
                    if (sheet != null)
                    {
                        if (sheetData.DictionaryData != null && sheetData.DictionaryData.Count > 0)
                        {
                            foreach (var key in sheetData.DictionaryData.Keys)
                            {
                                this.WriteContent(sheet, sheetData.DictionaryData[key]);
                            }
                        }
                        else
                        {
                            this.WriteContent(sheet, sheetData.Data);
                        }
                    }
                }

                package.SaveAs(output);
            }

            return output;
        }
        private void WriteContent(ExcelWorksheet sheet, object value)
        {
            var lastRow = 1;
            if (sheet.Dimension != null)
            {
                lastRow = sheet.Dimension.End.Row + 1;
            }
            var cellAddress = string.Format("A{0}", lastRow);
            var cell = sheet.Cells[cellAddress];
            this.WriteCellValue(cell, value);
        }
        private void WriteContentByKey(ExcelWorksheet sheet, string key, object value)
        {
            var cell = sheet.Cells.FirstOrDefault(c => c.Value != null && c.Value.ToString() == key);
            if (cell != null)
            {
                this.WriteCellValue(cell, value);
            }
        }
        private void WriteText(ExcelRangeBase cell, string value)
        {
            cell.Value = value.ToString();
        }
        private void WriteNumber(ExcelRangeBase cell, decimal value)
        {
            cell.Value = value;
            cell.Style.Numberformat.Format = "#,##0.00";
        }
        private void WriteInt(ExcelRangeBase cell, int value)
        {
            cell.Value = value.ToString(Culture);
        }
        private void WriteDateTime(ExcelRangeBase cell, DateTime value)
        {
            cell.Value = value.ToString(Culture);
        }
        private void WriteTable(ExcelRangeBase cell, DataTable value)
        {
            var dt = value;
            if (dt != null)
            {
                var sheet = cell.Worksheet;
                var startColumnIndex = cell.Start.Column;
                var startRowIndex = cell.Start.Row;
                var colHeaderIndex = startColumnIndex;
                ExcelStyle headerStyle = null;
                foreach (DataColumn col in dt.Columns)
                {
                    var tempCell = sheet.Cells[startRowIndex, colHeaderIndex];
                    if (col.Ordinal == 0)
                    {
                        headerStyle = sheet.Cells[startRowIndex, colHeaderIndex].Style;
                    }

                    this.WriteCellValue(tempCell, col.ColumnName);
                    this.SetBorderCell(tempCell, headerStyle.Border);
                    this.SetFontCell(tempCell, headerStyle.Font);
                    this.SetFillStyleCell(tempCell, headerStyle.Fill);
                    tempCell.Style.WrapText = headerStyle.WrapText;


                    colHeaderIndex++;
                }

                if (dt.Rows.Count > 0)
                {
                    var rowDataIndex = startRowIndex + 1;
                    var colDataIndex = startColumnIndex;
                    ExcelStyle rowStyle = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            var tempCell = sheet.Cells[rowDataIndex, colDataIndex];
                            this.WriteCellValue(tempCell, row[col.ColumnName]);
                            if (col.Ordinal == 0 && colDataIndex == startColumnIndex && rowDataIndex == (startRowIndex + 1))
                            {
                                rowStyle = tempCell.Style;
                            }

                            this.SetBorderCell(tempCell, rowStyle.Border);
                            this.SetFontCell(tempCell, rowStyle.Font);
                            tempCell.Style.WrapText = rowStyle.WrapText;
                            colDataIndex++;
                        }
                        colDataIndex = startColumnIndex;
                        rowDataIndex++;
                    }
                }
                else
                {
                    ///TODO: write somethings if data is empty
                }
            }
        }
        private void WriteCellValue(ExcelRangeBase cell, object value)
        {
            if (value != null)
            {
                var valueType = value.GetType();
                switch (valueType.FullName)
                {
                    case "System.DateTime": this.WriteDateTime(cell, (DateTime)value); break;
                    case "System.Int": this.WriteInt(cell, (int)value); break;
                    case "System.Decimal": this.WriteNumber(cell, (decimal)value); break;
                    case "System.Data.DataTable": this.WriteTable(cell, (DataTable)value); break;
                    default: this.WriteText(cell, value.ToString()); break;
                }
            }
        }
        private void SetBorderCell(ExcelRangeBase cell, Border defaultBorder)
        {
            cell.Style.Border.Bottom.Style = defaultBorder.Bottom.Style;
            cell.Style.Border.Left.Style = defaultBorder.Left.Style;
            cell.Style.Border.Top.Style = defaultBorder.Top.Style;
            cell.Style.Border.Right.Style = defaultBorder.Right.Style;
        }
        private void SetFontCell(ExcelRangeBase cell, ExcelFont defaultFont)
        {
            cell.Style.Font.Bold = defaultFont.Bold;
            cell.Style.Font.Italic = defaultFont.Italic;
            cell.Style.Font.Family = defaultFont.Family;
            cell.Style.Font.Size = defaultFont.Size;
            cell.Style.Font.Name = defaultFont.Name;
            cell.Style.Font.Strike = defaultFont.Strike;
            cell.Style.Font.UnderLine = defaultFont.UnderLine;
            cell.Style.Font.VerticalAlign = defaultFont.VerticalAlign;
        }
        private void SetFillStyleCell(ExcelRangeBase cell, ExcelFill defaultFill)
        {
            cell.Style.Fill.PatternType = defaultFill.PatternType;
            if (!string.IsNullOrEmpty(defaultFill.BackgroundColor.Rgb))
            {
                var backgroundColor = ColorTranslator.FromHtml("#" + defaultFill.BackgroundColor.Rgb);
                cell.Style.Fill.BackgroundColor.SetColor(backgroundColor);
            }

            if (!string.IsNullOrEmpty(defaultFill.PatternColor.Rgb))
            {
                var patternColor = ColorTranslator.FromHtml("#" + defaultFill.PatternColor.Rgb);
                cell.Style.Fill.PatternColor.SetColor(patternColor);
            }
        }
    }
}
