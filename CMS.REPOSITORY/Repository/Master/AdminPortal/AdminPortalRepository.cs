using ApplicationTools.Configuration;
using ApplicationTools.PasswordHasher;
using CMS.MODEL.Event;
using CMS.MODEL.Library;
using CMS.MODEL.Master;
using CMS.MODEL.User;
using CMS.REPOSITORY.IRepository.IMaster.AdminPortal;
using CMS.UTILITIES.CreatePDF;
using CMS.UTILITIES.FolderCeration;
using CMS.UTILITIES.ImageUpload;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.REPOSITORY.Repository.Master.AdminPortal
{
    public class AdminPortalRepository : IAdminPortalRepository
    {
        #region Parameters
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int ReportAount { get; set; }
        #endregion

        #region Dropdowns
        public List<DropDownListModel> dropdown()
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["AdminPortalDropdown"].ToString();
                List<DropDownListModel> dropdowndata = new List<DropDownListModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            dropdowndata.Add(new DropDownListModel
                            {
                                BookName = rdr["BookName"].ToString(),
                                BookGuid = rdr["BookGuid"].ToString()
                            });
                        }
                        if(dropdowndata != null)
                        {
                            return dropdowndata;
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

        public List<DropDownListModel> RoleList()
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["UserRoleDropdown"].ToString();
                List<DropDownListModel> dropdowndata = new List<DropDownListModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            dropdowndata.Add(new DropDownListModel
                            {
                                RoleName = rdr["RoleName"].ToString(),
                                RoleGuid = rdr["RoleGuid"].ToString()
                            });
                        }
                        if (dropdowndata != null)
                        {
                            return dropdowndata;
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
        public List<DropDownListModel> TeacherList()
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["TeacherDropdown"].ToString();
                List<DropDownListModel> dropdowndata = new List<DropDownListModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            dropdowndata.Add(new DropDownListModel
                            {
                                TeacherName = rdr["TeacherName"].ToString(),
                                TeacherGuid = rdr["TeacherGuid"].ToString()
                            });
                        }
                        if (dropdowndata != null)
                        {
                            return dropdowndata;
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
        public List<DropDownListModel> SubjectList()
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["SubjectListDropdown"].ToString();
                List<DropDownListModel> dropdowndata = new List<DropDownListModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            dropdowndata.Add(new DropDownListModel
                            {
                                Subejct = rdr["SubectName"].ToString(),
                                SubejctGuid = rdr["SubectGuid"].ToString()
                            });
                        }
                        if (dropdowndata != null)
                        {
                            return dropdowndata;
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
        public List<DropDownListModel> SemeList(string Id)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = "select SemesterName,SemesterGuid from CourseSemester WHERE CourseId in (select id FROM Course where CourseGuid = @Id)";
                List<DropDownListModel> dropdowndata = new List<DropDownListModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Id", Id));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            dropdowndata.Add(new DropDownListModel
                            {
                                Semester = rdr["SemesterName"].ToString(),
                                SemesterGuid = rdr["SemesterGuid"].ToString()
                            });
                        }
                        if (dropdowndata != null)
                        {
                            return dropdowndata;
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

        #region Login And LogOut
        public TeachersEditModel AdminstrationLogin(ItemCode credentials)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["TeacherLoginData"].ToString();
                TeachersEditModel TeachersDetails = new TeachersEditModel();
                string UserPassword = string.Empty;
                SqlConnection con;
                SqlCommand cmd;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    using (cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Email", credentials.Value));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            TeachersDetails.TeacherName = rdr["TeacherName"].ToString();
                            TeachersDetails.RoleName = rdr["RoleName"].ToString();
                            TeachersDetails.ImagePath = rdr["TeacherImagePath"].ToString();
                            TeachersDetails.TeacherGuid = rdr["TeacherGuid"].ToString();
                            TeachersDetails.EmailId = rdr["EmailId"].ToString();
                            Password = rdr["Password"].ToString();
                            PasswordSalt = rdr["PasswordSalt"].ToString();
                        }
                        if (PasswordSalt != null || PasswordSalt != string.Empty && Password != null || Password != string.Empty)
                        {
                            rdr.Close();
                            UserPassword = PasswordHasher.PasswordHash(credentials.Code, PasswordSalt);
                            if(UserPassword == Password)
                            {
                                return TeachersDetails;
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

        #region Get
        #region GetDetailsAboutCourse
        public Tuple<CourseEditModel, List<CourseSemesterModel>> GetSpecificCourse(string CourseId)
        {
            try
            {
                CourseEditModel Course = new CourseEditModel();
                List<CourseSemesterModel> CourseSemester = new List<CourseSemesterModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = "select CourseName, CourseGuid, PerSemesterFess, FullCourseFess, CourseDetails, CourseRequirements, TotalSemester, Duration, CourseImagePath, CourseType FROM Course WHERE CourseGuid = @CourseId";
                string Query_ = "select SemesterName, SemesterGuid FROM CourseSemester WHERE CourseId in (SELECT Id FROM Course WHERE CourseGuid = @CourseId)";
                SqlConnection con;
                SqlCommand cmd;
                SqlDataReader rdr;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    using (cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@CourseId", CourseId));
                        rdr = cmd.ExecuteReader();
                        if(rdr.Read())
                        {
                            Course.CourseName = rdr["CourseName"].ToString();
                            Course.PerSemesterFess = Convert.ToInt32(rdr["PerSemesterFess"]);
                            Course.FullCourseFess = Convert.ToInt32(rdr["FullCourseFess"]);
                            Course.CourseDetails = rdr["CourseDetails"].ToString();
                            Course.CourseRequirements = rdr["CourseRequirements"].ToString();
                            Course.TotalSemester = Convert.ToInt32(rdr["TotalSemester"]);
                            Course.CourseDuration = rdr["Duration"].ToString();
                            Course.CourseType = !string.IsNullOrEmpty(rdr["CourseType"].ToString()) ? rdr["CourseType"].ToString() : string.Empty ;
                            Course.CourseImagePath = rdr["CourseImagePath"].ToString();
                            Course.CourseGuid = CourseId;
                        }
                        if(Course != null)
                        {
                            rdr.Close();
                            using (cmd = new SqlCommand(Query_, con))
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.Add(new SqlParameter("@CourseId", CourseId));
                                rdr = cmd.ExecuteReader();
                                while(rdr.Read())
                                {
                                    CourseSemester.Add(new CourseSemesterModel
                                    {
                                        SemesterName = rdr["SemesterName"].ToString(),
                                        SemmesterGuid = rdr["SemesterGuid"].ToString()
                                    });
                                }
                                if(CourseSemester != null)
                                {
                                    return new Tuple<CourseEditModel, List<CourseSemesterModel>>(Course, CourseSemester);
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

        #region TeacherAllDetails
        public Tuple<CourseTeacherModel, List<DropDownListModel>> TeacherAllDetails(string TeacherId)
        {
            try
            {
                List<DropDownListModel> ListDetails = new List<DropDownListModel>();
                CourseTeacherModel TeacherDetails = new CourseTeacherModel();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["AdminPortalTeacherListQueryByTeacherId"].ToString();
                string Query_ = QueryConfig.BookQuerySettings["AdminPortalTeacherCourseDetailsListQueryByTeacherId"].ToString();
                SqlConnection con;
                SqlCommand cmd;
                SqlDataReader rdr;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    using (cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@TeacherId", TeacherId));
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            TeacherDetails.TeacherName = rdr["TeacherName"].ToString();
                            TeacherDetails.TeacherGuid = rdr["TeacherGuid"].ToString();
                            TeacherDetails.SocialLink1 = rdr["SocialLink1"].ToString();
                            TeacherDetails.SocialLink2 = rdr["SocialLink2"].ToString();
                            TeacherDetails.SocialLink3 = rdr["SocialLink3"].ToString();
                            TeacherDetails.SocialLink4 = rdr["SocialLink4"].ToString();
                            TeacherDetails.ImagePath = rdr["TeacherImagePath"].ToString();
                            TeacherDetails.JoiningDate = DateTimeOffset.Parse(rdr["JoiningDate"].ToString());
                        }
                        if (TeacherDetails != null)
                        {
                            rdr.Close();
                            using (cmd = new SqlCommand(Query_, con))
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.Add(new SqlParameter("@TeacherId", TeacherId));
                                rdr = cmd.ExecuteReader();
                                while(rdr.Read())
                                {
                                    ListDetails.Add(new DropDownListModel
                                    {
                                        Course = rdr["CourseName"].ToString(),
                                        CourseGuid = rdr["CourseGuid"].ToString(),
                                        Semester = rdr["SemesterName"].ToString(),
                                        SemesterGuid = rdr["SemesterGuid"].ToString(),
                                        Subejct = rdr["SubectName"].ToString(),
                                        SubejctGuid = rdr["SubectGuid"].ToString()
                                    });
                                }
                                if(ListDetails != null)
                                {
                                    return new Tuple<CourseTeacherModel, List<DropDownListModel>>(TeacherDetails, ListDetails);
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

        #region CollegtEnrolledTeachersList
        public List<CourseTeacherModel> EnrolledTeachers()
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["AdminPortalTeacherListQuery"].ToString();
                List<CourseTeacherModel> Teachers = new List<CourseTeacherModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            Teachers.Add(new CourseTeacherModel
                            {
                                TeacherName = rdr["TeacherName"].ToString(),
                                TeacherGuid = rdr["TeacherGuid"].ToString(),
                                RoleName = rdr["RoleName"].ToString(),
                                RoleGuid = rdr["RoleGuid"].ToString(),
                                ImagePath = rdr["TeacherImagePath"].ToString()
                            });
                        }
                        if(Teachers != null)
                        {
                            return Teachers;
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

        #region Student
        #region GetListOfPassedOutStudents
        public List<StudentModel> PassedOutStudents(DropDownListModel Year)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["GetListOfPassedOutStudents"].ToString();
                List<StudentModel> Students = new List<StudentModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Year", Year.Code));
                        cmd.Parameters.Add(new SqlParameter("@CourseGuid", Year.Stream));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            Students.Add(new StudentModel
                            {
                                StudentName = rdr["FirstName"].ToString() + rdr["LastName"].ToString(),
                                Stream = rdr["CourseName"].ToString(),
                                Semester = rdr["SemesterName"].ToString(),
                                StudentGuid = rdr["StudentGuid"].ToString()
                            });
                        }
                        if(Students != null)
                        {
                            return Students;
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

        #region JosnResults
        #region CheckCourseNameAlreadyAvailableOrNot
        public bool CheckCourseName(string name)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["CheckIsCourseNameAvailable"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Name", name));
                        if(Convert.ToInt32(cmd.ExecuteScalar()) > 0)
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

        #region ListDeletedCourses
        public List<CourseEditModel> DeletedCourseList()
        {

            try
            {
                List<CourseEditModel> CourseList = new List<CourseEditModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetDeletedCourselist"].ToString();

                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            CourseList.Add(new CourseEditModel
                            {
                                CourseName = rdr["CourseName"].ToString(),
                                CourseGuid = rdr["CourseGuid"].ToString(),
                                FullCourseFess = Convert.ToInt32(rdr["FullCourseFess"]),
                                CourseDetails = rdr["Coursedetails"].ToString(),
                                CourseRequirements = rdr["CourseRequirements"].ToString(),
                                CourseDuration = rdr["Duration"].ToString(),
                                CourseImagePath = rdr["CourseImagePath"].ToString()
                            });
                        }
                        if (CourseList != null)
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

        #region Library
        #region GetAllLibraryCards
        public List<LibraryRecordsEditModel> LibraryCardsList(DropDownListModel data)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["GetlistOfStudentsLibraryCardsByFilter"].ToString();
                List<LibraryRecordsEditModel> LibraryCardLists = new List<LibraryRecordsEditModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@CrsGuid", data.StreamGuid));
                        cmd.Parameters.Add(new SqlParameter("@SemGuid", data.SemesterGuid));
                        cmd.Parameters.Add(new SqlParameter("@Value", data.Value));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            LibraryCardLists.Add(new LibraryRecordsEditModel
                            {
                                StudentName = rdr["FirstName"].ToString() + rdr["MiddleName"]?.ToString() + rdr["LastName"]?.ToString(),
                                Course = rdr["CourseName"].ToString(),
                                Semester = rdr["SemesterName"].ToString(),
                                LibraryCardSerialNumber = rdr["LibraryCardSerialNumber"].ToString(),
                                IsBookTaken = !string.IsNullOrEmpty(rdr["IsBookTaken"].ToString()) ? Convert.ToBoolean(rdr["IsBookTaken"].ToString()) != Convert.ToBoolean(0) ? true : false : false ,
                                StudentRegId = rdr["StudentGuid"].ToString(),
                                Code = rdr["StudentImagePath"].ToString().Substring(1)
                            });
                        }
                        if(LibraryCardLists.Count != 0)
                        {
                            return LibraryCardLists;
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

        #region Reports
        #region CountReportAmount
        public int TotalReports()
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["GetNotReadNoticeAmount"];
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            ReportAount = Convert.ToInt32(rdr["ReportAmount"].ToString());
                        }
                        return ReportAount;
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

        #region GetAllReports
        public List<ContactUsModel> Reports()
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["RetriveAllReports"].ToString();
                List<ContactUsModel> ReportsList = new List<ContactUsModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            ReportsList.Add(new ContactUsModel
                            {
                                ReportSubject = rdr["ReportSubject"].ToString(),
                                ReportDetails = rdr["ReportDetails"].ToString(),
                                CustomerName = rdr["CustomerName"].ToString(),
                                CustomerEmail = rdr["CustomerEmail"].ToString(),
                                ReportGuid = rdr["ReportGuid"].ToString(),
                                ReportImagePath = !string.IsNullOrEmpty(rdr["ReportImagePath"].ToString()) ? rdr["ReportImagePath"].ToString().Substring(1) : string.Empty,
                                IsRead = Convert.ToBoolean(rdr["IsRead"].ToString())
                            });
                        }
                        if(ReportsList.Count != 0)
                        {
                            return ReportsList;
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
        #region UplaodEventImages
        public bool UploadEventImages(EventGalleryModel Images)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["UploadEventImages"].ToString();
                string FolderPath = ConfigurationManager.AppSettings["UploadEventImagePath"].ToString();
                string ImageFolderPath = CreateFolder.UploadEventImagePath(Images.Value, FolderPath);
                foreach (var item in Images.UploadFiles)
                {
                    string ImageSavePath = UploadImage.Upload(item, ImageFolderPath);
                }
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Code", Images.Value));
                        cmd.Parameters.AddWithValue("@ImageFolderPath", ImageFolderPath);
                        cmd.Parameters.AddWithValue("@CreatedBy", Images.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", DateTimeOffset.UtcNow);
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

        #region TeacherZone
        #region AddNewTeacher
        public bool NewTeacher(TeachersEditModel addTeacher)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["AddNewTeacherForInst"].ToString();
                string guid = Guid.NewGuid().ToString().ToUpper();
                string Path = ConfigurationManager.AppSettings["TeacherImageUpload"].ToString();
                string ImageFolderPath = CreateFolder.UploadEventImagePath(guid, Path);
                string ImagePath = UploadImage.Upload(addTeacher.UploadFile, ImageFolderPath);
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Code", addTeacher.RoleName));
                        cmd.Parameters.AddWithValue("@TeacherName", addTeacher.TeacherName);
                        cmd.Parameters.AddWithValue("@Biography", addTeacher.Biography);
                        cmd.Parameters.AddWithValue("@EmailId", addTeacher.EmailId);
                        cmd.Parameters.AddWithValue("@PhoneNumber", addTeacher.PhoneNumber);
                        cmd.Parameters.AddWithValue("@SocialLink1", addTeacher.SocialLink1);
                        cmd.Parameters.AddWithValue("@SocialLink2", addTeacher.SocialLink2);
                        cmd.Parameters.AddWithValue("@SocialLink3", addTeacher.SocialLink3);
                        cmd.Parameters.AddWithValue("@SocialLink4", addTeacher.SocialLink4);
                        cmd.Parameters.AddWithValue("@TeacherGuid", guid);
                        cmd.Parameters.AddWithValue("@TeacherImagePath", ImagePath);
                        cmd.Parameters.AddWithValue("@JoiningDate", addTeacher.JoiningDate);
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

        #region AppointTeacher
        public bool AppointTeacher(DropDownListModel addCourseTeacher)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = "DECLARE @TID int = (select Id from Teachers WHERE TeacherGuid = @TeacherGuid)"+
                " DECLARE @CID int = (select Id FROM Course where CourseGuid = @StreamGuid)"+
                " DECLARE @SeID int = (select Id from CourseSemester WHERE SemesterGuid = @SemesterGuid)"+
                " DECLARE @SbID int = (select Id from Subjects WHERE SubectGuid = @SubejctGuid)"+
                " insert into CourseTeachers(TeacherId, CourseId, SemesterId, SubjectId) VALUES(@TID, @CID, @SeID, @SbID)";
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@TeacherGuid", addCourseTeacher.TeacherName));
                        cmd.Parameters.Add(new SqlParameter("@SubejctGuid", addCourseTeacher.Subejct));
                        cmd.Parameters.Add(new SqlParameter("@StreamGuid", addCourseTeacher.StreamGuid));
                        cmd.Parameters.Add(new SqlParameter("@SemesterGuid", addCourseTeacher.SemesterGuid));
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

        #region Course
        #region AddNewCourse
        public bool AddCourse(CourseEditModel Course)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["AddNewCourse"].ToString();
                string Query_ = QueryConfig.BookQuerySettings["AddNewCourseSemester"].ToString();
                string CourseGuid = Guid.NewGuid().ToString().ToUpper();
                SqlConnection con;
                SqlCommand cmd;
                string Path = ConfigurationManager.AppSettings["CourseImageUploadPath"].ToString();
                string ImageFolder = CreateFolder.UploadEventImagePath(Course.CourseName, Path);
                string CourseImagePath = UploadImage.Upload(Course.UploadImage, ImageFolder);
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction("First");
                    try
                    {
                        using (cmd = new SqlCommand(Query, con, transaction))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CourseName", Course.CourseName);
                            cmd.Parameters.AddWithValue("@PerSemesterFess", Course.PerSemesterFess);
                            cmd.Parameters.AddWithValue("@FullCourseFess", Course.FullCourseFess);
                            cmd.Parameters.AddWithValue("@CourseDetails", Course.CourseDetails);
                            cmd.Parameters.AddWithValue("@Duration", Course.CourseDuration);
                            cmd.Parameters.AddWithValue("@CourseRequirements", Course.CourseRequirements);
                            cmd.Parameters.AddWithValue("@TotalSemester", Course.TotalSemester);
                            cmd.Parameters.AddWithValue("@CourseImagePath", CourseImagePath);
                            cmd.Parameters.AddWithValue("@CourseType", Course.CourseType);
                            cmd.Parameters.AddWithValue("@CourseGuid", CourseGuid);
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                int j = 0;
                                for (int i = 1; i <= Course.TotalSemester; i++)
                                {
                                    using (cmd = new SqlCommand(Query_, con, transaction))
                                    {
                                        cmd.Parameters.Add(new SqlParameter("@CourseId", CourseGuid));
                                        cmd.Parameters.AddWithValue("@SemesterName", i + "st Semester");
                                        cmd.Parameters.AddWithValue("@SemesterGuid", Guid.NewGuid().ToString().ToUpper());
                                        j = cmd.ExecuteNonQuery();
                                    }
                                }
                                if (j > 0)
                                {
                                    transaction.Commit();
                                    return true;
                                }
                                else
                                {
                                    transaction.Rollback();
                                    return false;
                                }
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        LogManager.ErrorEntry(ex);
                        throw;
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

        #region Put
        #region UpdateCourseData
        public bool UpdateCourse(CourseEditModel EditCourse)
        {
            string ImageFilePath = null;
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["UpdateCourseData"].ToString();
                if (EditCourse.UploadImage != null)
                {
                    string FilePath = ConfigurationManager.AppSettings["CourseImageUploadPath"].ToString() + EditCourse.CourseName + "/";
                    ImageFilePath = UploadImage.Upload(EditCourse.UploadImage, FilePath);
                }
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@CRSID", EditCourse.Code));
                        cmd.Parameters.AddWithValue("@CourseName", !string.IsNullOrEmpty(EditCourse.CourseName) ? EditCourse.CourseName : string.Empty);
                        cmd.Parameters.AddWithValue("@PerSemesterFess", !string.IsNullOrEmpty(EditCourse.PerSemesterFess.ToString()) ? EditCourse.PerSemesterFess : 0);
                        cmd.Parameters.AddWithValue("@FullCourseFess", !string.IsNullOrEmpty(EditCourse.FullCourseFess.ToString()) ? EditCourse.FullCourseFess : 0);
                        cmd.Parameters.AddWithValue("@CourseDetails", !string.IsNullOrEmpty(EditCourse.CourseDetails) ? EditCourse.CourseDetails : string.Empty);
                        cmd.Parameters.AddWithValue("@CourseRequirements", !string.IsNullOrEmpty(EditCourse.CourseRequirements) ? EditCourse.CourseRequirements : string.Empty);
                        cmd.Parameters.AddWithValue("@TotalSemester", !string.IsNullOrEmpty(EditCourse.TotalSemester.ToString()) ? EditCourse.TotalSemester : 0);
                        cmd.Parameters.AddWithValue("@Duration", !string.IsNullOrEmpty(EditCourse.CourseDuration) ? EditCourse.CourseDuration : string.Empty);
                        cmd.Parameters.AddWithValue("@CourseImagePath", !string.IsNullOrEmpty(ImageFilePath) ? ImageFilePath : EditCourse.CourseImagePath);
                        cmd.Parameters.AddWithValue("@CourseType", !string.IsNullOrEmpty(EditCourse.CourseType) ? EditCourse.CourseType : string.Empty);
                        if(cmd.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                        else
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(ImageFilePath));
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                File.Delete(HttpContext.Current.Server.MapPath(ImageFilePath));
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region EnrollDeletedCourse
        public bool EnrollDeletedCourse(string CID)
        {
            string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
            string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
            string Query = QueryConfig.BookQuerySettings["EnrollDeletedCourse"].ToString();
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@CID", CID));
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
        #endregion

        #region Student
        #region UPdateStudentData
        public List<UserStudenEdittModel> StudentsDetailsByFilter(DropDownListModel data)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["ListFilteredStudentslist"].ToString();
                List<UserStudenEdittModel> FilterStudents = new List<UserStudenEdittModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Crsid", data.StreamGuid));
                        cmd.Parameters.Add(new SqlParameter("@SId", data.SemesterGuid));
                        cmd.Parameters.Add(new SqlParameter("@Year", data.Value));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            FilterStudents.Add(new UserStudenEdittModel
                            {
                                FirstName = rdr["FirstName"].ToString() + rdr["MiddleName"]?.ToString() + rdr["LastName"].ToString(),
                                Course = rdr["CourseName"].ToString(),
                                Semester = rdr["SemesterName"].ToString(),
                                StudentImagePath = rdr["StudentImagePath"].ToString().Substring(1),
                                StudentGuid = rdr["StudentGuid"].ToString()
                            });
                        }
                        int i = FilterStudents.Count;
                        if (FilterStudents.Count != 0)
                        {
                            return FilterStudents;
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

        #region Report
        #region UpdateReportReadStatus
        public bool ReadReport(string RID)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["UpdateReportReadStatus"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@RepId", RID));
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

        #region Teacher
        #region UpdateTeacherData
        public bool UpdateTeacherData(TeachersEditModel TeacherData)
        {
            string NewImagePath = null;
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["UpdateTeacherData"].ToString();
                if (TeacherData.UploadFile != null)
                {
                    string Path = ConfigurationManager.AppSettings["TeacherImageUpload"].ToString() + TeacherData.Code.ToUpper() + "/";
                    NewImagePath = UploadImage.Upload(TeacherData.UploadFile, Path);
                }
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@TeacherGuid", TeacherData.Code));
                        cmd.Parameters.AddWithValue("@TeacherName", TeacherData.TeacherName);
                        cmd.Parameters.AddWithValue("SocialLink1", !string.IsNullOrEmpty(TeacherData.SocialLink1) ? TeacherData.SocialLink1 : string.Empty);
                        cmd.Parameters.AddWithValue("SocialLink2", !string.IsNullOrEmpty(TeacherData.SocialLink2) ? TeacherData.SocialLink2 : string.Empty);
                        cmd.Parameters.AddWithValue("SocialLink3", !string.IsNullOrEmpty(TeacherData.SocialLink3) ? TeacherData.SocialLink3 : string.Empty);
                        cmd.Parameters.AddWithValue("SocialLink4", !string.IsNullOrEmpty(TeacherData.SocialLink4) ? TeacherData.SocialLink4 : string.Empty);
                        cmd.Parameters.AddWithValue("@Biography", !string.IsNullOrEmpty(TeacherData.Biography) ? TeacherData.Biography : string.Empty);
                        cmd.Parameters.AddWithValue("@PhoneNumber", !string.IsNullOrEmpty(TeacherData.PhoneNumber) ? TeacherData.PhoneNumber : string.Empty);
                        cmd.Parameters.AddWithValue("@TeacherImagePath", !string.IsNullOrEmpty(NewImagePath) ? NewImagePath : "~" + TeacherData.ImagePath);
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
                File.Delete(HttpContext.Current.Server.MapPath(NewImagePath));
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion
        #endregion
        #endregion

        #region Delete
        #region Teacher
        #region RemoveTeacherFromCourses
        public bool RemoveCourseTeacher(DropDownListModel details)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["RemoveTeacherFromCourse"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@TeacherGuid", details.TeacherGuid));
                        cmd.Parameters.Add(new SqlParameter("@SemesterGuid", details.SemesterGuid));
                        cmd.Parameters.Add(new SqlParameter("@CourseGuid", details.CourseGuid));
                        cmd.Parameters.Add(new SqlParameter("@SubectGuid", details.SubejctGuid));
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

        #region Resignteacher
        public bool Resignteacher(ResignteacherModel TeacherID)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings[""].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@TeacherId", TeacherID.TeacherGuid));
                        cmd.Parameters.AddWithValue("@IsResigned", true);
                        cmd.Parameters.AddWithValue("@ResigneDate", DateTimeOffset.UtcNow);
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

        #region Course
        #region DeleteCourse
        public bool DeleteCourse(string CID)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["DeleteCourse"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@CID", CID));
                        if(cmd.ExecuteNonQuery () > 0)
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

        #region DeleteNotice
        public bool DeleteNotice(string NID)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["DeleteNoticeByNoticeId"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@NoticeId", NID));
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

        #region LibraryCard
        public bool DeleteLibraryCard(string LCID)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["DeleteLibrayCardSerialNumber"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@LCID", LCID));
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

        #region deleteCourseSemester
        public bool deleteCourseSemester(string CRSID, string STRID)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["DeleteCourseSemester"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@CrsId", CRSID));
                        cmd.Parameters.Add(new SqlParameter("@StrId", STRID));
                        if(cmd.ExecuteNonQuery () > 0)
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
