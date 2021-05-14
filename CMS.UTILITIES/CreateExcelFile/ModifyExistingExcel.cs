using CMS.MODEL.Addmission;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.UTILITIES.CreateExcelFile
{
    public class ModifyExistingExcel
    {
        public static void UpdateStudentFessExcelFile(string ExcelFilePath, StudentFessEditModel FessDetails, int DueAmount)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            string filepat = HttpContext.Current.Server.MapPath(ExcelFilePath);
            FileInfo file = new FileInfo(filepat);
            using (ExcelPackage excelpk = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = excelpk.Workbook.Worksheets[0];
                int j = worksheet.Dimension.End.Row;
                worksheet.Cells[j + 1, 1].Value = FessDetails.StudentName;
                worksheet.Cells[j + 1, 2].Value = FessDetails.Course;
                worksheet.Cells[j + 1, 3].Value = FessDetails.Semester;
                worksheet.Cells[j + 1, 4].Value = "SemesterFess";
                worksheet.Cells[j + 1, 5].Value = DateTime.Now.ToShortDateString();
                worksheet.Cells[j + 1, 6].Value = FessDetails.PaidFess;
                worksheet.Cells[j + 1, 7].Value = DueAmount;
                excelpk.Save();
            }
        }
        public static void UpdateStudentFineExcelFile(string ExcelFilePath, string FineType ,int DueAmount,int FinePaidAmount)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            string filepat = HttpContext.Current.Server.MapPath(ExcelFilePath);
            FileInfo file = new FileInfo(filepat);
            using (ExcelPackage excelpk = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = excelpk.Workbook.Worksheets[0];
                int j = worksheet.Dimension.End.Row;
                worksheet.Cells[j + 1, 4].Value = FineType;
                worksheet.Cells[j + 1, 5].Value = DateTime.Now.ToShortDateString();
                worksheet.Cells[j + 1, 9].Value = DueAmount;
                worksheet.Cells[j + 1, 10].Value = FinePaidAmount;
                excelpk.Save();
            }
        }
    }
}
