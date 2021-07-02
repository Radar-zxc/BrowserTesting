using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace BrowserTesting.ExcelWorking
{
    public static class ExcelInfo
    {
        public static void Add_Fill_Delete()
        {
            Excel.Application application = null;
            Excel.Workbooks books = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            try
            {
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                string filePath = projectPath + "ExcelWorking\\" + $"file.xlsx";
                application = new Excel.Application();
                books = application.Workbooks;
                application.Visible = true;
                workbook = application.Workbooks.Add();
                application.Visible = true;
                worksheet = (Worksheet)application.Sheets[1];
                worksheet.Cells[1, 2] = "Hello WorldHello WorldHello World '\n' Hello World";
                worksheet.Cells[2, 1] = "Again Hello World";
                worksheet.Rows.AutoFit();
                worksheet.Columns.AutoFit();
                workbook.Sheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);
                worksheet = (Worksheet)application.Sheets[workbook.Sheets.Count];
                worksheet.Cells[3, 3] = "Hello World";
                worksheet.Cells[3, 6] = "Again Hello World";
                worksheet.Delete();
                if (System.IO.File.Exists(filePath))
                {
                    workbook.SaveCopyAs(filePath);
                }
                else
                {
                    workbook.SaveAs(filePath);
                }
            }
            finally
            {
                application.Quit();
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(books);
                Marshal.ReleaseComObject(application);
            }
        }
    }
}
