using ApplicationTools.Configuration;
using ApplicationTools.PasswordHasher;
using CMS.MODEL.AuthenticationAndAuthorization;
using CMS.UTILITIES.Connections;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApplicationTools.PasswordHasher.EncryptionLibrary;

namespace CMS.UTILITIES.AuthenticationAndAuthorization
{
    public class AuthenticateConcrete : IAuthenticate
    {

        public UserDataModel GetClientRegsDetailsbyCLientEmailId(string EmailId, string password)
        {
            try
            {
                UserDataModel user = new UserDataModel();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                SqlConnection con;
                SqlCommand cmd;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    string query = QueryConfig.BookQuerySettings["GetClientRegsDetails"].ToString();
                    using (cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@EmailId", EmailId));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            user.Email = rdr["EmailId"].ToString();
                            user.Password = rdr["Password"].ToString();
                            user.UserName = rdr["StudentName"].ToString();
                            user.UserGuid = rdr["StudentGuid"].ToString().ToUpper();
                            user.PasswordSalt = rdr["PasswordSalt"].ToString();
                            /*user.FirstName = rdr["FirstName"].ToString();
                            user.LastName = rdr["LastName"].ToString();
                            user.PhoneNumber = rdr["PhoneNumber"].ToString();*/
                        }
                        if (user != null)
                        {
                            string userpassword = PasswordHasher.PasswordHash(password, user.PasswordSalt);
                            if (userpassword == user.Password)
                            {
                                return user;
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
                throw new Exception("Error", ex);
            }
        }

        public bool ValidateKey(string userEMail)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    string Query = QueryConfig.BookQuerySettings["CheckUserExistense"].ToString();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@EmailID", userEMail));
                        int result = Convert.ToInt32(cmd.ExecuteScalar());
                        if (result > 0)
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
                throw new Exception("Error", ex);
            }
        }

        public bool IsTokenAlreadyExists(string UserGuid)
        {
            try
            {
                RedisConnection connection = new RedisConnection();
                IDatabase db = connection.Connection.GetDatabase();
                string result = db.StringGet(UserGuid);
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error", ex);
            }
        }

        public int DeleteGenerateToken(string UserGuid)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                RedisConnection connection = new RedisConnection();
                IDatabase db = connection.Connection.GetDatabase();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    string query = QueryConfig.BookQuerySettings["DeleteExistingToken"].ToString();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@UserGuid", UserGuid));
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            db.KeyDelete(UserGuid);
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public string GenerateToken(DateTime IssuedOn, UserDataModel User)
        {
            try
            {
                RoleModel role = new RoleModel();
                string randomnumber =
                   string.Join(":", new string[]
                   {
                       KeyGenerator.GetUniqueKey(),
                       User.PhoneNumber,
                       User.Email,
                       Convert.ToString(IssuedOn.Ticks),
                       User.Password,
                       User.UserName,
                       User.UserGuid,
                       User.FirstName,
                       User.LastName,
                       role.RolesName
                   });

                return EncryptionLibrary.EncryptText(randomnumber);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool InsertToken(TokensManager token, UserDataModel User)
        {
            RedisConnection rdis = new RedisConnection();
            IDatabase db = rdis.Connection.GetDatabase();
            try
            {
                var date = DateTime.Now;
                var minute = date.AddMinutes(5);
                int expiryTime = Convert.ToInt32(ConfigurationManager.AppSettings["TokenExpiry"]); 
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    string query = QueryConfig.BookQuerySettings["AddNewToken"].ToString();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@UserGuid", User.UserGuid));
                        cmd.Parameters.AddWithValue("@CreatedOn", token.CreatedOn);
                        cmd.Parameters.AddWithValue("@ExpiresOn", token.ExpiresOn);
                        cmd.Parameters.AddWithValue("@IssudOn", token.IssuedOn);
                        cmd.Parameters.AddWithValue("@TokenKey", token.TokenKey);
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            string value1 = JsonConvert.SerializeObject(token);
                            string value2 = JsonConvert.SerializeObject(User);
                            string value = string.Concat(value1, value2).Replace("}{", ",");
                            db.StringSet(User.UserGuid, value);
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

                throw new Exception("error", ex);
            }
        }

    }
}
