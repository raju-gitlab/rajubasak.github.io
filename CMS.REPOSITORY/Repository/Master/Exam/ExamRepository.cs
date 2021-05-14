using ApplicationTools.Configuration;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.IMaster.IExam;
using CMS.UTILITIES.CreatePDF;
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

namespace CMS.REPOSITORY.Repository.Master.Exam
{
    public class ExamRepository : IExamRepository
    {
        #region Get
        #region ExamPaper
        #region GetExamPaperByExamAnd Year Id
        public List<ExamPaperModel> GetExamPaper(DropDownListModel data)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ToString();
                string Query = QueryConfig.BookQuerySettings["GetExamPaperByExamId"].ToString();
                List<ExamPaperModel> ListExamPaper = new List<ExamPaperModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Value", data.Value));
                        cmd.Parameters.Add(new SqlParameter("@SemesterGuid", data.SemesterGuid));
                        cmd.Parameters.Add(new SqlParameter("@StreamGuid", data.StreamGuid));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            ListExamPaper.Add(new ExamPaperModel
                            {
                                PaperName = rdr["PaperName"].ToString(),
                                PaperDestinationpath = rdr["PaperDestinationpath"].ToString().Substring(1),
                                Stream = rdr["CourseName"].ToString(),
                                Semester = rdr["SemesterName"].ToString(),
                                ExamYear = Convert.ToInt32(rdr["ExamYear"].ToString())
                            });
                        }
                        if (ListExamPaper != null)
                        {
                            return ListExamPaper;
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
        #endregion
        #endregion

        #region Post
        #region Exampaper
        #region UploadPaper
        public bool UploadPreviousYearQuestionpaper(ExamPaperModel Paper)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["AddNewExamPaper"].ToString();
                string Path = Paper.StreamGuid.ToUpper() == "3A2E8503-9B1D-4D8E-8CC8-25AD02C8F18E" ? ConfigurationManager.AppSettings["BCAQUESTIONUploadPath"].ToString() : Paper.StreamGuid == "0587A27E-3894-434C-A473-A9B0CAF88CC8" ? ConfigurationManager.AppSettings["BBAQUESTIONUploadPath"].ToString() : Paper.StreamGuid == "6DBF6FB4-B4D3-4960-8D1C-1C0DA3E925F2" ? ConfigurationManager.AppSettings["BHMQUESTIONUploadPath"].ToString() : ConfigurationManager.AppSettings["BHSMQUESTIONUploadPath"].ToString();
                string PaperDestinationFolderpath = CreateFolder.UploadEventImagePath("QuestionPaper" + Paper.ExamYear, Path);
                string PaperDestinationpath = Paper.UploadExamPaperPDF != null ? UploadImage.UploadFile(Paper.UploadExamPaperPDF, PaperDestinationFolderpath, Paper.PaperName + Paper.ExamYear) : ConvertIntoPDF.CreateExamPaperPdf(Paper.UploadExamPaperImages, Paper.PaperName + Paper.ExamYear, PaperDestinationFolderpath);
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@SemesterGuid", Paper.SemesterGuid));
                        cmd.Parameters.Add(new SqlParameter("@StreamGuid", Paper.StreamGuid));
                        cmd.Parameters.AddWithValue("@PaperName", Paper.PaperName);
                        cmd.Parameters.AddWithValue("@PaperDestinationPath", PaperDestinationpath);
                        cmd.Parameters.AddWithValue("@CreatedBy", Paper.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", DateTimeOffset.UtcNow);
                        cmd.Parameters.AddWithValue("@SerialId", Guid.NewGuid().ToString().ToUpper());
                        cmd.Parameters.AddWithValue("@ExamYear", Paper.ExamYear);
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
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion
        #endregion
        #endregion
    }
}
