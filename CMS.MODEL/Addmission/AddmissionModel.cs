using CMS.MODEL.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.MODEL.Addmission
{
    public class AddmissionModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
    }
    public class AddmissionEditModel : UserStudenEdittModel
    {
        public string[] QualiFication { get; set; }
        public string[] Board { get; set; }
        public float[] Percentage { get; set; }
        public int[] TotalNumber { get; set; }
        public string[] DocumentPath { get; set; }
        public HttpPostedFileBase[] Fileupload { get; set; }
        public string Fess { get; set; }
    }
}
