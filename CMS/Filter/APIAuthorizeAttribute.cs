using ApplicationTools.Configuration;
using ApplicationTools.PasswordHasher;
using CMS.MODEL.AuthenticationAndAuthorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace CMS.Filter
{
    public class APIAuthorizeAttribute : AuthorizeAttribute
    {
        public string Role { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Authorize(filterContext))
            {
                if (validUserRole(filterContext, Role))
                {
                    return;
                }
            }
            HandleUnauthorizedRequest(filterContext);
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }

        private bool Authorize(AuthorizationContext actionContext)
        {
            try
            {
                var encodedString = actionContext.HttpContext.Request.Headers.GetValues("cookie").First();

                bool validFlag = false;

                if (!string.IsNullOrEmpty(encodedString))
                {
                    var key = EncryptionLibrary.DecryptText(encodedString);

                    string[] parts = key.Split(new char[] { ':' });

                    var RandomKey = parts[0];        // UserID
                    var PhoneNumber = Convert.ToString(parts[1]);                // Random Key
                    var Email = Convert.ToString(parts[2]);    // CompanyID
                    long ticks = long.Parse(parts[3]);            // Ticks
                    DateTime IssuedOn = new DateTime(ticks);
                    var Password = Convert.ToString(parts[4]);
                    var UserName = Convert.ToString(parts[5]);
                    var UserGuid = Convert.ToString(parts[6]);
                    var FirstName = Convert.ToString(parts[7]);
                    var LastName = Convert.ToString(parts[8]);
                    var RolesName = Convert.ToString(parts[9]);

                    string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                    string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                    UserDataModel userData = new UserDataModel();
                    TokensManager tokens = new TokensManager();
                    SqlConnection con;
                    SqlCommand cmd;
                    using (con = new SqlConnection(CS))
                    {
                        con.Open();
                        string query = QueryConfig.BookQuerySettings["CheckUserExistense"].ToString();
                        using (cmd = new SqlCommand(query, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            //cmd.Parameters.Add(new SqlParameter("@UserGuid", UserGuid));
                            cmd.Parameters.Add(new SqlParameter("@EmailID", Email));
                            int i = Convert.ToInt32(cmd.ExecuteScalar());
                            if (i > 0)
                            {
                                con.Close();
                                string Query = "select ExpiresOn from TokensManager WHERE TokenKey = @encodedString";
                                using (SqlConnection Con = new SqlConnection(CS))
                                {
                                    Con.Open();
                                    using (SqlCommand Cmd = new SqlCommand(Query, Con))
                                    {
                                        Cmd.CommandType = CommandType.Text;
                                        Cmd.Parameters.Add(new SqlParameter("@encodedString", encodedString));
                                        SqlDataReader Rdr = Cmd.ExecuteReader();
                                        if (Rdr.Read())
                                        {
                                            tokens.ExpiresOn = Convert.ToDateTime(Rdr["ExpiresOn"]);
                                        }
                                    }
                                }
                            }

                        }
                    }
                    if (tokens.ExpiresOn != null)
                    {
                        // Validating Time
                        /*var ExpiresOn = (from token in db.TokensManager
                                         where token.CompanyID == CompanyID
                                         select token.ExpiresOn).FirstOrDefault();*/

                        if ((DateTime.Now > tokens.ExpiresOn))
                        {
                            validFlag = false;
                        }
                        else
                        {
                            validFlag = true;
                        }
                    }
                    else
                    {
                        validFlag = false;
                    }
                }
                return validFlag;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }
        private bool validUserRole(AuthorizationContext filterContext, string Role)
        {
            try
            {
                var encodedString = filterContext.HttpContext.Request.Headers.GetValues("Access-Token").First();
                string[] Roles = Role.Split(new char[] { ',' });
                if (!string.IsNullOrEmpty(encodedString))
                {
                    var key = EncryptionLibrary.DecryptText(encodedString);
                    string[] parts = key.Split(new char[] { ':' });
                    var RolesUser = parts[9];
                    foreach (string i in Roles)
                    {
                        if (RolesUser == i)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}