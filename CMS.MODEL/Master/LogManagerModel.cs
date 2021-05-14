using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.MODEL.Master
{
    public class LogManagerModel
    {
        public string ErrorType { get; set; }
        public string ErrorLocation { get; set; }
        public string ErrorDetails { get; set; }
        public DateTimeOffset ErrorCreatedOn { get; set; }
        public bool IsErrorHandled { get; set; }
        public int UserId { get; set; }
    }
}
