using CMS.MODEL.User;
using CMS.REPOSITORY.IRepository.IUser;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.Repository.User
{
    public class UserStudentsRepository : IUserStudentsRepository
    {
        #region Get
        #region GetStudentDetailsByStudentId
        public UserStudenEdittModel GetSpecificStudentStudent(string StudentId)
        {
            try
            {
                UserStudenEdittModel student = new UserStudenEdittModel();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ToString();
                string query = "";
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@StudentId", StudentId));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if(rdr.Read())
                        {
                            student.FirstName = rdr["FirstName"].ToString();
                            student.LastName = rdr["LastName"].ToString();
                            student.Stream = rdr["Stream"].ToString();
                            student.DateOfBirth = rdr["DateOfBirth"].ToString();
                            student.AddmissionYear = rdr["AddmissionYear"].ToString();
                            student.City = rdr["City"].ToString();
                            student.State = rdr["State"].ToString();
                            student.ContactNumber = rdr["ContactNumber"].ToString();
                            student.ContactNumberOpt = rdr["ContactNumberOpt"].ToString();
                            student.ParentName = rdr["ParentName"].ToString();
                            student.AddressLine1 = rdr["AddressLine1"].ToString();
                            student.AddressLine2 = rdr["AddressLine2"].ToString();
                            student.StudentImagePath = rdr["ImagePath"].ToString();
                        }
                        if(student != null)
                        {
                            return student;
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

        #region GetStudents
        /// <summary>
        /// This Section Of Code Will show all the Students of all stream are available in the College,Previous Year
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        #region GetAllStudentsListBySemester and depeartment wise
        public List<StudentModel> GetStudentsList(StudentModel students)
        {
            try
            {
                List<StudentModel> student = new List<StudentModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ToString();
                string query = "";
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Semester", students.Semester));
                        cmd.Parameters.Add(new SqlParameter("@Stream", students.Stream));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            student.Add(new StudentModel { 
                                StudentName = Convert.ToString(rdr["FirstName"].ToString() + rdr["LastName"]),
                                Stream = rdr["Stream"].ToString(),
                                Semester = rdr["Semester"].ToString()
                            });
                        }
                        if(student != null)
                        {
                            return student;
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
    }
}
