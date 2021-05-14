using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.MODEL.Master
{
    public class CourseModel
    {
        public int Id { get; set; }
    }
    public class CourseEditModel : DropDownListModel
    {
        public string CourseName { get; set; }
        public string CourseDuration { get; set; }
        public int PerSemesterFess { get; set; }
        public int TotalSemester { get; set; }
        public int FullCourseFess { get; set; }
        public string CourseDetails { get; set; }
        public string CourseRequirements { get; set; }
        public string CourseImagePath { get; set; }
        public HttpPostedFileBase UploadImage { get; set; }
        public string CourseType { get; set; }
    }
    public class CourseSemesterModel : BaseModel
    {
        public string SemesterName { get; set; }
        public string SemmesterGuid { get; set; }
    }
}
