using CMS.BUSINESS.IBusiness.ILibrary;
using CMS.Filter;
using CMS.MODEL.Book;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Library
{
    public class BooksControlController : Controller
    {
        #region Constructor And Parameter
        private readonly IBooksControlBusiness _BooksControlBusiness;
        public BooksControlController(IBooksControlBusiness BooksControlBusiness)
        {
            this._BooksControlBusiness = BooksControlBusiness;
        }
        #endregion

        #region Post
        public ActionResult AddNewBooks(BooksModel books)
        {
            try
            {
                books.CreatedBy = Session["UserGuid"]?.ToString();
                if(books.CreatedBy != null && books.CreatedBy != string.Empty)
                {

                    bool result = this._BooksControlBusiness.AddNewBooks(books);
                    if (result == true)
                    {
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion
    }
}