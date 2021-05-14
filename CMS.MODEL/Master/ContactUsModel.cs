using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.MODEL.Master
{
    public class ContactUsModel : DropDownListModel
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string ReportSubject { get; set; }
        public string ReportDetails { get; set; }
        public HttpPostedFileBase UploadFile { get; set; }
        public string ReportImagePath { get; set; }
        public string ReportGuid { get; set; }
        public bool IsRead { get; set; }
    }
}
