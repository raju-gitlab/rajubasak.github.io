using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.MODEL.Master
{
    public class ExamPaperModel : DropDownListModel
    {
        public string PaperName { get; set; }
        public int ExamYear { get; set; }
        public string PaperDestinationpath { get; set; }
        public string PaperSerialId { get; set; }
        public string UplaodImage { get; set; }
        public HttpPostedFileBase[] UploadExamPaperImages { get; set; }
        public string UploadPDF { get; set; }
        public HttpPostedFileBase UploadExamPaperPDF { get; set; }
    }
}
