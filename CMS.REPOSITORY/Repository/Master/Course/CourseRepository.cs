using ApplicationTools.Configuration;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.IMaster.ICourse;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.Repository.Master.Course
{
    public class CourseRepository : ICourseRepository
    {
        #region Get
        #region HomePageContent
        #region CourseSection
        public List<CourseEditModel> HomePageCourseContents()
        {
            try
            {
                List<CourseEditModel> courses = new List<CourseEditModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["HomePageCourseContent"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            courses.Add(new CourseEditModel
                            {
                                CourseName = rdr["CourseName"].ToString(),
                                CourseImagePath = rdr["CourseImagePath"].ToString().Substring(1),
                                CourseGuid = rdr["CourseGuid"].ToString(),
                                CourseType = !string.IsNullOrEmpty(rdr["CourseType"].ToString()) ? rdr["CourseType"].ToString() : "Not Added"
                            });
                        }
                        if (courses.Count != 0)
                        {
                            return courses;
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

        #region GetListOffCourses
        public List<CourseEditModel> courses()
        {
            try
            {
                List<CourseEditModel> CourseList = new List<CourseEditModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetCourses"].ToString();

                using(SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            CourseList.Add(new CourseEditModel {
                                CourseName = rdr["CourseName"].ToString(),
                                CourseGuid = rdr["CourseGuid"].ToString(),
                                FullCourseFess = Convert.ToInt32(rdr["FullCourseFess"]),
                                CourseDetails = rdr["Coursedetails"].ToString(),
                                CourseRequirements = rdr["CourseRequirements"].ToString(),
                                CourseDuration = rdr["Duration"].ToString(),
                                CourseImagePath = rdr["CourseImagePath"].ToString().Substring(1),
                                CourseType = !string.IsNullOrEmpty(rdr["CourseType"].ToString()) ? rdr["CourseType"].ToString() : "Not Added"
                            });
                        }
                        if(CourseList != null)
                        {
                            return CourseList;
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

        #region GetListOffCoursesById
        public Tuple<List<CourseEditModel>, List<CourseTeacherModel>, List<CourseEditModel>> CourseDetails(string CourseId)
        {
            try
            {
                List<CourseEditModel> CourseList = new List<CourseEditModel>();
                List<CourseEditModel> RelatedCourseList = new List<CourseEditModel>();
                List<CourseTeacherModel> TeacherList = new List<CourseTeacherModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetCoursesByCourseId"].ToString();
                string query_ = QueryConfig.BookQuerySettings["GetCourseTeachersByCourseId"].ToString();
                string _query = QueryConfig.BookQuerySettings["RelatedCourseList"].ToString();
                SqlConnection con;
                SqlCommand cmd;
                SqlDataReader rdr;
                using(con = new SqlConnection(CS))
                {
                    con.Open();
                    using(cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@CourseId", CourseId));
                        rdr = cmd.ExecuteReader();
                        if(rdr.Read())
                        {
                            CourseList.Add(new CourseEditModel {
                                CourseName = rdr["CourseName"].ToString(),
                                CourseGuid = rdr["CourseGuid"].ToString(),
                                FullCourseFess = Convert.ToInt32(rdr["FullCourseFess"]),
                                CourseDetails = rdr["Coursedetails"].ToString(),
                                CourseRequirements = rdr["CourseRequirements"].ToString(),
                                CourseDuration = rdr["Duration"].ToString(),
                                CourseImagePath = rdr["CourseImagePath"].ToString(),
                                TotalSemester = Convert.ToInt32(rdr["TotalSemester"])
                            });
                        }
                        if(CourseList != null)
                        {
                            rdr.Close();
                            using(cmd = new SqlCommand(query_ , con))
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.Add(new SqlParameter("@CourseId", CourseId));
                                rdr = cmd.ExecuteReader();
                                while(rdr.Read())
                                {
                                    TeacherList.Add(new CourseTeacherModel
                                    {
                                        TeacherName = rdr["TeacherName"].ToString(),
                                        SubjectName = rdr["SubectName"].ToString(),
                                        RoleName = rdr["RoleName"].ToString(),
                                        SocialLink1 = !string.IsNullOrEmpty(Convert.ToString(rdr["SocialLink1"])) ? rdr["SocialLink1"].ToString() : string.Empty,
                                        SocialLink2 = !string.IsNullOrEmpty(Convert.ToString(rdr["SocialLink2"])) ? rdr["SocialLink2"].ToString() : string.Empty,
                                        SocialLink3 = !string.IsNullOrEmpty(Convert.ToString(rdr["SocialLink3"])) ? rdr["SocialLink3"].ToString() : string.Empty,
                                        SocialLink4 = !string.IsNullOrEmpty(Convert.ToString(rdr["SocialLink4"])) ? rdr["SocialLink4"].ToString() : string.Empty,
                                        ImagePath = rdr["TeacherImagePath"].ToString()
                                    });
                                }
                                if(TeacherList != null)
                                {
                                    rdr.Close();
                                    using(cmd = new SqlCommand(_query,con))
                                    {
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Parameters.Add(new SqlParameter("@CourseId", CourseId));
                                        rdr = cmd.ExecuteReader();
                                        while(rdr.Read())
                                        {
                                            RelatedCourseList.Add(new CourseEditModel {
                                                CourseName = rdr["CourseName"].ToString(),
                                                CourseGuid = rdr["CourseGuid"].ToString(),
                                                FullCourseFess = Convert.ToInt32(rdr["FullCourseFess"]),
                                                CourseDetails = rdr["Coursedetails"].ToString(),
                                                CourseRequirements = rdr["CourseRequirements"].ToString(),
                                                CourseDuration = rdr["Duration"].ToString(),
                                                CourseImagePath = rdr["CourseImagePath"].ToString()
                                            });
                                        }
                                        if(RelatedCourseList != null)
                                        {
                                            return new Tuple<List<CourseEditModel>, List<CourseTeacherModel>, List<CourseEditModel>>(CourseList, TeacherList, RelatedCourseList);
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
