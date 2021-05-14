using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.MODEL.Event
{
    public class EventEditModel : EventScheduleEditModel
    {
        public string EventType { get; set; }
        public string EventStatus { get; set; }
        public int TotalInvitations { get; set; }
        public DateTimeOffset EventExpirationDate { get; set; }
        public bool PreRegistration { get; set; }
        public bool IsDeleted { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public int RemainSeats { get; set; }
        public string EntryFee { get; set; }
        public string EventHeader { get; set; }
    }
    public class EventCardModel
    {
        public string EventName { get; set; }
        public DateTimeOffset EventDate { get; set; }
        public string ImagePath { get; set; }
        public string EventShortDetails { get; set; }
        public string EventGuid { get; set; }
        public string Location { get; set; }
        public string MonthName { get; set; }
        public string EventPlace { get; set; }
    }
}
