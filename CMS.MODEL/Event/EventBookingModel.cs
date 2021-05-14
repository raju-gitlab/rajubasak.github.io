using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.MODEL.Event
{
    public class EventBookingModel : BaseModel
    {
        public string CustomerName { get; set; }
        public string EmailId { get; set; }
        public string TicketSerialNumber { get; set; }
        public bool IsTicketChecked { get; set; }
        public string ContactNumber { get; set; }
    }
}
