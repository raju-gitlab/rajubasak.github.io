using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.MODEL.Master
{
    public class ImageGalleryModel
    {
        public int Id { get; set; }
        public int ImageeTypeId { get; set; }
    }
    public class ImageGalleryEditModel
    {
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string ImageDescription { get; set; }
        public int LikesCount { get; set; }
        public int DislikeCount { get; set; }
        public string ImageGuid { get; set; }
        public bool IsDeleted { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBase UploadFile { get; set; }
    }
    public class ImageTypeModel
    {
        public int Id { get; set; }
    }
    public class ImageTypeEditModel
    {
        public string ImageType { get; set; }
        public string TypeId { get; set; }
    }
}
