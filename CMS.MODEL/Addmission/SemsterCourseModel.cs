using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.MODEL.Addmission
{
    public class OldCourseModel
    {
        public string CourseName { get; set; }
        public string CourseGuid { get; set; }
    }
    public class SemesterModel
    {
        public string SemesterName { get; set; }
        public string SemesterGuid { get; set; }
        public int MyProperty { get; set; }
    }
}
