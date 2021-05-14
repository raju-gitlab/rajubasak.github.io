using ApplicationTools.Configuration;
using CMS.MODEL.Notice;
using CMS.REPOSITORY.IRepository.INotice;
using CMS.UTILITIES.CreatePDF;
using CMS.UTILITIES.DateTimeHandel;
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

namespace CMS.REPOSITORY.Repository.Notice
{
    public class NoticeRepository : INoticeRepository
    {
        #region Create Text Notice
        public bool CreateNewNotice(NoticeEditModel Createnotice)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = "insert into Notice(NoticeTitle, CreatedBy, CreatedOn, NoticePdfPath, NoticeShortDescription, NoticeGuid) values(@NoticeTitle, @CreatedBy, @CreatedOn, @NoticePdfPath, @NoticeShortDescription, @NoticeGuid)";// QueryConfig.BookQuerySettings[""].ToString();
                string LogoImagePath = ConfigurationManager.AppSettings["NoticeLogoImagePath"].ToString();//UploadImage.Upload(Createnotice.UploadImageFile, ConfigurationManager.AppSettings["NoticeImageUploadPath"].ToString());
                string SignatureImagePath = UploadImage.Upload(Createnotice.UploadSignatureFile, ConfigurationManager.AppSettings["NoticeImageUploadPath"].ToString());
                string PdfPath = ConvertIntoPDF.CreateTextPdf(Createnotice.NoticeDate.ToString(),Createnotice.RefNo ,Createnotice.NoticeBody, LogoImagePath, SignatureImagePath, Createnotice.NoticeTitle);
                SqlConnection con;
                SqlCommand cmd;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    using (cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@NoticeTitle", Createnotice.NoticeTitle);
                        cmd.Parameters.AddWithValue("@CreatedBy", Createnotice.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", DateTimeOffset.UtcNow);
                        cmd.Parameters.AddWithValue("@NoticePdfPath", PdfPath);
                        cmd.Parameters.AddWithValue("@NoticeShortDescription", Createnotice.NoticeHeader);
                        cmd.Parameters.AddWithValue("@NoticeGuid", Guid.NewGuid().ToString());
                    }
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
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

        #region Get Visual Notice
        public bool CreateNewVisualNotice(VisualNoticeEditModel Createnotice)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings[""].ToString();
                string LogoImagePath = UploadImage.Upload(Createnotice.UploadImageFile, ConfigurationManager.AppSettings["NoticeImageUploadPath"].ToString());
                string OrganizationImage = UploadImage.Upload(Createnotice.OrganizationImageFile, LogoImagePath);
                string BodyImage = UploadImage.Upload(Createnotice.NoticeBodyImageFile, LogoImagePath);
                string SignatureImage = UploadImage.Upload(Createnotice.UploadSignatureFile, LogoImagePath);
                string PdfPath = ConvertIntoPDF.CreateVisualPdf(Createnotice.NoticeHeader, OrganizationImage, BodyImage, SignatureImage, Createnotice.NoticeTitle);
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@", Createnotice.NoticeTitle);
                        cmd.Parameters.AddWithValue("@", Createnotice.CreatedBy);
                        cmd.Parameters.AddWithValue("@", DateTimeOffset.UtcNow);
                        cmd.Parameters.AddWithValue("@", PdfPath);
                        cmd.Parameters.AddWithValue("@", Createnotice.NoticeShortDescription);
                        cmd.Parameters.AddWithValue("@", Guid.NewGuid().ToString());
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

        #region Get
        #region GetAllNotice
        public List<NoticeEditModel> GetNotices()
        {
            try
            {
                List<NoticeEditModel> Notices = new List<NoticeEditModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = "Select NoticeTitle,CreatedOn,NoticeShortDescription,NoticePdfPath,NoticeGuid from Notice ORDER BY CreatedOn DESC";// QueryConfig.BookQuerySettings[""].ToString();
                using(SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            Notices.Add(new NoticeEditModel {
                                 NoticeTitle = rdr["NoticeTitle"].ToString(),
                                 CreatedOn = DateTimeOffset.Parse(rdr["CreatedOn"].ToString()),
                                 MonthName = DateTimeManagement.GetMonthName(rdr["CreatedOn"].ToString()),
                                 NoticeShortDescription = rdr["NoticeShortDescription"].ToString(),
                                 NoticePdfPath = rdr["NoticePdfPath"].ToString().Substring(1),
                                 NoticeGuid = rdr["NoticeGuid"].ToString()
                            });
                        }
                        if(Notices != null)
                        {
                            return Notices;
                        }
                        else
                        {
                            return null;
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

        #region GetNoticeById
        public string ViewNotice(string NgId)
        {
            throw new NotImplementedException();
        } 
        #endregion
        #endregion
    }
}
