using CMS.MODEL.AuthenticationAndAuthorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMS.UTILITIES.AuthenticationAndAuthorization
{
    public class TokenManagement
    {
        AuthenticateConcrete _IAuthenticate = new AuthenticateConcrete();

        public HttpResponseMessage Authenticate(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Password))
            {
                var message = new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                {
                    Content = new StringContent("Not Valid Request")
                };
                return message;
            }
            else
            {
                if (_IAuthenticate.ValidateKey(Email))
                {
                    UserDataModel User = new UserDataModel();
                    var UserDetails = _IAuthenticate.GetClientRegsDetailsbyCLientEmailId(Email, Password);

                    if (UserDetails == null)
                    {
                        var message = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent("User Not Found")
                        };
                        return message;
                    }
                    else
                    {
                        if (_IAuthenticate.IsTokenAlreadyExists(UserDetails.UserGuid))
                        {
                            _IAuthenticate.DeleteGenerateToken(UserDetails.UserGuid);

                            return GenerateandSaveToken(UserDetails);
                        }
                        else
                        {
                            return GenerateandSaveToken(UserDetails);
                        }
                    }
                }
                else
                {
                    var message = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("User Not Found")
                    };
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.NotAcceptable };
                }
            }
        }


        private HttpResponseMessage GenerateandSaveToken(UserDataModel User)
        {
            //See  that steps
            var IssuedOn = DateTime.Now;
            var newToken = _IAuthenticate.GenerateToken(IssuedOn, User);
            TokensManager token = new TokensManager()
            {
                TokenID = 0,
                TokenKey = newToken,
                IssuedOn = IssuedOn,
                ExpiresOn = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["TokenExpiry"])),
                CreatedOn = DateTime.Now
            };
            var result = _IAuthenticate.InsertToken(token, User);

            if (result == true)
            {
                try
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    AuthorizationContext authorization = new AuthorizationContext();
                    authorization.RequestContext.HttpContext.Request.Headers.Add("Token", newToken);
                    response.Headers.Add("Access-Token", newToken);
                    response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["TokenExpiry"]);
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                    //response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
                    return response;
                }
                catch (Exception ex)
                {
                    LogManager.LogManager.ErrorEntry(ex);
                    throw;
                }
            }
            else
            {
                var message = new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                {
                    Content = new StringContent("Error in Creating Token")
                };
                return message;
            }
        }
    }
}
