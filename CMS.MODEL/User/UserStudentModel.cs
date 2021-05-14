using CMS.MODEL.Event;
using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.MODEL.User
{
    public class UserStudentModel
    {
        public int Id { get; set; }
    }
    public class UserStudenEdittModel : DropDownListModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ParentName { get; set; }
        public string MiddleName { get; set; }
        public string Cast { get; set; }
        public string BloodGroup { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string ContactNumberOpt { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string DateOfBirth { get; set; }
        public int ZipCode { get; set; }
        public string AddmissionYear { get; set; }
        [Required, DisplayName("Upload Student Image")]
        public string StudentImagePath { get; set; }
        public HttpPostedFileBase StudentImageFile { get; set; }
        [Required, DisplayName("Upload Student Signature")]
        public string StudentSignaturePath { get; set; }
        public HttpPostedFileBase StudentSignatureFile { get; set; }
        public string StudentGuid { get; set; }
        //public string CityCode { get; set; }
    }
    public class StudentModel : DropDownListModel
    {
        public string StudentName { get; set; }
        [Required, DisplayName("Upload Student Image")]
        public string StudentImagePath { get; set; }
        public HttpPostedFileBase StudentImageFile { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public string StudentGuid { get; set; }
        public string StudentRole { get; set; }
    }
}
