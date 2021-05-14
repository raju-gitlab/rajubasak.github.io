using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.UTILITIES.FolderCeration
{
    public class CreateFolder
    {
        /// <summary>
        /// This Function create a SubFolder Under Predefined RootFolder.But this folder also called a root folder for User.
        /// </summary>
        public static string ImageRootFolder(string FolderName)
        {
            try
            {
                string FolderPath = ConfigurationManager.AppSettings["StudentImageUploadPath"].ToString();
                string Path = FolderPath + FolderName;
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(Path));
                }
                return Path;
            }
            catch (Exception ex)
            {
                LogManager.LogManager.ErrorEntry(ex);
                throw;
            }
        }

        public static string DocumentRootFolder(string FolderName)
        {
            try
            {
                string FolderPath = ConfigurationManager.AppSettings["StudentDocumentUploadPath"].ToString();
                string Path = @FolderPath + FolderName;
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                return Path;
            }
            catch (Exception ex)
            {
                LogManager.LogManager.ErrorEntry(ex);
                throw;
            }
        }
        public static string UploadEventImagePath(string FolderName, string FolderPath)
        {
            try
            {
                string Path = FolderPath + FolderName;
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(Path));
                }
                return Path + "/";
            }
            catch (Exception ex)
            {
                LogManager.LogManager.ErrorEntry(ex);
                throw;
            }
        }
    }
}
