using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.MODEL.Master
{
    public class TeachersModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
    }
    public class CourseTeacherModel : DropDownListModel
    {
        public string SubjectName { get; set; }
        public string SocialLink1 { get; set; }
        public string SocialLink2 { get; set; }
        public string SocialLink3 { get; set; }
        public string SocialLink4 { get; set; }
        public string ImagePath { get; set; }
        public DateTimeOffset JoiningDate { get; set; }
    }
    public class TeachersEditModel : CourseTeacherModel
    {
        public string Biography { get; set; }
        public string CourseName { get; set; }
        public string SubejctName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public string Interests { get; set; }
        public HttpPostedFileBase UploadFile { get; set; }
    }
    public class ResignteacherModel
    {
        public string TeacherName { get; set; }
        public string TeacherGuid { get; set; }
        public DateTimeOffset ResignDate { get; set; }
    }
}
