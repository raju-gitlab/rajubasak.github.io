using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.MODEL.Event
{
    public class EventGalleryModel : DropDownListModel
    {
        public string ImageFolderPath { get; set; }
        public HttpPostedFileBase[] UploadFiles { get; set; }
        public string EventName { get; set; }
        public string EventPlace { get; set; }
        public DateTimeOffset EventDate { get; set; }
    }
}
