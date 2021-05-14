using ApplicationTools.Configuration;
using CMS.MODEL.Addmission;
using CMS.MODEL.Master;
using CMS.MODEL.User;
using CMS.REPOSITORY.IRepository.IAddmission;
using CMS.UTILITIES.CreateExcelFile;
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

namespace CMS.REPOSITORY.Repository.Addmission
{
    public class NewStudentAddmissionRepository : INewStudentAddmissionRepository
    {
        #region Get
        #region GetListItemsForDropDownLists
        #region GetCountryList
        public List<DropDownListModel> CountryList()
        {
            try
            {
                List<DropDownListModel> DropDownList = new List<DropDownListModel>();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["CountryList"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            DropDownList.Add(new DropDownListModel
                            {
                                CountryGuid = rdr["CountryGuid"].ToString(),
                                Country = rdr["CountryName"].ToString()
                            });
                        }
                        if (DropDownList != null)
                        {
                            return DropDownList;
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

        #region GetStreamList
        public List<DropDownListModel> StreamList()
        {
            try
            {
                List<DropDownListModel> DropDownList = new List<DropDownListModel>();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["CourseList"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            DropDownList.Add(new DropDownListModel
                            {
                                StreamGuid = rdr["CourseGuid"].ToString(),
                                Stream = rdr["CourseName"].ToString()
                            });
                        }
                        if (DropDownList != null)
                        {
                            return DropDownList;
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

        #region GetGerderList
        public List<DropDownListModel> GenderList()
        {
            try
            {
                List<DropDownListModel> DropDownList = new List<DropDownListModel>();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GenderList"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            DropDownList.Add(new DropDownListModel
                            {
                                GenderGuid = rdr["GenderGuid"].ToString(),
                                Gender = rdr["GenderName"].ToString()
                            });
                        }
                        if (DropDownList != null)
                        {
                            return DropDownList;
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

        #region GetStateList
        public List<DropDownListModel> StateList()
        {
            try
            {
                List<DropDownListModel> DropDownList = new List<DropDownListModel>();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["StateList"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            DropDownList.Add(new DropDownListModel
                            {
                                StateGuid = rdr["StateGuid"].ToString(),
                                State = rdr["StateName"].ToString()
                            });
                        }
                        if (DropDownList != null)
                        {
                            return DropDownList;
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

        #region SemesterListByCourse
        public List<DropDownListModel> SemesterList()
        {
            try
            {
                List<DropDownListModel> DropDownList = new List<DropDownListModel>();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["SemesterList"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            DropDownList.Add(new DropDownListModel
                            {
                                Semester = rdr["SemesterName"].ToString(),
                                SemesterGuid = rdr["SemesterGuid"].ToString()
                            });
                        }
                        if (DropDownList != null)
                        {
                            return DropDownList;
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

        #region GetCitylist
        public List<DropDownListModel> CityList()
        {
            try
            {
                List<DropDownListModel> DropDownList = new List<DropDownListModel>();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["CityList"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            DropDownList.Add(new DropDownListModel
                            {
                                City = rdr["CityName"].ToString(),
                                CityCode = rdr["CityCode"].ToString()
                            });
                        }
                        if (DropDownList != null)
                        {
                            return DropDownList;
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

        #region Parameterized Dropdown Lists
        #region StatelistByCountryId
        public List<DropDownListModel> StateListById(string StateGuid)
        {

            try
            {
                List<DropDownListModel> DropDownList = new List<DropDownListModel>();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["StateListById"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@StateGuid", StateGuid));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            DropDownList.Add(new DropDownListModel
                            {
                                StateGuid = rdr["StateGuid"].ToString(),
                                State = rdr["StateName"].ToString()
                            });
                        }
                        if (DropDownList != null)
                        {
                            return DropDownList;
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

        #region StatelistByCountryId
        public List<DropDownListModel> CityListById(string CityGuid)
        {

            try
            {
                List<DropDownListModel> DropDownList = new List<DropDownListModel>();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["CityListById"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@CityGuid", CityGuid));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            DropDownList.Add(new DropDownListModel
                            {
                                City = rdr["CityName"].ToString(),
                                CityCode = rdr["CityCode"].ToString()
                            });
                        }
                        if (DropDownList != null)
                        {
                            return DropDownList;
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

        #region SemesterlistByCourseId
        public List<DropDownListModel> SemesterListById(string CourseGuid)
        {
            try
            {
                List<DropDownListModel> DropDownList = new List<DropDownListModel>();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["SemesterListById"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@CourseGuid", CourseGuid));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            DropDownList.Add(new DropDownListModel
                            {
                                Semester = rdr["SemesterName"].ToString(),
                                SemesterGuid = rdr["SemesterGuid"].ToString()
                            });
                        }
                        if (DropDownList != null)
                        {
                            return DropDownList;
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

        #region SearchStudentbyStudentName,Stream,Semester
        public List<StudentCardModel> SearchStudent(StudentFessEditModel studentFess)
        {
            try
            {
                List<StudentCardModel> students = new List<StudentCardModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["SearchStudent"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@StudentName", studentFess.StudentName));
                        cmd.Parameters.Add(new SqlParameter("@CourseGuid", studentFess.CourseGuid));
                        cmd.Parameters.Add(new SqlParameter("@SemesterGuid", studentFess.SemesterGuid));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            students.Add(new StudentCardModel {
                                StudentName = Convert.ToString(rdr["FirstName"].ToString() + " " + rdr["LastName"].ToString()),
                                Stream = rdr["Stream"].ToString(),
                                StudentImagePath = rdr["StudentImagePath"].ToString(),
                                StudentGuid = rdr["StudentGuid"].ToString()
                            });
                        }
                        if (students != null)
                        {
                            return students;
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

        #region GetNewStudentsAddmissionListByCurrentTime(ByCurrentYear)
        /// <summary>
        /// returns full list of the studenyts who are joining in current season
        /// </summary>
        /// <returns></returns>
        public List<StudentModel> NewStudentsAddmissionList()
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetListOfCurrentAdmittedStudents"].ToString();
                List<StudentModel> newStudents = new List<StudentModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        DateTime dt = DateTime.Now;
                        var i = dt.Year;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Year", i));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            newStudents.Add(new StudentModel { 
                                StudentName = rdr["FirstName"].ToString() + rdr["LastName"].ToString(),
                                Stream = rdr["CourseName"].ToString(),
                                Semester = rdr["SemesterName"].ToString(),
                                StudentGuid = rdr["StudentGuid"].ToString()
                                //Value = rdr["Roll"].ToString(), //Value Is Based For StudentRoll
                                //Email = rdr["Email"].ToString(),
                                //Code = rdr["Code"].ToString() //Code Is Based for Studentguid
                            });
                        }
                        if(newStudents != null)
                        {
                            return newStudents;
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

        #region GetNewStudentsAddmissionListByStreamAndByCurrentYear
        /// <summary>
        /// Get StudentLists whore are oinging in the Current Year By stream(Course)
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<StudentModel> NewStudentsAddmissionListByFilterParameter(StudentModel student)
        {
            try
            {
                string Cd = string.IsNullOrEmpty(Convert.ToString(student.Stream)) ? student.Stream : string.Empty;
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings[""].ToString();
                List<StudentModel> newStudents = new List<StudentModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Stream", string.IsNullOrEmpty(Convert.ToString(student.Stream)) ? student.Stream : string.Empty));
                        cmd.Parameters.Add(new SqlParameter("@Year", DateTime.Now.Year));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            newStudents.Add(new StudentModel
                            {
                                StudentName = rdr["StudentName"].ToString(),
                                Stream = rdr["Stream"].ToString(),
                                Value = rdr["Roll"].ToString(), //Value Is Based For StudentRoll
                                Email = rdr["Email"].ToString(),
                                Code = rdr["Code"].ToString() //Code Is Based for Studentguid
                            });
                        }
                        if(newStudents != null)
                        {
                            return newStudents;
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

        #region FetchGuidCodeDetails
        public GetGuidDetailsModel GuidDetails(string SemesterData , string CourseData)
        {
            try
            {
                GetGuidDetailsModel dataModel = new GetGuidDetailsModel();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetNameFromGuid"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@SemesterGuid", CourseData));
                        cmd.Parameters.Add(new SqlParameter("@CourseGuid", SemesterData));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if(rdr.Read())
                        {
                            dataModel.CourseName = rdr["CourseName"].ToString();
                            dataModel.SemesterName = rdr["SemesterName"].ToString();
                        }
                        if(dataModel != null)
                        {
                            return dataModel;
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

        #region Post
        #region NewStudentAddmission
        public bool StudentAddmission(AddmissionEditModel student)
        {
            try
            {
                string Path = ConfigurationManager.AppSettings["StudentDocumentUploadPath"].ToString();
                string RootFolderPath = CreateFolder.UploadEventImagePath(student.FirstName + Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 6), Path);
                string ImageFolderPath = CreateFolder.UploadEventImagePath(student.FirstName + Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 6), RootFolderPath);
                string ExcelFolderPath = CreateFolder.UploadEventImagePath(student.FirstName + Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 6), RootFolderPath);
                string StudentImageFolderPath = CreateFolder.UploadEventImagePath(student.FirstName + Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 6), RootFolderPath);
                string StudentFessRecordFolderPath = CreateFolder.UploadEventImagePath(student.FirstName + student.LastName + student.AddmissionYear + Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 3), RootFolderPath);
                string ExcelFilePath = CreateExcel.Create(student.FirstName, student, ExcelFolderPath);
                string ImageFilePath = UploadImage.UploadMultiple(student.Fileupload, ImageFolderPath);
                string StudentImageFilePath = UploadImage.Upload(student.StudentImageFile, StudentImageFolderPath);
                string guid = Guid.NewGuid().ToString().ToUpper();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["AddNewRegistration"].ToString();
                string query_ = QueryConfig.BookQuerySettings["AddStudentDocument"].ToString();
                string _query_ = QueryConfig.BookQuerySettings["AddStudentFess"].ToString();
                string Query = QueryConfig.BookQuerySettings["AddStudentSemster"].ToString();
                SqlCommand cmd = new SqlCommand() ;
                SqlConnection con;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction("First");
                    try
                    {
                        using (cmd = new SqlCommand(query, con,transaction))
                        {
                            //insert into addmissiontable
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new SqlParameter("@CityCode", student.CityCode));
                            cmd.Parameters.Add(new SqlParameter("@CountryGuid", student.CountryGuid));
                            cmd.Parameters.Add(new SqlParameter("@StateGuid", student.StateGuid));
                            cmd.Parameters.Add(new SqlParameter("@Gender", student.GenderGuid));
                            cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                            cmd.Parameters.AddWithValue("@LastName", student.LastName);
                            cmd.Parameters.AddWithValue("@MiddleName", !string.IsNullOrEmpty(student.MiddleName) ? student.MiddleName : string.Empty);
                            cmd.Parameters.AddWithValue("@ParentName", student.ParentName);
                            cmd.Parameters.AddWithValue("@AddmissionYear", student.AddmissionYear);
                            cmd.Parameters.AddWithValue("@AddressLine1", student.AddressLine1);
                            cmd.Parameters.AddWithValue("@AddressLine2", student.AddressLine2);
                            cmd.Parameters.AddWithValue("@BloodGroup", student.BloodGroup);
                            cmd.Parameters.AddWithValue("@ContactNumber", student.ContactNumber);
                            cmd.Parameters.AddWithValue("@ContactNumberOpt", !string.IsNullOrEmpty(student.ContactNumberOpt) ? student.ContactNumberOpt : string.Empty);
                            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                            cmd.Parameters.AddWithValue("@StudentImagePath", StudentImageFilePath);
                            cmd.Parameters.AddWithValue("@ZipCode", student.ZipCode);
                            cmd.Parameters.AddWithValue("@Email", student.Email);
                            cmd.Parameters.AddWithValue("@Cast", !string.IsNullOrEmpty(student.Cast) ? student.Cast : string.Empty);
                            cmd.Parameters.AddWithValue("@StudentGuid", guid);
                            /*cmd.Parameters.AddWithValue("@CourseGuid", student.Course);*/
                        }
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            int i = 0;
                            using (cmd = new SqlCommand(query_, con,transaction))
                            {
                                //insert into studentDocumenttalbe
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.Add(new SqlParameter("@Studentguid", guid));
                                cmd.Parameters.AddWithValue("@ExcelDocumentPath", ExcelFilePath);
                                cmd.Parameters.AddWithValue("@ImageDocumentPath", ImageFilePath);
                                i = cmd.ExecuteNonQuery();
                            }
                            if (i > 0)
                            {
                                using (cmd = new SqlCommand(_query_, con,transaction))
                                {
                                    //insert into student fess table
                                    cmd.CommandType = CommandType.Text;
                                    GetGuidDetailsModel Data = GuidDetails(student.StreamGuid, student.SemesterGuid);
                                    string FessRecordExcelFilePath = CreateExcel.CreateFessRecordExcelFile(student.FirstName + student.LastName + "FessRecord", student, StudentFessRecordFolderPath,Data.CourseName,Data.SemesterName,"AddmissionFess");
                                    cmd.Parameters.Add(new SqlParameter("@StuId", guid));
                                    cmd.Parameters.Add(new SqlParameter("@SeId", student.SemesterGuid));
                                    cmd.Parameters.Add(new SqlParameter("@CrsId", student.StreamGuid));
                                    cmd.Parameters.AddWithValue("@FessDocumentPath", FessRecordExcelFilePath);
                                    cmd.Parameters.AddWithValue("@DueAmount", 0);
                                    cmd.Parameters.AddWithValue("@FineAmount", 0);
                                }
                                if (cmd.ExecuteNonQuery() > 0)
                                {
                                    //insert data into StudentSemestertable
                                    using (cmd = new SqlCommand(Query, con,transaction))
                                    {
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Parameters.Add(new SqlParameter("@StudentGuid", guid));
                                        cmd.Parameters.Add(new SqlParameter("@Semesterguid", student.SemesterGuid));
                                        cmd.Parameters.Add(new SqlParameter("@CourseGuid", student.StreamGuid));
                                        cmd.Parameters.Add(new SqlParameter("@SemesterStatus", "Current"));
                                    }
                                    if (cmd.ExecuteNonQuery() > 0)
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

    }
}
