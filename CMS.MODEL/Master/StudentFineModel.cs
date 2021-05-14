using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.MODEL.Master
{
    public class StudentFineModel : DropDownListModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int FineAmount1 { get; set; }
        public int FineAmount2 { get; set; }
        public string StudentGuid { get; set; }
        public string StudentImagePath { get; set; }
        public int TotalFineAmount { get; set; }
        public int PaidAmount { get; set; }
    }
}
