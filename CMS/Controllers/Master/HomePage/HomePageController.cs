using CMS.BUSINESS.IBusiness.IMaster.IHomePage;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Master.HomePage
{
    public class HomePageController : Controller
    {
        #region Parameters And Constructor
        private readonly IHomePageBusiness _iHomePageBusiness;
        public HomePageController(IHomePageBusiness iHomePageBusiness)
        {
            this._iHomePageBusiness = iHomePageBusiness;
        }
        #endregion

        #region Get(HomePage Contents By Parts)

        #region HomePage
        [HttpGet]
        public ActionResult Home()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region GetEventsSection

        #endregion

        #region GetNewsSection

        #endregion

        #region ImageSlideImagesSection

        #endregion

        #endregion

        #region Post
        #region NewsLetter
        public ActionResult NewsLetter(string Email)
        {
            bool result = this._iHomePageBusiness.Newsletter(Email);
            if(result == true)
            {
                return View();
            }
            else
            {
                return View();
            }
        }
        #endregion
        #endregion

        //#region Update
        //#region UnsubscribeNewsLetter
        //public ActionResult UnsubscribeNewsLetter(string Email)
        //{

        //}
        //#endregion
        //#endregion
    }
}