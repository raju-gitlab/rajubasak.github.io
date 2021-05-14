using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.MODEL.Master
{
    public class DropDownListModel : BaseModel
    {
        public string Subejct { get; set; }
        public string SubejctGuid { get; set; }
        public string Course { get; set; }
        public string CourseGuid { get; set; }
        public string Stream { get; set; }
        public string StreamGuid { get; set; }
        public string Semester { get; set; }
        public string SemesterGuid { get; set; }
        public string State { get; set; }
        public string StateGuid { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public string CityGuid { get; set; }
        public string Country { get; set; }
        public string CountryGuid { get; set; }
        public string Gender { get; set; }
        public string GenderGuid { get; set; }
        public string StudentRegId { get; set; }
        public string BookName { get; set; }
        public string BookGuid { get; set; }
        public string RoleName { get; set; }
        public string RoleGuid { get; set; }
        public string TeacherName { get; set; }
        public string TeacherGuid { get; set; }
    }
}
