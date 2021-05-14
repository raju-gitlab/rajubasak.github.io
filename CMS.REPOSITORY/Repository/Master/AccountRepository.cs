using ApplicationTools.Configuration;
using ApplicationTools.EmailSender;
using ApplicationTools.PasswordHasher;
using CMS.MODEL.Master;
using CMS.MODEL.User;
using CMS.REPOSITORY.IRepository.IMaster;
using CMS.UTILITIES.FolderCeration;
using CMS.UTILITIES.ImageUpload;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.REPOSITORY.Repository.Master
{
    public class AccountRepository : IAccountRepository
    {
        #region Properties
        public string Password { get; set; }
        public string Passwordsalt { get; set; }
        public string UserRole { get; set; }
        public string VerificationCode { get; set; }
        #endregion

        #region Get
        #region CheckUserCredentials
        public bool UserCredentials(string EmailId)
        {
            try
            {
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["CheckUserExistense"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@EmailId", EmailId));
                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
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

        #region CheckUserExistance
        public bool UserExistance(string EmailId)
        {
            try
            {
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["UserExistenseCheck"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@EmailId", EmailId));
                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
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

        #region CheckStudentCredentials
        public bool CheckStudentCredentials(string Email)
        {
            try
            {
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["CheckStudentLoginExistense"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@RegNo", Email));
                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
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

        #region CheckUserACcountIsActiveOrNot
        public bool CheckActivation(string EmailId)
        {
            try
            {
                string HosName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HosName].ConnectionString;
                string query = "declare @VerifiedStatus int = (select IsVerified from UserTbl where EmailId = @EmailId)"+
                    " if (@VerifiedStatus != 0)"+
                    " select 1;"+
                    " else"+
                    " select 0; ";//QueryConfig.BookQuerySettings["CheckUserAccountActivation"].ToString();
                using(SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@EmailId",EmailId));
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

        #region CheckStudentAccountIsActiveOrNot
        public bool CheckStudentAccountActivation(string UserRegId)
        {
            try
            {
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["CheckStudentAccountVerification"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@StudentRegId", UserRegId));
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

        #region CheckStudentExistance
        public bool CheckStudentExistance(string RegId)
        {
            try
            {
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ConnectionString;
                string _query = QueryConfig.BookQuerySettings["CheckStudentExistance"].ToString();
                using(SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(_query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@StudentRegNo", RegId));
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

        #region UserLogin
        public StudentModel UserLogin(ItemCode credentials)
        {
            try
            {
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ToString();
                string query = QueryConfig.BookQuerySettings["fetchUserLoginData"].ToString();
                StudentModel student = new StudentModel();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@EmailId", credentials.Value));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if(rdr.Read())
                        {
                            student.StudentRole = rdr["RoleName"].ToString();
                            student.StudentName = rdr["StudentName"].ToString();
                            student.StudentGuid = rdr["StudentGuid"].ToString();
                            student.Password = rdr["Password"].ToString();
                            student.PasswordSalt = rdr["PasswordSalt"].ToString();
                            student.StudentImagePath = rdr["StudentImagePath"].ToString();
                        }
                        if (student != null)
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

        #region Student Login
        public StudentModel StudentLogin(ItemCode data)
        {
            try
            {
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetStudentLoginDetails"].ToString();
                StudentModel studentdata = new StudentModel();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@StRegId", data.Value));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if(rdr.Read())
                        {
                            Password = rdr["Password"].ToString();
                            Passwordsalt = rdr["Passwordsalt"].ToString();
                            studentdata.StudentName = rdr["FirstName"].ToString() + " " + rdr["LastName"].ToString();
                            studentdata.RoleName = rdr["RoleName"].ToString();
                            studentdata.StudentGuid = rdr["StudentGuid"].ToString();
                            studentdata.StudentImagePath = rdr["StudentImagePath"].ToString().Substring(1);
                            studentdata.Email = rdr["Email"].ToString();
                        }
                        if(studentdata != null)
                        {
                            string UserPassword = PasswordHasher.PasswordHash(data.Code, Passwordsalt);
                            if(UserPassword == Password)
                            {
                                return studentdata;
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
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region AdminstrationLogin
        public TeachersEditModel AdminstrationLogin(ItemCode Credentials)
        {
            try
            {
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["TeacherLoginData"].ToString();
                TeachersEditModel TeacherDetails = new TeachersEditModel();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@EmailId", Credentials.Value));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            UserRole = rdr["Role"].ToString();
                            Password = rdr["Password"].ToString();
                            Passwordsalt = rdr["PasswordSalt"].ToString();
                        }
                        if (Password != null || Password != string.Empty || Passwordsalt != null || Passwordsalt != string.Empty)
                        {
                            string userPassword = PasswordHasher.PasswordHash(Credentials.Code, Passwordsalt);
                            if (Password == userPassword)
                            {
                                return TeacherDetails;
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

        #region SetVerificationCodeAgain
        #region SetStudentVerificationCode
        public bool SetStudentVerificationCode(string RegId)
        {
            try
            {
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ConnectionString;
                string _query = QueryConfig.BookQuerySettings["RetriveStudentEmail"].ToString();
                string query = QueryConfig.BookQuerySettings["DeleteStudentExistingVerificationCode"].ToString();
                string query_ = QueryConfig.BookQuerySettings["SetNewStudentVerificationCode"].ToString();
                string VerificationCode = Guid.NewGuid().ToString().Substring(0, 8);
                string EmailTemplatepath = ConfigurationManager.AppSettings["StudentAccountVerificationTemplateEmail"].ToString();
                SqlConnection con;
                SqlCommand cmd;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction("first");
                    try
                    {
                        using (cmd = new SqlCommand(_query, con, transaction))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new SqlParameter("@StudentGuid", RegId));
                            SqlDataReader rdr = cmd.ExecuteReader();
                            if(rdr.Read())
                            {
                                Password = !string.IsNullOrEmpty(rdr["Email"].ToString()) ? rdr["Email"].ToString() : string.Empty;
                            }
                            if(Password != null && Password != string.Empty)
                            {
                                rdr.Close();
                                using (cmd = new SqlCommand(query, con, transaction))
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Parameters.Add(new SqlParameter("@StudentGuid", RegId));
                                    if(cmd.ExecuteNonQuery() > 0)
                                    {
                                        using (cmd = new SqlCommand(query_, con, transaction))
                                        {
                                            cmd.CommandType = CommandType.Text;
                                            cmd.Parameters.Add(new SqlParameter("@StudentGuid", RegId));
                                            cmd.Parameters.AddWithValue("@VerificationCode", VerificationCode);
                                            if(cmd.ExecuteNonQuery() > 0)
                                            {
                                                EmailSender.SendStudentAccountVerificationEmail(Password, VerificationCode, "Account Verification", EmailTemplatepath, RegId);
                                                transaction.Commit();
                                                return true;
                                            }
                                            else
                                            {
                                                transaction.Rollback();
                                                return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                        return false;
                                    }
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

        #region SetUserVerificationCodeAgain
        public bool SetUserVerificationCodeAgain(ItemCode StdId)
        {
            try
            {
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["DeleteExistingUserVerificationCode"].ToString();
                string query_ = QueryConfig.BookQuerySettings["AddUserNewVerificationCode"].ToString();
                string EmailTemplatePath = ConfigurationManager.AppSettings["UserAccountVerificationTemplateEmail"].ToString();
                string VerificationCode = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
                SqlConnection con;
                SqlCommand cmd;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction("First");
                    try
                    {
                        using (cmd = new SqlCommand(query, con, transaction))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new SqlParameter("@UserEmailId", StdId.Code));
                            if(cmd.ExecuteNonQuery() > 0)
                            {
                                using (cmd = new SqlCommand(query_, con, transaction))
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Parameters.Add(new SqlParameter("@UserEmailId", StdId.Code));
                                    cmd.Parameters.AddWithValue("@VerificationCode", VerificationCode);
                                }
                                if(cmd.ExecuteNonQuery () > 0)
                                {
                                    EmailSender.SendUserAccountVerificationEmail(StdId.Code, VerificationCode, "UserAccount Verification Code", EmailTemplatePath);
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

        #region GetVerificationCode
        public string GetVerificationCode(ItemCode code)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["RetriveVerificationCode"].ToString();
                string query_ = QueryConfig.BookQuerySettings["RemoveUserVerificationCode"].ToString();
                string _query = QueryConfig.BookQuerySettings["ActiveUserAccount"].ToString();
                SqlConnection con;
                SqlCommand cmd;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction("First");
                    try
                    {
                        using (cmd = new SqlCommand(query, con, transaction))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new SqlParameter("@Value", code.Value));
                            SqlDataReader rdr = cmd.ExecuteReader();
                            if (rdr.Read())
                            {
                                VerificationCode = rdr["IdentityCode"].ToString();
                            }
                            if (VerificationCode != null && VerificationCode != string.Empty)
                            {
                                rdr.Close();
                                using (cmd = new SqlCommand(query_, con, transaction))
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Parameters.Add(new SqlParameter("@Value", code.Value));
                                    if (cmd.ExecuteNonQuery() > 0)
                                    {
                                        using (cmd = new SqlCommand(_query, con, transaction))
                                        {
                                            cmd.Parameters.Add(new SqlParameter("@Email", code.Value));
                                            cmd.Parameters.AddWithValue("@IsVerified", true);
                                            if (cmd.ExecuteNonQuery() > 0)
                                            {
                                                transaction.Commit();
                                                return VerificationCode;
                                            }
                                            else
                                            {
                                                transaction.Rollback();
                                                return null;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                        return null;
                                    }
                                }
                            }
                            else
                            {
                                transaction.Rollback();
                                return null;
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

        #region Checks
        #region CheckTeacherExistence
        public bool CheckTeacherExistence(string Email)
        {
            string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
            string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
            string Query = QueryConfig.BookQuerySettings["CheckTeacherExistance"].ToString();
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@Email", Email));
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
        #endregion
        #endregion
        #endregion

        #region Post

        #region User(Students Registration For WebSight)
        public bool RegisterNewStudent(StudentModel RegNewStudent)
        {
            try
            {
                string guid = Guid.NewGuid().ToString();
                string hostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[hostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["RegisterNewUser"].ToString();
                string query_ = QueryConfig.BookQuerySettings["AdduserVerifiactionCode"].ToString();
                Passwordsalt = PasswordHasher.SaltGenerator(20);
                Password = PasswordHasher.PasswordHash(RegNewStudent.Password, Passwordsalt);
                string FolderPath = ConfigurationManager.AppSettings["UserImageUploadPath"].ToString();
                string ImageFolderPath = CreateFolder.UploadEventImagePath(RegNewStudent.StudentName + Guid.NewGuid().ToString().Substring(0, 6), FolderPath);/*ImageRootFolder(RegNewStudent.StudentName + Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8).ToUpper());*/
                string path = UploadImage.Upload(RegNewStudent.StudentImageFile, ImageFolderPath);
                string EmailTemplatePath = ConfigurationManager.AppSettings["UserAccountVerificationTemplateEmail"].ToString();
                SqlConnection con;
                SqlCommand cmd;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction("First");
                    try
                    {
                        using (cmd = new SqlCommand(query, con,transaction))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new SqlParameter("@GenderGuid", RegNewStudent.GenderGuid));
                            cmd.Parameters.AddWithValue("@StudentName", RegNewStudent.StudentName);
                            cmd.Parameters.AddWithValue("@Password", Password);
                            cmd.Parameters.AddWithValue("@PasswordSalt", Passwordsalt);
                            cmd.Parameters.AddWithValue("@IsVerified", false);
                            cmd.Parameters.AddWithValue("@EmailId", RegNewStudent.Email);
                            cmd.Parameters.AddWithValue("@StudentImagePath", path);
                            cmd.Parameters.AddWithValue("@CreatedOn", DateTimeOffset.UtcNow);
                            cmd.Parameters.AddWithValue("@StudentGuid", guid);
                        }
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            using (cmd = new SqlCommand(query_, con,transaction))
                            {
                                string verificationId = Guid.NewGuid().ToString().Replace("-", "");
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.AddWithValue("@IdentityCode", verificationId);
                                cmd.Parameters.Add(new SqlParameter("@guid", guid));
                                if (cmd.ExecuteNonQuery() > 0)
                                {
                                    EmailSender.SendUserAccountVerificationEmail(RegNewStudent.Email, verificationId, "Account Verification", EmailTemplatePath);
                                    transaction.Commit();
                                    return true;
                                }
                                else
                                {
                                    File.Delete(HttpContext.Current.Server.MapPath(FolderPath));
                                    transaction.Rollback();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(FolderPath));
                            transaction.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        File.Delete(HttpContext.Current.Server.MapPath(FolderPath));
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

        #region CreateTeacherAccount
        public bool CreateTeacherAccount(ItemCode code)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["AddNewTeacherCredential"].ToString();
                Passwordsalt = PasswordHasher.SaltGenerator(20);
                Password = PasswordHasher.PasswordHash(code.Code, Passwordsalt);
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Email", code.Value));
                        cmd.Parameters.AddWithValue("@TEmail", code.Value);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        cmd.Parameters.AddWithValue("@PasswordSalt", Passwordsalt);
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

        #region CreateStudentAccount
        public bool CreateStudentAccount(ItemCode code)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["AddStudentLoginCredentials"].ToString();
                string Query_ = QueryConfig.BookQuerySettings["AddStudentVerificationCode"].ToString();
                string _Query = QueryConfig.BookQuerySettings["RetriveStudentEmail"].ToString();
                string VerificationCode = Guid.NewGuid().ToString().Substring(0, 8);
                string EmailTemplatepath = ConfigurationManager.AppSettings["StudentAccountVerificationTemplateEmail"].ToString();
                string StudentEmailId = null;
                Passwordsalt = PasswordHasher.SaltGenerator(20);
                Password = PasswordHasher.PasswordHash(code.Code, Passwordsalt);
                SqlConnection con;
                SqlCommand cmd;
                SqlDataReader rdr;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction("first");
                    try
                    {
                        using (cmd = new SqlCommand(_Query, con, transaction))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new SqlParameter("@StudentGuid", code.Value));
                            rdr = cmd.ExecuteReader();
                            if(rdr.Read())
                            {
                                StudentEmailId = rdr["Email"].ToString();
                            }
                            if(StudentEmailId != null && StudentEmailId != string.Empty)
                            {
                                rdr.Close();
                                using (cmd = new SqlCommand(Query, con, transaction))
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Parameters.Add(new SqlParameter("@RegId", code.Value));
                                    cmd.Parameters.AddWithValue("@StudentRegNo", code.Value);
                                    cmd.Parameters.AddWithValue("@Password", Password);
                                    cmd.Parameters.AddWithValue("@PasswordSalt", Passwordsalt);
                                    cmd.Parameters.AddWithValue("@IsVerified", false);
                                    if (cmd.ExecuteNonQuery() > 0)
                                    {
                                        using (cmd = new SqlCommand(Query_, con, transaction))
                                        {
                                            cmd.CommandType = CommandType.Text;
                                            cmd.Parameters.Add(new SqlParameter("@RegId", code.Value));
                                            cmd.Parameters.AddWithValue("@VerificationCode", VerificationCode);
                                            if (cmd.ExecuteNonQuery() > 0)
                                            {
                                                EmailSender.SendStudentAccountVerificationEmail(StudentEmailId, VerificationCode, "Account Verification Email", EmailTemplatepath,code.Value);
                                                transaction.Commit();
                                                return true;
                                            }
                                            else
                                            {
                                                transaction.Rollback();
                                                return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        transaction.Rollback();
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

        #region ForgetPassword
        #region ResetUsingExistingPassword

        #endregion
        #region PerformForgetPassword

        #endregion
        #endregion

        #region Verifies
        #region VerifyStudentAccount
        public bool VerifyStudentAccount(ItemCode data)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["RetriveStudentVerificationCode"].ToString();
                string Query_ = QueryConfig.BookQuerySettings["VerifyStudentAccount"].ToString();
                string _Query_ = QueryConfig.BookQuerySettings["DeleteUsedVerificationCode"].ToString();
                SqlConnection con;
                SqlCommand cmd;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction("first");
                    try
                    {
                        using (cmd = new SqlCommand(Query, con, transaction))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new SqlParameter("@RegId", data.Value));
                            SqlDataReader rdr = cmd.ExecuteReader();
                            if (rdr.Read())
                            {
                                Password = !string.IsNullOrEmpty(rdr["VerificationCode"].ToString()) ? rdr["VerificationCode"].ToString() : string.Empty;
                            }
                            if (Password != null && Password != string.Empty)
                            {
                                rdr.Close();
                                using (cmd = new SqlCommand(Query_, con, transaction))
                                {
                                    if(Password == data.Code)
                                    {
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Parameters.Add(new SqlParameter("@StuRegId", data.Value));
                                        cmd.Parameters.AddWithValue("@IsVerified", true);
                                        if (cmd.ExecuteNonQuery() > 0)
                                        {
                                            using (cmd = new SqlCommand(_Query_, con, transaction))
                                            {
                                                cmd.CommandType = CommandType.Text;
                                                cmd.Parameters.Add(new SqlParameter("@StRegId", data.Value));
                                                if(cmd.ExecuteNonQuery() > 0)
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
        #region Update Password
        public bool ForgetPassword(string UserId)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["SetForgetPasswordVerificationCode"].ToString();
                string EmailTemplatePath = ConfigurationManager.AppSettings["ForgetPasswordVerificationCodeEmailTemplate"].ToString();
                VerificationCode = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction("first");
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(Query, con, transaction))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                            cmd.Parameters.AddWithValue("@VerificationCode", VerificationCode);
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                EmailSender.ForgetPasswordVerificationCodeEmail(UserId, "Forget Password Link", VerificationCode, EmailTemplatePath);
                                transaction.Commit();
                                return true;
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

        #region UpdatePassword
        public bool UpdatePasword(BaseModel credentials)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["UpdatePassword"].ToString();
                string EmailTemplatePath = ConfigurationManager.AppSettings["PasswordChangeConfirmationEmailTemplate"].ToString();
                Passwordsalt = PasswordHasher.SaltGenerator(20);
                Password = PasswordHasher.PasswordHash(credentials.Code, Passwordsalt);
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@CreatedBy", credentials.CreatedBy));
                        cmd.Parameters.AddWithValue("@Password", Password);
                        cmd.Parameters.AddWithValue("@Passwordsalt", Passwordsalt);
                        if(cmd.ExecuteNonQuery() > 0)
                        {
                            EmailSender.PasswordChangeConfirmation(credentials.CreatedBy, "Password Changed Successfully", EmailTemplatePath);
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
