using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.MODEL.Master
{
    public class ItemCode
    {
        public string Code { get; set; }
        public string Value { get; set; }
    }
    public class BaseModel : ItemCode
    {
        public bool IsVerified { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string Modifiedby { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public string MonthName { get; set; }
    }
}
