using CMS.MODEL.AuthenticationAndAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.UTILITIES.AuthenticationAndAuthorization
{
    public interface IAuthenticate
    {
        UserDataModel GetClientRegsDetailsbyCLientEmailId(string EmailId, string Password);
        bool ValidateKey(string userEMail);
        bool IsTokenAlreadyExists(string UserGuid);
        int DeleteGenerateToken(string UserGuid);
        bool InsertToken(TokensManager token, UserDataModel User);
        string GenerateToken(DateTime IssuedOn, UserDataModel User);
    }
}
