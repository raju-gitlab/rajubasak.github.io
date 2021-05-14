using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.MODEL.AuthenticationAndAuthorization
{
    public class TokensManager
    {
        public int TokenID { get; set; }
        public string TokenKey { get; set; }
        public DateTimeOffset IssuedOn { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int UserId { get; set; }
    }
}
