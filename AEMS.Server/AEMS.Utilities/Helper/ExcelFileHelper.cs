using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AEMS.Utilities.Helper
{
    public class ExcelFileHelper
    {
        public static void SetBorderStyle(ExcelRange excelRange, ExcelBorderStyle style = ExcelBorderStyle.Thin)
        {
            excelRange.Style.Border.Top.Style = style;
            excelRange.Style.Border.Left.Style = style;
            excelRange.Style.Border.Right.Style = style;
            excelRange.Style.Border.Bottom.Style = style;
        }

        public static void CreateSheetData(ExcelWorksheet worksheet, int totalRows, int totalColumns, List<object[]> dataCollection)
        {
            // Start From A1 cell
            worksheet.Cells["A1"].LoadFromArrays(dataCollection);
            // Set Header style Bold
            worksheet.Cells[1, 1, 1, totalColumns].Style.Font.Bold = true;
            // Set Border
            SetBorderStyle(worksheet.Cells[1, 1, totalRows, totalColumns]);
        }

        private static void FormatWorksheet(ExcelWorksheet worksheet, int totalRows, int totalColumns, float fontSize, string fontName, bool autoFitColmuns = true)
        {
            worksheet.Cells.Style.Font.Size = fontSize;
            worksheet.Cells.Style.Font.Name = fontName;

            var headerRange = worksheet.Cells[1, 1, 1, totalColumns];
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Font.Color.SetColor(Color.White);
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.Orange);
            headerRange.AutoFilter = false;

            if (autoFitColmuns)
            {
                // Auto fit columns for header
                headerRange.AutoFitColumns();
            }

            var excelRange = worksheet.Cells[1, 1, totalRows + 1, totalColumns];
            excelRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            excelRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            excelRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            excelRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }


        public static MemoryStream CreateExcel<T>(ExportExcelModel<T> model, bool isHideCustomColumns = false) where T : class
        {
            MemoryStream fileStream = new MemoryStream();

            using (ExcelPackage package = new ExcelPackage(fileStream))
            {
                // create sheet with name input
                var workSheet = package.Workbook.Worksheets.Add(model.SheetName);

                // Load data list to Cell start from A1
                workSheet.Cells["A1"].LoadFromCollectionFiltered(model.Data, true, isHideCustomColumns);

                // Format Worksheet
                FormatWorksheet(workSheet, model.TotalRows, model.GetTotalColumns(), model.FontSize, model.FontName);

                // Wrap text column
                _ = workSheet.WraptextCells<T>(false);

                package.Save();
            }

            fileStream.Seek(0, SeekOrigin.Begin);

            return new MemoryStream(fileStream.ToArray());
        }
    }


    public class ExportExcelModel<T> where T : class
    {
        public string SheetName { get; set; }

        public float FontSize { get; set; } = 18;

        public string FontName { get; set; } = "Times New Roman";

        public IEnumerable<T> Data { get; set; }

        public int TotalRows { get => Data.Count(); }

        public ExportExcelModel(IEnumerable<T> data, string sheetName)
        {
            SheetName = sheetName;
            Data = data;
        }

        public int GetTotalColumns()
        {
            int totals = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !Attribute.IsDefined(p, typeof(ExcelIgnore)))
                .Count();
            return totals;
        }
    }
}
