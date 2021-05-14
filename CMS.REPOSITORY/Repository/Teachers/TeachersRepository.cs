using ApplicationTools.Configuration;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.ITeachers;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.Repository.Teachers
{
    public class TeachersRepository : ITeachersRepository
    {
        #region Get
        #region HomePageContents
        #region TeacherSection
        public List<CourseTeacherModel> HomePageTeacherContents()
        {
            try
            {
                List<CourseTeacherModel> courseTeachers = new List<CourseTeacherModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["HomePageTeacherContent"].ToString();
                using(SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            courseTeachers.Add(new CourseTeacherModel {
                                TeacherName = rdr["TeacherName"].ToString(),
                                RoleName = rdr["RoleName"].ToString(),
                                SocialLink1 = !string.IsNullOrEmpty(rdr["SocialLink1"].ToString()) ? rdr["SocialLink1"].ToString() : string.Empty,
                                SocialLink2 = !string.IsNullOrEmpty(rdr["SocialLink2"].ToString()) ? rdr["SocialLink2"].ToString() : string.Empty,
                                SocialLink3 = !string.IsNullOrEmpty(rdr["SocialLink3"].ToString()) ? rdr["SocialLink3"].ToString() : string.Empty,
                                SocialLink4 = !string.IsNullOrEmpty(rdr["SocialLink4"].ToString()) ? rdr["SocialLink4"].ToString() : string.Empty,
                                ImagePath = rdr["TeacherImagePath"].ToString().Substring(1)
                            });
                        }
                        if(courseTeachers.Count != 0)
                        {
                            return courseTeachers;
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

        #region GetAllTeachers
        public Tuple<List<CourseTeacherModel>, List<TeachersEditModel>> ListTeachers()
        {
            try
            {
                List<CourseTeacherModel> ListTeachers = new List<CourseTeacherModel>();
                List<TeachersEditModel> ListCourseTeachers = new List<TeachersEditModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["GetListOfCollegeTeachers"].ToString();
                string query_ = QueryConfig.BookQuerySettings["GetListOfCollegeTeachersByHisCorrespondCourse"].ToString();
                SqlConnection con;
                SqlCommand cmd;
                SqlDataReader rdr;
                using(con = new SqlConnection(CS))
                {
                    con.Open();
                    using(cmd = new SqlCommand(Query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            ListTeachers.Add(new CourseTeacherModel
                            {
                                TeacherName = rdr["TeacherName"].ToString(),
                                TeacherGuid = rdr["TeacherGuid"].ToString(),
                                ImagePath = rdr["TeacherImagePath"].ToString(),
                                SocialLink1 = !string.IsNullOrEmpty(rdr["SocialLink1"].ToString()) ? rdr["SocialLink1"].ToString() : string.Empty,
                                SocialLink2 = !string.IsNullOrEmpty(rdr["SocialLink2"].ToString()) ? rdr["SocialLink2"].ToString() : string.Empty,
                                SocialLink3 = !string.IsNullOrEmpty(rdr["SocialLink3"].ToString()) ? rdr["SocialLink3"].ToString() : string.Empty,
                                SocialLink4 = !string.IsNullOrEmpty(rdr["SocialLink4"].ToString()) ? rdr["SocialLink4"].ToString() : string.Empty,
                                RoleName = rdr["RoleName"].ToString()
                            });
                        }
                        if(ListTeachers.Count != 0)
                        {
                            rdr.Close();
                            using (cmd = new SqlCommand(query_,con))
                            {
                                cmd.CommandType = CommandType.Text;
                                rdr = cmd.ExecuteReader();
                                while(rdr.Read())
                                {
                                    ListCourseTeachers.Add(new TeachersEditModel {
                                        TeacherName = rdr["TeacherName"].ToString(),
                                        TeacherGuid = rdr["TeacherGuid"].ToString(),
                                        ImagePath = rdr["TeacherImagePath"].ToString().Substring(1),
                                        SocialLink1 = !string.IsNullOrEmpty(rdr["SocialLink1"].ToString()) ? rdr["SocialLink1"].ToString() : string.Empty,
                                        SocialLink2 = !string.IsNullOrEmpty(rdr["SocialLink2"].ToString()) ? rdr["SocialLink2"].ToString() : string.Empty,
                                        SocialLink3 = !string.IsNullOrEmpty(rdr["SocialLink3"].ToString()) ? rdr["SocialLink3"].ToString() : string.Empty,
                                        SocialLink4 = !string.IsNullOrEmpty(rdr["SocialLink4"].ToString()) ? rdr["SocialLink4"].ToString() : string.Empty,
                                        RoleName = rdr["RoleName"].ToString(),
                                        CourseName = !string.IsNullOrEmpty(rdr["CourseName"].ToString()) ? rdr["CourseName"].ToString() : string.Empty
                                    });
                                }
                                if(ListCourseTeachers.Count != 0)
                                {
                                    return new Tuple<List<CourseTeacherModel>, List<TeachersEditModel>>(ListTeachers, ListCourseTeachers);
                                }
                                else
                                {
                                    return null;
                                }
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

        #region GetTeacherDetails
        public Tuple<TeachersEditModel, List<ItemCode>, List<CourseEditModel>> Teacher_SingleDetails(string TeacherGuid)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["GetListOfCollegeTeachersById"].ToString();
                string Query_ = QueryConfig.BookQuerySettings["GetListOfCollegeTeachersCourseDetailsById"].ToString();
                string query = QueryConfig.BookQuerySettings["GetTop3CourseList"].ToString();
                TeachersEditModel teacherDetails = new TeachersEditModel();
                List<ItemCode> items = new List<ItemCode>();
                List<CourseEditModel> Courses = new List<CourseEditModel>();
                SqlConnection con;
                SqlCommand cmd;
                SqlDataReader rdr;
                using(con = new SqlConnection(CS))
                {
                    con.Open();
                    using(cmd = new SqlCommand(Query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@TeacherGuid", TeacherGuid));
                        rdr = cmd.ExecuteReader();
                        if(rdr.Read())
                        {
                            teacherDetails.TeacherName = rdr["TeacherName"].ToString();
                            teacherDetails.TeacherGuid = rdr["TeacherGuid"].ToString();
                            teacherDetails.SocialLink1 = !string.IsNullOrEmpty(rdr["SocialLink1"].ToString()) ? rdr["SocialLink1"].ToString() : string.Empty;
                            teacherDetails.SocialLink2 = !string.IsNullOrEmpty(rdr["SocialLink2"].ToString()) ? rdr["SocialLink2"].ToString() : string.Empty;
                            teacherDetails.SocialLink3 = !string.IsNullOrEmpty(rdr["SocialLink3"].ToString()) ? rdr["SocialLink3"].ToString() : string.Empty;
                            teacherDetails.SocialLink4 = !string.IsNullOrEmpty(rdr["SocialLink4"].ToString()) ? rdr["SocialLink4"].ToString() : string.Empty;
                            teacherDetails.ImagePath = rdr["TeacherImagePath"].ToString().Substring(1);
                            teacherDetails.EmailId = rdr["EmailId"].ToString();
                            teacherDetails.PhoneNumber = rdr["PhoneNumber"].ToString();
                            teacherDetails.RoleName = rdr["RoleName"].ToString();
                            teacherDetails.Biography = !string.IsNullOrEmpty(rdr["Biography"].ToString()) ? rdr["Biography"].ToString() : string.Empty;
                        }
                        if(teacherDetails != null)
                        {
                            rdr.Close();
                            using(cmd = new SqlCommand(Query_,con))
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.Add(new SqlParameter("@TeacherGuid", TeacherGuid));
                                rdr = cmd.ExecuteReader();
                                while(rdr.Read())
                                {
                                    items.Add(new ItemCode
                                    {
                                        Value = rdr["SubectName"].ToString()
                                    });
                                }
                            }
                            if(items != null)
                            {
                                rdr.Close();
                                using(cmd = new SqlCommand(query,con))
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Parameters.Add(new SqlParameter("@TeacherGuid", TeacherGuid));
                                    rdr = cmd.ExecuteReader();
                                    while(rdr.Read())
                                    {
                                        Courses.Add(new CourseEditModel
                                        {
                                            CourseName = rdr["CourseName"].ToString(),
                                            CourseImagePath = rdr["CourseImagePath"].ToString().Substring(1),
                                            CourseGuid = rdr["CourseGuid"].ToString(),
                                            CourseDetails = rdr["CourseDetails"].ToString()
                                        });
                                    }
                                }
                                if(Courses.Count != 0)
                                {
                                    return new Tuple<TeachersEditModel, List<ItemCode>, List<CourseEditModel>>(teacherDetails,items,Courses);
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
    }
}
