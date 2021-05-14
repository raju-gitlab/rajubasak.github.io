using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.MODEL.Notice
{
    public class NoticeModel
    {
        public int Id { get; set; }
    }
    public class NoticeEditModel : BaseModel
    {
        public string NoticeTitle { get; set; }
        public string NoticeHeader { get; set; }
        public string NoticeBody { get; set; }
        public string OrganizationImagePath { get; set; }
        [DisplayName("Organization Image")]
        public HttpPostedFileBase UploadImageFile { get; set; }
        public string SignatureImagePath { get; set; }
        public HttpPostedFileBase UploadSignatureFile { get; set; }
        public string NoticeGuid { get; set; }
        public string NoticePdfPath { get; set; }
        public string NoticeShortDescription { get; set; }
        public string RefNo { get; set; }
        public DateTimeOffset NoticeDate { get; set; }
    }

    public class VisualNoticeEditModel : BaseModel
    {
        public string NoticeTitle { get; set; }
        public string NoticeHeader { get; set; }
        public string NoticeBodyImagePath { get; set; }
        public HttpPostedFileBase NoticeBodyImageFile { get; set; }
        public string OrganizationImagePath { get; set; }
        public HttpPostedFileBase OrganizationImageFile { get; set; }
        public HttpPostedFileBase UploadImageFile { get; set; }
        public string SignatureImagePath { get; set; }
        public HttpPostedFileBase UploadSignatureFile { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string NoticeGuid { get; set; }
        public string NoticePdfPath { get; set; }
        public string NoticeShortDescription { get; set; }
    }
}
