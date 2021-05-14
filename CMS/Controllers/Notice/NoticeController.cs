using CMS.BUSINESS.IBusiness.INotice;
using CMS.Filter;
using CMS.MODEL.Notice;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Notice
{
    public class NoticeController : Controller
    {
        #region Parameter And Constructor
        private readonly INoticeBusiness _noticeBusiness;
        public NoticeController(INoticeBusiness noticeBusiness)
        {
            this._noticeBusiness = noticeBusiness;
        }
        #endregion

        #region Post
        #region Create New Text Notice
        [HttpGet]
        //[UserAuthorizeAttribute(RoleUser = "Teacher,Admin")]
        public ActionResult CreateTextNotice()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTextNotice(NoticeEditModel Createnotice)
        {
            try
            {
                    Createnotice.CreatedBy = Session["UserGuid"].ToString();
                //if(Createnotice.CreatedBy != null && Createnotice.CreatedBy != string.Empty)
                //{
                    bool result = this._noticeBusiness.CreateNewNotice(Createnotice);
                    if (result == true)
                    {
                        ViewBag.IsSuccess = true;
                        ViewBag.NoticeName = Createnotice.NoticeTitle;
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                //}
                //else
                //{
                //    return RedirectToAction("Login", "Accounts");
                //}
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region Create New Visual Notice
        [HttpGet]
        //[UserAuthorizeAttribute(RoleUser = "Teacher,Admin")]
        public ActionResult CreateVisualNotice()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateVisualNotice(VisualNoticeEditModel Createnotice)
        {
            try
            {
                bool result = this._noticeBusiness.CreateNewVisualNotice(Createnotice);
                if (result == true)
                {
                    ViewBag.IsSuccess = true;
                    return View();
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

        #region Get
        #region ViewNotices
        public ActionResult Notices()
        {
            try
            {
                var result = this._noticeBusiness.GetNotices();
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

        #region View Full Notice
        public ActionResult ViewNotice(string NgId)
        {
            try
            {
                var result = this._noticeBusiness.ViewNotice(NgId);
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
        #endregion

    }
}