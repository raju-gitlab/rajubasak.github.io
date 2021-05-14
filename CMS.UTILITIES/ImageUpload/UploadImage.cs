using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.UTILITIES.ImageUpload
{
    public class UploadImage
    {
        public static string Upload(HttpPostedFileBase FileUpload,string ImagePath)
        {
            try
            {
                string guid = Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower().Substring(0,5);
                string ImageName = guid + FileUpload.FileName;
                string Name = Path.GetFileName(FileUpload.FileName);
                string FIleName = string.Concat(ImagePath, ImageName);
                FileUpload.SaveAs(System.Web.HttpContext.Current.Server.MapPath(FIleName));
                return FIleName;
            }
            catch (Exception ex)
            {
                LogManager.LogManager.ErrorEntry(ex);
                throw;
            }
        }
        public static string UploadMultiple(HttpPostedFileBase[] FileUpload, string ImagePath)
        {
            try
            {
                for(int i = 0; i < FileUpload.Length;i++)
                {
                    string guid = Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower().Substring(0, 5);
                    string ImageName = guid + FileUpload[i].FileName;
                    string Name = Path.GetFileName(FileUpload[i].FileName);
                    string FIleName = string.Concat(ImagePath, ImageName);
                    FileUpload[i].SaveAs(System.Web.HttpContext.Current.Server.MapPath(FIleName));
                }
                return ImagePath;
            }
            catch (Exception ex)
            {
                LogManager.LogManager.ErrorEntry(ex);
                throw;
            }
        }
        public static string UploadFile(HttpPostedFileBase FileUpload, string ImagePath, string FileName)
        {
            try
            {
                string guid = Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower().Substring(0, 5);
                string ImageName = FileName;
                string Name = Path.GetFileName(FileUpload.FileName);
                string FIleName = string.Concat(ImagePath, FileUpload.FileName);
                FileUpload.SaveAs(System.Web.HttpContext.Current.Server.MapPath(FIleName));
                return FIleName;
            }
            catch (Exception ex)
            {
                LogManager.LogManager.ErrorEntry(ex);
                throw;
            }
        }
    }
}
