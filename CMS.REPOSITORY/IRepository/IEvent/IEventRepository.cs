using CMS.MODEL.Event;
using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.IRepository.IEvent
{
    public interface IEventRepository
    {
        #region HomePage Contents
        #region EventSection
        List<EventCardModel> HomePageEventContents();
        #endregion

        #endregion

        #region Get
        #region Get All Events
        List<EventCardModel> AllEvents();
        #endregion

        #region CheckSchedule
        bool CheckEventSchedule(EventEditModel itemCode);
        #endregion

        #region GetTop3CurrentEvents
        List<EventCardModel> GetCurrentEvents();
        #endregion

        #region GetAllCurrentEvents
        List<EventCardModel> GetAllCurrentEvents();
        #endregion

        #region Get Upcoming Events
        List<EventCardModel> UpcomingEvents();
        #endregion

        #region GetEventByEventId
        Tuple<List<EventScheduleEditModel>, List<EventCardModel>> GetEventScheduleById(string EventGuid);
        #endregion

        #region GetEventByEventIdAndScheduleId
        Tuple<List<EventEditModel>, List<EventCardModel>> EventDetails(string EviD, string SchId);
        #endregion

        #region EventGallery
        #region GetEventGalleryEventsList
        List<EventGalleryModel> EventGallery();
        #endregion

        #region EventImaegsByEventId
        Tuple<List<EventGalleryModel>, EventGalleryModel> EventImages(string EvID);
        #endregion
        #endregion
        #endregion

        #region Post
        #region AddNewEvent
        bool RegisterNewEvent(EventEditModel regevent,ScheduleEditModel Schedule);
        #endregion

        #region BookEvent
        bool BookEvent(EventBookingModel BookEvent);
        #endregion
        #endregion
    }
}
