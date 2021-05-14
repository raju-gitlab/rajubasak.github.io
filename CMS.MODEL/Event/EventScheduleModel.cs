using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.MODEL.Event
{
    public class EventScheduleModel
    {
        public int EventId { get; set; }
    }
    public class EventScheduleEditModel : EventCardModel
    {
        public string EventScheduleName { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
        public string EventDescription { get; set; }
        public string ScheduleGuid { get; set; }
        public DateTime ScheduleDate { get; set; }
    }
    public class ScheduleEditModel
    {
        public string[] ScheduleName { get; set; }
        public DateTime[] ScheduleStartingTime { get; set; }
        public DateTime[] ScheduleEndingTime { get; set; }
        public string[] EventScheduleDescription { get; set; }
        public string[] ScheduleGUID { get; set; }
        public DateTime[] ScheduleEventDate { get; set; }
        public int[] TotalInvitations { get; set; }
    }
}
