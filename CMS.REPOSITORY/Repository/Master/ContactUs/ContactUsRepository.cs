using ApplicationTools.Configuration;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.IMaster.IContactUs;
using CMS.UTILITIES.FolderCeration;
using CMS.UTILITIES.ImageUpload;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.Repository.Master.ContactUs
{
    public class ContactUsRepository : IContactUsRepository
    {
        #region Post
        #region AddNewReport
        public bool CreateReport(ContactUsModel contactUs)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["ContactUsReportCreate"].ToString();
                string guid = Guid.NewGuid().ToString().ToUpper();
                string Path = CreateFolder.UploadEventImagePath(guid, ConfigurationManager.AppSettings["ReportFolderPath"].ToString());
                string ImagePath = UploadImage.Upload(contactUs.UploadFile, Path);
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@CustomerName", contactUs.CustomerName);
                        cmd.Parameters.AddWithValue("@CustomerEmail", contactUs.CustomerEmail);
                        cmd.Parameters.AddWithValue("@ReportSubject", contactUs.ReportSubject);
                        cmd.Parameters.AddWithValue("@ReportDetails", contactUs.ReportDetails);
                        cmd.Parameters.AddWithValue("@CreatedBy", contactUs.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", DateTimeOffset.UtcNow);
                        cmd.Parameters.AddWithValue("@ReportImagePath", ImagePath);
                        cmd.Parameters.AddWithValue("@IsRead", false);
                        cmd.Parameters.AddWithValue("@ReportGuid", guid);
                        if(cmd.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion
        #endregion
    }
}
