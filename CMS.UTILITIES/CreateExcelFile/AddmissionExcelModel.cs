using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.UTILITIES.CreateExcelFile
{
    public class ExcelFessDetails
    {
        public string FessType { get; set; }
        public string FessDepositDate { get; set; }
        public string FessAmount { get; set; }
        public int DueAmount { get; set; }
    }
    public class GetGuidDetailsModel : ExcelFessDetails
    {
        public string SemesterName { get; set; }
        public string CourseName { get; set; }
    }
    public class AddmissionExcelModel : GetGuidDetailsModel
    {
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Qualification { get; set; }
        public string Board { get; set; }
        public string TotalNumber { get; set; }
        public string Percentage { get; set; }
    }
}
