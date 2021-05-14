using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.MODEL.Addmission
{
    public class StudentFessModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
    }
    public class StudentFessEditModel : DropDownListModel
    {
        public string StudentName { get; set; }
        public string SurName { get; set; }
        public string StudentGuid  { get; set; }
        public string CourseName { get; set; }
        public string SemesterName { get; set; }
        public int TotalAmount { get; set; }
        public int DueFessAmount { get; set; }
        public int PaidFess { get; set; }
        public int FineAmount { get; set; }
        public DateTimeOffset FessPaidDate { get; set; }
        public string AddmissionYear { get; set; }
        public string FessDocumentpath { get; set; }
    }
    public class StudentFessDetails : StudentFessEditModel
    {
        public string CurrentSemester { get; set; }
        public string StudentImagepath { get; set; }
    }
    public class StudentCardModel : ItemCode
    {
        public string StudentName { get; set; }
        public string StudentImagePath { get; set; }
        public string Stream { get; set; }
        public string StudentGuid { get; set; }
    }
}
