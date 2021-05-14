using CMS.BUSINESS.IBusiness.IEvent;
using CMS.Filter;
using CMS.MODEL.Event;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Event
{
    public class EventController : Controller
    {
        #region Parameters And Constructor
        private readonly IEventBusiness _eventBusiness;

        public EventController(IEventBusiness eventBusiness)
        {
            this._eventBusiness = eventBusiness;
        }
        #endregion

        #region Get

        #region Events
        public ActionResult Events()
        {
            try
            {
                var CurrentEvents = this._eventBusiness.GetCurrentEvents();
                var UpcomingEvents = this._eventBusiness.UpcomingEvents();
                var Events = new Tuple<List<EventCardModel>, List<EventCardModel>>(CurrentEvents, UpcomingEvents);
                if(Events != null)
                {
                    return View(Events);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region GetCurrentEvents
        [HttpGet]
        public ActionResult AllEvents()
        {
            try
            {
                List<EventCardModel> result = this._eventBusiness.GetAllCurrentEvents();
                if(result != null)
                {
                    return View(result);
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

        #region Get Upcoming Events
        public ActionResult UpcomingEvents()
        {
            try
            {
                List<EventCardModel> result = this._eventBusiness.UpcomingEvents();
                if(result != null)
                {
                    return View(result);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region EventSchedulesByEventId
        public ActionResult EventSchedules(string EviD)

        {
            try
            {
                var result = this._eventBusiness.GetEventScheduleById(EviD.ToUpper());
                if(result != null)
                {
                    return View(result);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region GetEventDetails
        public ActionResult EventDetails(string EviD , string SchiD)
        {
            try
            {
                var result = this._eventBusiness.EventDetails(EviD, SchiD);
                if (result != null)
                {
                    return View(result);
                }
                else
                {
                    return View();
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
        public ActionResult EventGallery()
        {
            var result = this._eventBusiness.EventGallery();
            return View(result);
        }
        #endregion

        #region EventImaegsByEventId
        public ActionResult EventImages(string EvID)
        {
            var result = this._eventBusiness.EventImages(EvID);
            return View(result);
        }
        #endregion
        #endregion
        #endregion

        #region Post
        #region AddNewEvent
        [HttpPost]
        public ActionResult AddEvent(EventEditModel AddEvent , ScheduleEditModel Schedule)
        {
            try
            {
                bool result = this._eventBusiness.RegisterNewEvent(AddEvent, Schedule);
                if (result == true)
                {
                    return RedirectToAction("AdminstrationPortal", "AdminPortal");
                }
                return View("EventErrorViewPage"); //Needed To add This View Page
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region BookEvent
        [HttpPost]
        public ActionResult BookEvent(EventBookingModel eventBooking)
        {
            try
            {
                bool result = this._eventBusiness.BookEvent(eventBooking);
                if(result == true)
                {
                    return View("BookEventSuccess");
                }
                else
                {
                    return View();
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
