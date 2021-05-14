using ApplicationTools.Configuration;
using CMS.MODEL.Addmission;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.IAddmission;
using CMS.UTILITIES.CreateExcelFile;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.Repository.Addmission
{
    public class StudentFessRepository : IStudentFessRepository
    {
        #region Get
        #region GetStudentDetailsById
        public List<StudentFessDetails> GetStudentDetails(StudentFessEditModel studentFess)
        {
            try
            {
                List<StudentFessDetails> Student = new List<StudentFessDetails>();
                string hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetStudentBySemCourseAndAddmissionYearId"].ToString();
                using(SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@CourseGuid", studentFess.StreamGuid));
                        cmd.Parameters.Add(new SqlParameter("@SemesterGuid", studentFess.SemesterGuid));
                        cmd.Parameters.Add(new SqlParameter("@Name", studentFess.StudentName));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            Student.Add(new StudentFessDetails
                            { 
                                StudentName = rdr["FirstName"].ToString(),
                                StudentImagepath = rdr["StudentImagePath"].ToString(),
                                CurrentSemester = rdr["SemesterName"].ToString(),
                                Stream = rdr["CourseName"].ToString(),
                                StudentGuid = rdr["StudentGuid"].ToString(),
                                CourseGuid = studentFess.StreamGuid,
                                SemesterGuid = studentFess.SemesterGuid,
                                FessDocumentpath = !string.IsNullOrEmpty(rdr["FessDocumentpath"].ToString()) ? rdr["FessDocumentpath"].ToString().Substring(1) : string.Empty,
                                DueFessAmount = Convert.ToInt32(rdr["DueAmount"].ToString())
                            });
                        }
                        if(Student != null)
                        {
                            return Student;
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

        #region GetStudentById
        /// <summary>
        /// This Tuple Return FessDetails And also Some Student Details.Mainly Focused on StudentFess Records
        /// </summary>
        /// <param name="StudentGuid"></param>
        /// <returns></returns>
        public Tuple<List<StudentFessDetails>, List<StudentFessEditModel>> GetStudentFeesDetails(string StudentGuid)
        {
            try
            {
                /*Tuple<StudentFessDetails, List<StudentFessEditModel>> StudentFess = new Tuple<StudentFessDetails, List<StudentFessEditModel>>()*/
                List<StudentFessDetails> student = new List<StudentFessDetails>();
                List<StudentFessEditModel> fessDetails = new List<StudentFessEditModel>();
                string hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostname].ConnectionString;
                SqlConnection con;
                SqlCommand cmd;
                SqlDataReader rdr;
                string query = QueryConfig.BookQuerySettings["GetStudentFDetails"].ToString();
                string query_ = QueryConfig.BookQuerySettings["GetStudentFessDetails"].ToString();
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    using(cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@StudentGuid", StudentGuid));
                        rdr = cmd.ExecuteReader();
                        if(rdr.Read())
                        {
                            student.Add(new StudentFessDetails
                            {
                                StudentName = rdr["FirstName"].ToString(),
                                Stream = rdr["CourseName"].ToString(),
                                AddmissionYear = rdr["AddmissionYear"].ToString(),
                                StudentImagepath = rdr["StudentImagepath"].ToString(),
                                CurrentSemester = rdr["SemesterName"].ToString(),
                                StudentGuid = rdr["StudentGuid"].ToString(),
                                SemesterGuid = rdr["SemesterGuid"].ToString(),
                                CourseGuid = rdr["Courseguid"].ToString()
                            });
                        }
                        if(student != null)
                        {
                            con.Close();
                            rdr.Close();
                            using(cmd = new SqlCommand(query_,con))
                            {
                                con.Open();
                                cmd.Parameters.Add(new SqlParameter("@StudentGuid", StudentGuid));
                                rdr = cmd.ExecuteReader();
                                while(rdr.Read())
                                {
                                    fessDetails.Add(new StudentFessEditModel
                                    {
                                        SemesterName = rdr["SemesterName"].ToString(),
                                        TotalAmount = Convert.ToInt32(rdr["PerSemesterFess"]),
                                        PaidFess = Convert.ToInt32(rdr["Paid"]),
                                        DueFessAmount = Convert.ToInt32(rdr["DueFess"]),
                                        FineAmount = Convert.ToInt32(rdr["FineAmount"]),
                                        FessPaidDate = DateTimeOffset.Parse(rdr["FessPaidDate"].ToString())
                                    });
                                }
                            }
                            if(fessDetails != null)
                            {
                                return new Tuple<List<StudentFessDetails>, List<StudentFessEditModel>>(student, fessDetails);
                            }
                            else
                            {
                                return null;
                            }
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

        #region GetStudentFess

        #endregion
        #endregion

        #region Put
        #region UpdateFess
        public bool UpdateFess(StudentFessEditModel studentFess)
        {
            try
            {
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["AddStudentFess"].ToString();
                string query_ = QueryConfig.BookQuerySettings["GetstudentFessPathAndDetails"].ToString();
                SqlConnection con;
                SqlCommand cmd;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    using (cmd = new SqlCommand(query_, con)) 
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@StudentGuid", studentFess.StudentGuid));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.Read()) 
                        {
                            studentFess.FessDocumentpath = rdr["FessDocumentpath"].ToString();
                            studentFess.Course = rdr["CourseName"].ToString();
                            studentFess.Semester = rdr["SemesterName"].ToString();
                            studentFess.TotalAmount = Convert.ToInt32(rdr["PerSemesterFess"].ToString());
                            studentFess.DueFessAmount = Convert.ToInt32(rdr["DueAmount"].ToString());
                            studentFess.FineAmount = Convert.ToInt32(rdr["FineAmount"].ToString());
                        }
                        if (studentFess.FessDocumentpath != null && studentFess.FessDocumentpath != string.Empty) 
                        {
                            rdr.Close();
                            using (cmd = new SqlCommand(query, con))
                            {
                                cmd.CommandType = CommandType.Text;
                                int DueAmount = studentFess.TotalAmount - studentFess.PaidFess;
                                int TotalDueAmount = DueAmount + studentFess.DueFessAmount;
                                cmd.Parameters.Add(new SqlParameter("@StudentGuid", studentFess.StudentGuid));
                                cmd.Parameters.AddWithValue("@FineAmount", 0);
                                cmd.Parameters.AddWithValue("@DueAmount", TotalDueAmount);
                                if (cmd.ExecuteNonQuery() > 0)
                                {
                                    ModifyExistingExcel.UpdateStudentFessExcelFile(studentFess.FessDocumentpath, studentFess, DueAmount);
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
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

        #region UpdateFine
        public List<StudentFineModel> UpdateFine(StudentFineModel studentFess)
        {
            try
            {
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetStudentFineAmount"].ToString();
                List<StudentFineModel> studentsFessDetails = new List<StudentFineModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@FirstName", studentFess.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@LastName", !string.IsNullOrEmpty(studentFess.LastName) ? studentFess.LastName : string.Empty));
                        cmd.Parameters.Add(new SqlParameter("@CrsGuid", studentFess.StreamGuid));
                        cmd.Parameters.Add(new SqlParameter("@SemGuid", studentFess.SemesterGuid));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            studentsFessDetails.Add(new StudentFineModel
                            {
                                FirstName = rdr["FirstName"].ToString(),
                                LastName = rdr["LastName"].ToString(),
                                StudentImagePath = rdr["StudentImagePath"].ToString().Substring(1),
                                FineAmount1 = Convert.ToInt32(rdr["LibraryFineAmount"].ToString()),
                                FineAmount2 = Convert.ToInt32(rdr["FessFineAmount"].ToString()),
                                StudentGuid = rdr["StudentGuid"].ToString(),
                                TotalFineAmount = (Convert.ToInt32(rdr["LibraryFineAmount"].ToString()) + Convert.ToInt32(rdr["FessFineAmount"].ToString())) ,
                                Course = rdr["CourseName"].ToString(),
                                Semester = rdr["SemesterName"].ToString()
                            });
                        }
                        if(studentsFessDetails.Count != 0)
                        {
                            return studentsFessDetails;
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

        #region UpdateFine
        public bool DepositFine(StudentFineModel SID)
        {
            try
            {

                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetStudentFessDocumentPath"].ToString();
                string query_ = QueryConfig.BookQuerySettings["DepositPaidFine"].ToString();
                StudentFineModel fess = new StudentFineModel();
                SqlConnection con;
                SqlCommand cmd;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    using (cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@StudentGuid", SID.StudentGuid));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if(rdr.Read())
                        {
                            fess.FineAmount1 = Convert.ToInt32(rdr["LibraryFineAmount"].ToString());
                            fess.FineAmount2 = Convert.ToInt32(rdr["FessFineAmount"].ToString());
                            fess.StudentImagePath = rdr["FessDocumentPath"].ToString();
                        }
                    }
                    if (fess != null)
                    {
                        using (cmd = new SqlCommand(query_, con))
                        {
                            var CalculateDue = (fess.FineAmount1 + fess.FineAmount2) - SID.PaidAmount;
                            cmd.Parameters.Add(new SqlParameter("@StudentGuid", SID.StudentGuid));
                            cmd.Parameters.AddWithValue("@FineAmount", CalculateDue);
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                ModifyExistingExcel.UpdateStudentFineExcelFile(fess.StudentImagePath, "Fine", SID.PaidAmount, CalculateDue);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
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
        #endregion
    }
}