using CMS.BUSINESS.IBusiness.IEvent;
using CMS.MODEL.Event;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.IEvent;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.Event
{
    public class EventBusiness : IEventBusiness
    {
        #region Parameter And Variables
        private readonly IEventRepository _eventRepository;

        public EventBusiness(IEventRepository eventRepository)
        {
            this._eventRepository = eventRepository;
        }
        #endregion

        #region HomePage Contents
        #region EventContent
        public List<EventCardModel> HomePageEventContents()
        {
            return this._eventRepository.HomePageEventContents();
        }
        #endregion
        #endregion

        #region Get
        #region Get All Events
        public List<EventCardModel> AllEvents()
        {
            return this._eventRepository.AllEvents();
        }
        #endregion

        #region CheckEventSchedule
        public bool CheckEventSchedule(EventEditModel itemCode)
        {
            try
            {
                return this._eventRepository.CheckEventSchedule(itemCode);
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region GetTop3CurrentEvents
        public List<EventCardModel> GetCurrentEvents()
        {
            return this._eventRepository.GetCurrentEvents();
        }
        #endregion

        #region GetAllCurrentEvents
        public List<EventCardModel> GetAllCurrentEvents()
        {
            return this._eventRepository.GetAllCurrentEvents();
        }
        #endregion

        #region Get Upcoming Events
        public List<EventCardModel> UpcomingEvents()
        {
            return this._eventRepository.UpcomingEvents();
        }
        #endregion

        #region GetEventByEventId
        public Tuple<List<EventScheduleEditModel>, List<EventCardModel>> GetEventScheduleById(string EventGuid)
        {
            try
            {
                if(EventGuid != null || EventGuid != string.Empty)
                {
                    return this._eventRepository.GetEventScheduleById(EventGuid);
                }
                else
                {
                    return null;
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
            if(EviD != null && EviD != string.Empty && SchId != null && SchId != string.Empty)
            {
                return this._eventRepository.EventDetails(EviD, SchId);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region EventGallery
        #region GetEventGalleryEventsList
        public List<EventGalleryModel> EventGallery()
        {
            return this._eventRepository.EventGallery();
        }
        #endregion

        #region EventImaegsByEventId
        public Tuple<List<EventGalleryModel>, EventGalleryModel> EventImages(string EvID)
        {
            return this._eventRepository.EventImages(EvID);
        }
        #endregion
        #endregion
        #endregion

        #region Post
        #region AddNewEvent
        public bool RegisterNewEvent(EventEditModel regevent, ScheduleEditModel Schedule)
        {
            try
            {
            //    if (this._eventRepository.CheckEventSchedule(regevent))
            //    {
                    return this._eventRepository.RegisterNewEvent(regevent,Schedule);
                //}
                //else
                //{
                //    return false;
                //}
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
            return this._eventRepository.BookEvent(BookEvent);
        }
        #endregion
        #endregion
    }
}
