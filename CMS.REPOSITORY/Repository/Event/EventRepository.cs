using ApplicationTools.Configuration;
using ApplicationTools.EmailSender;
using CMS.MODEL.Event;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.IEvent;
using CMS.UTILITIES.DateTimeHandel;
using CMS.UTILITIES.FolderCeration;
using CMS.UTILITIES.ImageUpload;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.REPOSITORY.Repository.Event
{
    public class EventRepository : IEventRepository
    {
        #region HomePage Contents
        #region EventsSection
        public List<EventCardModel> HomePageEventContents()
        {
            try
            {
                List<EventCardModel> events = new List<EventCardModel>(); 
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["HomePageEventControl"].ToString();
                using(SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(Query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            events.Add(new EventCardModel
                            {
                                EventName = rdr["EventName"].ToString(),
                                EventGuid = rdr["EventGuid"].ToString(),
                                EventDate = !string.IsNullOrEmpty(rdr["EventDate"].ToString()) ? DateTimeOffset.Parse(rdr["EventDate"].ToString()) : DateTimeOffset.Parse(string.Empty),
                                EventShortDetails = !string.IsNullOrEmpty(rdr["EventShortDescription"].ToString()) ? rdr["EventShortDescription"].ToString() : string.Empty,
                                ImagePath = !string.IsNullOrEmpty(rdr["ImagePath"].ToString()) ? rdr["ImagePath"].ToString() : string.Empty,
                                MonthName = DateTimeManagement.GetMonthName(rdr["EventDate"].ToString()),
                                Location = rdr["Location"].ToString()
                            });
                        }
                        if(events != null)
                        {
                            return events;
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
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion 

        #endregion

        #region Get
        #region Get All Events
        public List<EventCardModel> AllEvents()
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["s"].ToString();
                List<EventCardModel> Events = new List<EventCardModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            Events.Add(new EventCardModel
                            {
                                EventName = rdr["EventName"].ToString(),
                                EventGuid = rdr["EventGuid"].ToString(),
                                EventDate = !string.IsNullOrEmpty(rdr["EventDate"].ToString()) ? DateTimeOffset.Parse(rdr["EventDate"].ToString()) : DateTimeOffset.Parse(string.Empty),
                                EventShortDetails = !string.IsNullOrEmpty(rdr["EventShortDescription"].ToString()) ? rdr["EventShortDescription"].ToString() : string.Empty,
                                ImagePath = !string.IsNullOrEmpty(rdr["ImagePath"].ToString()) ? rdr["ImagePath"].ToString() : string.Empty,
                                MonthName = DateTimeManagement.GetMonthName(rdr["EventDate"].ToString()),
                                Location = rdr["Location"].ToString()
                            });
                        }
                        if(Events != null)
                        {
                            return Events;
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
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region CheckEventSchedule
        public bool CheckEventSchedule(EventEditModel Checkevent)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings[""].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@EventDate", Checkevent.EventDate));
                        cmd.Parameters.Add(new SqlParameter("@Time", Checkevent.StartingTime));
                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
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
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region GetTop3CurrentEvent
        public List<EventCardModel> GetCurrentEvents()
        {
            try
            {
                string monthName = new DateTime(2010, 8, 1).ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture);
                List<EventCardModel> events = new List<EventCardModel>();
                EventCardModel AllEvents = new EventCardModel();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetListOfCurrentEvents"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            events.Add(new EventCardModel {
                                EventName = rdr["EventName"].ToString(),
                                EventDate = DateTimeOffset.Parse(rdr["EventDate"].ToString()),
                                ImagePath = rdr["ImagePath"].ToString(),
                                EventGuid = rdr["EventGuid"].ToString(),
                                EventShortDetails = rdr["EventHeader"].ToString(),
                                Location = rdr["Location"].ToString(),
                                MonthName = DateTimeManagement.GetMonthName(Convert.ToString(rdr["EventDate"])),
                                EventPlace = rdr["EventPlace"].ToString()
                            });
                        }
                        if(events != null)
                        {
                            return events;
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
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        public List<EventCardModel> GetAllCurrentEvents()
        {
            try
            {
                string monthName = new DateTime(2010, 8, 1).ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture);
                List<EventCardModel> events = new List<EventCardModel>();
                EventCardModel AllEvents = new EventCardModel();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetAllCurrentEvents"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            events.Add(new EventCardModel
                            {
                                EventName = rdr["EventName"].ToString(),
                                EventDate = DateTimeOffset.Parse(rdr["EventDate"].ToString()),
                                ImagePath = rdr["ImagePath"].ToString(),
                                EventGuid = rdr["EventGuid"].ToString(),
                                EventShortDetails = rdr["EventDescription"].ToString(),
                                Location = rdr["Location"].ToString(),
                                MonthName = DateTimeManagement.GetMonthName(Convert.ToString(rdr["EventDate"]))
                            });
                        }
                        if (events != null)
                        {
                            return events;
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
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region Get Upcoming Events
        public List<EventCardModel> UpcomingEvents()
        {
            try
            {
                List<EventCardModel> Events = new List<EventCardModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetListOfUpcomingEvents"].ToString();
                using(SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            Events.Add(new EventCardModel
                            {
                                EventName = rdr["EventName"].ToString(),
                                EventDate = DateTimeOffset.Parse(rdr["EventDate"].ToString()),
                                ImagePath = rdr["ImagePath"].ToString(),
                                EventGuid = rdr["EventGuid"].ToString(),
                                EventShortDetails = rdr["EventDescription"].ToString(),
                                Location = rdr["Location"].ToString(),
                                MonthName = DateTimeManagement.GetMonthName(Convert.ToString(rdr["EventDate"]))
                            });
                        }
                        if(Events != null)
                        {
                            return Events;
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
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region GetEventScheduleByEventId
        public Tuple<List<EventScheduleEditModel>, List<EventCardModel>> GetEventScheduleById(string EventGuid)
        {
            try
            {
                List<EventScheduleEditModel> SameEventsSchedules = new List<EventScheduleEditModel>();
                List<EventCardModel> RelatedEvents = new List<EventCardModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query_ = "select CE.EventName,ES.EndingTime ,CE.EventDescription,CE.EventGuid,ES.EventScheduleName,CE.ImagePath"+
                 " , CE.Location,ES.StartingTime,ES.ScheduleGuid,ES.EventDate from ClgEvent CE" +
                 " left join EventSchedule ES On ES.EventId = CE.Id"+
                 " where CE.EventGuid = @EventGuid";
                string _query = "select top 3 EventName , EventDate ,Location , EventDescription , ImagePath,EventGuid from ClgEvent where EventStatus = 'Current'";
                SqlConnection con;
                SqlCommand cmd;
                SqlDataReader rdr;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    using (cmd = new SqlCommand(query_, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@EventGuid", EventGuid));
                        rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            SameEventsSchedules.Add(new EventScheduleEditModel {
                                EventScheduleName = rdr["EventScheduleName"].ToString(),
                                ScheduleGuid = rdr["ScheduleGuid"].ToString(),
                                StartingTime = !string.IsNullOrEmpty(Convert.ToString(rdr["StartingTime"])) ? DateTime.Parse(rdr["StartingTime"].ToString()) : DateTime.Parse("00:00:00"),
                                EndingTime = !string.IsNullOrEmpty(Convert.ToString(rdr["EndingTime"])) ? DateTime.Parse(rdr["EndingTime"].ToString()) : DateTime.Parse("00:00:00"),
                                ImagePath = !string.IsNullOrEmpty(Convert.ToString(rdr["ImagePath"])) ? rdr["ImagePath"].ToString() :string.Empty,
                                EventDescription = rdr["EventDescription"].ToString(),
                                EventGuid = rdr["EventGuid"].ToString(),
                                EventName = rdr["EventName"].ToString(),
                                Location = rdr["Location"].ToString(),
                                EventDate = DateTimeOffset.Parse(rdr["EventDate"].ToString()),
                                MonthName = DateTimeManagement.GetMonthName(Convert.ToString(rdr["EventDate"]))
                            });
                        }
                        if(SameEventsSchedules != null)
                        {
                            rdr.Close();
                            using (cmd = new SqlCommand(_query,con))
                            {
                                cmd.CommandType = CommandType.Text;
                                rdr = cmd.ExecuteReader();
                                while(rdr.Read())
                                {
                                    RelatedEvents.Add(new EventCardModel {
                                        EventName = rdr["EventName"].ToString(),
                                        EventDate = DateTimeOffset.Parse(rdr["EventDate"].ToString()),
                                        ImagePath = rdr["ImagePath"].ToString(),
                                        EventGuid = rdr["EventGuid"].ToString(),
                                        EventShortDetails = rdr["EventDescription"].ToString(),
                                        Location = rdr["Location"].ToString(),
                                        MonthName = DateTimeManagement.GetMonthName(Convert.ToString(rdr["EventDate"]))
                                    });
                                }
                                if(RelatedEvents != null)
                                {
                                    return new Tuple<List<EventScheduleEditModel>, List<EventCardModel>>(SameEventsSchedules, RelatedEvents);
                                }
                                else
                                {
                                    return null;
                                }
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
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region GetEventByEventIdAndScheduleId
        public Tuple<List<EventEditModel>, List<EventCardModel>> EventDetails(string EviD, string SchId)
        {
            try
            {
                List<EventEditModel> EventDetails = new List<EventEditModel>();
                List<EventCardModel> EventCard = new List<EventCardModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = "select CE.EventName,ES.EndingTime , ES.EventDate,CE.EventDescription,CE.EventExpireDate,CE.EventGuid,ES.ScheduleGuid,CE.EventPlace,ES.EventScheduleName,CE.EventStatus,CE.EventType,CE.ImagePath" +
                " , CE.Location,ES.StartingTime,ES.TotalInvitations,CE.EntryFee from ClgEvent CE" +
                " left join EventSchedule ES On ES.EventId = CE.Id" +
                " where CE.EventGuid = @EventId and ES.ScheduleGuid = @ScheduleId";
                string query_ = "select EventName , EventDate ,Location , EventDescription , ImagePath,EventGuid from ClgEvent where EventStatus = 'Current'";
                SqlConnection con;
                SqlCommand cmd;
                SqlDataReader rdr;
                using(con = new SqlConnection(CS))
                {
                    con.Open();
                    using(cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@EventId", EviD));
                        cmd.Parameters.Add(new SqlParameter("@ScheduleId", SchId));
                        rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            EventDetails.Add(new EventEditModel
                            {
                                EventName = rdr["EventName"].ToString(),
                                EventDescription = rdr["EventDescription"].ToString(),
                                EntryFee = rdr["EntryFee"].ToString(),
                                StartingTime = DateTime.Parse(rdr["StartingTime"].ToString()),
                                EndingTime = DateTime.Parse(rdr["EndingTime"].ToString()),
                                EventDate = DateTimeOffset.Parse(rdr["EventDate"].ToString()),
                                EventExpirationDate = DateTimeOffset.Parse(rdr["EventExpireDate"].ToString()),
                                EventPlace = rdr["EventPlace"].ToString(),
                                EventScheduleName = rdr["EventScheduleName"].ToString(),
                                EventGuid = rdr["EventGuid"].ToString(),
                                ScheduleGuid = rdr["ScheduleGuid"].ToString(),
                                EventStatus = rdr["EventStatus"].ToString(),
                                EventType = rdr["EventType"].ToString(),
                                ImagePath = rdr["ImagePath"].ToString(),
                                Location = rdr["Location"].ToString(),
                                TotalInvitations = Convert.ToInt32(rdr["TotalInvitations"])
                            });
                        }
                        if(EventDetails != null)
                        {
                            rdr.Close();
                            using(cmd = new SqlCommand(query_,con))
                            {
                                cmd.CommandType = CommandType.Text;
                                rdr = cmd.ExecuteReader();
                                while(rdr.Read())
                                {
                                    EventCard.Add(new EventCardModel {
                                        EventName = rdr["EventName"].ToString(),
                                        EventDate = DateTimeOffset.Parse(rdr["EventDate"].ToString()),
                                        ImagePath = rdr["ImagePath"].ToString(),
                                        EventGuid = rdr["EventGuid"].ToString(),
                                        EventShortDetails = rdr["EventDescription"].ToString(),
                                        Location = rdr["Location"].ToString(),
                                        MonthName = DateTimeManagement.GetMonthName(Convert.ToString(rdr["EventDate"]))
                                    });
                                }
                                if(EventCard != null)
                                {
                                    return new Tuple<List<EventEditModel>, List<EventCardModel>>(EventDetails, EventCard);
                                }
                                else
                                {
                                    return null;
                                }
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
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region EventGallery
        #region GetEventGalleryEventsList
        public List<EventGalleryModel> EventGallery()
        {
            try
            {
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetEventGalleryEventsList"].ToString();
                List<EventGalleryModel> EventList = new List<EventGalleryModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            EventList.Add(new EventGalleryModel
                            {
                                EventName = rdr["EventName"].ToString(),
                                EventPlace = rdr["EventPlace"].ToString(),
                                Code = rdr["EventGuid"].ToString(),
                                ImageFolderPath = rdr["ImagePath"].ToString().Substring(1),
                                Value = rdr["EventDate"].ToString()
                            });
                        }
                        if(EventList.Count != 0)
                        {
                            return EventList;
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
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region EventImaegsByEventId
        public Tuple<List<EventGalleryModel>, EventGalleryModel> EventImages(string EvID)
        {
            try
            {
                List<ItemCode> cal = new List<ItemCode>();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetEventGallery"].ToString();
                EventGalleryModel EventList1 = new EventGalleryModel();
                List<EventGalleryModel> EventList = new List<EventGalleryModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@EventGuid", EvID));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            EventList1.EventName = rdr["EventName"].ToString();
                            EventList1.EventPlace = rdr["EventPlace"].ToString();
                            EventList1.EventDate = DateTimeOffset.Parse(rdr["EventDate"].ToString());
                            EventList1.ImageFolderPath = rdr["ImageFolderPath"].ToString();
                        }
                        if (EventList1 != null)
                        {
                            string[] filePaths = System.IO.Directory.GetFiles(HttpContext.Current.Server.MapPath(EventList1.ImageFolderPath));
                            List<ItemCode> files = new List<ItemCode>();
                            foreach (string filePath in filePaths)
                            {
                                EventList.Add(new EventGalleryModel { ImageFolderPath = EventList1.ImageFolderPath.Substring(1) + System.IO.Path.GetFileName(filePath), EventName = EventList1.EventName });
                            }
                            return new Tuple<List<EventGalleryModel>, EventGalleryModel>(EventList, EventList1);
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
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion
        #endregion

        #region Post
        #region AddNewEvent
        public bool RegisterNewEvent(EventEditModel addEvent, ScheduleEditModel Schedule)
        {
            try
            {
                SqlConnection con;
                SqlCommand cmd;
                string guid = Guid.NewGuid().ToString();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Path = ConfigurationManager.AppSettings["EventTemplateImagePath"].ToString();
                string ImageFolderPath = CreateFolder.UploadEventImagePath(addEvent.EventName, Path);
                string ImagePath = UploadImage.Upload(addEvent.ImageFile, ImageFolderPath);
                string query = QueryConfig.BookQuerySettings["CreateNewEvent"].ToString();
                string query_ = QueryConfig.BookQuerySettings["CreateEventSchedule"].ToString();
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction("First");
                    try
                    {
                        using (cmd = new SqlCommand(query, con, transaction))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@EventName", addEvent.EventName);
                            cmd.Parameters.AddWithValue("@EventDate", addEvent.EventDate);
                            cmd.Parameters.AddWithValue("@EventDescription", addEvent.EventDescription);
                            cmd.Parameters.AddWithValue("@EventExpireDate", addEvent.EventExpirationDate);
                            cmd.Parameters.AddWithValue("@EventGuid", guid);//EventType Missing
                            cmd.Parameters.AddWithValue("@EventPlace", addEvent.EventPlace);
                            cmd.Parameters.AddWithValue("@EventStatus", "Current");
                            cmd.Parameters.AddWithValue("@IsDeleted", addEvent.IsDeleted);
                            cmd.Parameters.AddWithValue("@Location", addEvent.Location);
                            cmd.Parameters.AddWithValue("@PreRegistration", addEvent.PreRegistration);
                            cmd.Parameters.AddWithValue("@EventHeader", addEvent.EventHeader);
                            cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
                            cmd.Parameters.AddWithValue("@EntryFee", addEvent.EntryFee);
                        }

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            int j = 0;
                            for(var i = 0; i < Schedule.ScheduleName.Length; i++)
                            {
                                var a = Schedule.ScheduleName[i].ToString();
                                var b = Schedule.ScheduleEndingTime[i].ToString();
                                var c = Schedule.ScheduleEventDate[i].ToString();
                                var d = Schedule.ScheduleStartingTime[i].ToString();
                                var e = Convert.ToInt32(Schedule.TotalInvitations[i]);
                                using (cmd = new SqlCommand(query_, con, transaction))
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Parameters.Add(new SqlParameter("@Guid", guid));
                                    cmd.Parameters.AddWithValue("@EventScheduleName", a);
                                    cmd.Parameters.AddWithValue("@StartingTime", d);
                                    cmd.Parameters.AddWithValue("@EndingTime", b);
                                    cmd.Parameters.AddWithValue("@EventDate", c);
                                    cmd.Parameters.AddWithValue("@TotalInvitations", e);
                                    cmd.Parameters.AddWithValue("@ScheduleGuid", Guid.NewGuid().ToString());
                                    j = cmd.ExecuteNonQuery();
                                }
                            }
                            if (j > 0)
                            {
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        LogManager.ErrorEntry(ex);
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region BookEvent
        public bool BookEvent(EventBookingModel BookEvent)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = "declare @EventId int = (select Id from ClgEvent where EventGuid = @Code)"+
                " declare @ScheduleId int = (select Id from EventSchedule where ScheduleGuid = @Value)" +
                " insert into EventBooking(CustomerFullName, EmailId, EventId, ScheduleId, TicketSerialNumber, IsTicketChecked, ContactNumber) values(@CustomerFullName, @EmailId, @EventId, @ScheduleId, @TicketSerialNumber, @IsTicketChecked, @ContactNumber)";// QueryConfig.BookQuerySettings[""].ToString();
                string TicketSerialNumber = "#KITMEVNT" + Guid.NewGuid().ToString().ToUpper().Replace("-", string.Empty).Substring(0, 6);
                string EmailTemplatePath = ConfigurationManager.AppSettings["EventBookingEmailTemplate"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(Query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Code", BookEvent.Code));
                        cmd.Parameters.Add(new SqlParameter("@Value", BookEvent.Value));
                        cmd.Parameters.AddWithValue("@CustomerFullName", BookEvent.CustomerName);
                        cmd.Parameters.AddWithValue("@TicketSerialNumber", TicketSerialNumber);
                        cmd.Parameters.AddWithValue("@EmailId", BookEvent.EmailId);
                        cmd.Parameters.AddWithValue("@ContactNumber", BookEvent.ContactNumber);
                        cmd.Parameters.AddWithValue("@IsTicketChecked", false);
                        if(cmd.ExecuteNonQuery() > 0)
                        {
                            EmailSender.BookingConfirmationEmail(BookEvent.EmailId, "Event Booking Confirmation",DateTime.Now.Date.ToString(), DateTime.Now.TimeOfDay.ToString(), TicketSerialNumber,EmailTemplatePath);
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
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion
        #endregion
    }
}
