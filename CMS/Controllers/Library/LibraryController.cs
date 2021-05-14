using CMS.BUSINESS.IBusiness.IAddmission;
using CMS.BUSINESS.IBusiness.ILibrary;
using CMS.Filter;
using CMS.MODEL.Book;
using CMS.MODEL.Library;
using CMS.MODEL.Master;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Library
{
    public class LibraryController : Controller
    {
        #region Constructor And Parameters
        private readonly ILibraryBusiness _libraryBusiness;
        private readonly INewStudentAddmissionBusiness _newStudentAddmissionBusiness;
        public LibraryController(ILibraryBusiness libraryBusiness, INewStudentAddmissionBusiness newStudentAddmissionBusiness)
        {
            this._libraryBusiness = libraryBusiness;
            this._newStudentAddmissionBusiness = newStudentAddmissionBusiness;
        }
        #endregion

        #region Get
        #region DropDownList
        public ActionResult Dropdown()
        {
            List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
            List<DropDownListModel> result6 = this._newStudentAddmissionBusiness.SemesterList();
            ViewBag.StreamList = result2;
            ViewBag.SemesterList = result6;
            return View();
        }
        #endregion

        #region ListFilterBooks
        public ActionResult ListFilterBooks(string cVal)
        {
            try
            {
                List<BooksModel> result = this._libraryBusiness.ListFilteredBooks(cVal);
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

        #region GetAllBookListsAVailableInLibrary
        public ActionResult ListBooks()
        {
            try
            {
                List<BooksModel> result = this._libraryBusiness.ListBooks();
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

        #region GetListOfStudentsWhoTakeBookFromLibraryButNotReturned
        public ActionResult PendingBooks()
        {
            try
            {
                List<BookBusinessModel> result = this._libraryBusiness.listNotReturnedBooks();
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

        #region MasterPageOfLibrary
        [HttpGet]
        public ActionResult LibraryManagement()
        {
            return View();
        }
        #endregion

        #region ListNewlyAddedLibraryCards
        [HttpGet]
        public ActionResult ListCards()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region Error
        public ActionResult Errorshow()
        {
            return View();
        }
        #endregion

        /*#region GetListOfBooksWhichAreNot Returned By Name Or Id
        public ActionResult NotReturnedBook(string BookName , string AuthorName)
        {
            try
            {
                var result = this._libraryBusiness.ListNotReturnedBooks(BookName, AuthorName);
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
        #endregion*/

        #region GetListOf Available books IN Library By BookName, AuthorName
        public ActionResult ListAvailableBooksByNameAndAuthor(string BookName, string AuthorName)
        {
            try
            {
                var result = this._libraryBusiness.ListAvailableBooksByNameAndAuthor(BookName, AuthorName);
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

        
        #endregion

        #region Post
        #region CreateNewLibraryCard
        /// <summary>
        /// This Controller Is Used For Create A New Library Card For Newly Addmission Students.Clone Or Resamble is avoided (Duplicate Card cannot be Created).
        /// </summary>
        /// <returns></returns>
        /*[HttpGet]
        public ActionResult NewLibraryCard()
        {
            return View();
        }*/
        [HttpPost]
        [UserAuthorizeAttribute(RoleUser ="Admin,Vice-Admin")]
        public ActionResult NewLibraryCard(LibrarycardEditModel librarycard)
        {
            try
            {
                librarycard.CreatedBy = Session["UserGuid"].ToString();
                bool result = this._libraryBusiness.CreateNewCard(librarycard);
                if(result == true)
                {
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

        #region BookWithdrawlControl
        /// <summary>
        /// /This Api Used When a Student take a Book From College Library then this API will call for entry Taken Book data and This -
        /// Particular student Records in database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TakeBook()
        {
            List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
            List<DropDownListModel> result6 = this._newStudentAddmissionBusiness.SemesterList();
            ViewBag.StreamList = result2;
            ViewBag.SemesterList = result6;
            return View();
        }
        [HttpPost]
        public ActionResult TakeBook(LibraryRecordsEditModel libraryRecordsEdit)
        {
            try
            {
                if(this._libraryBusiness.CheckUserBookTaken(libraryRecordsEdit.LibraryCardSerialNumber))
                {
                   if(this._libraryBusiness.CheckUserPassedOut(libraryRecordsEdit.LibraryCardSerialNumber))
                    {
                        if(this._libraryBusiness.IsBookAvailableInLibrary(libraryRecordsEdit.BookSerialNumber))
                        {
                            bool result = this._libraryBusiness.BookWithDrawl(libraryRecordsEdit);
                            if (result == true)
                            {

                                List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
                                List<DropDownListModel> result6 = this._newStudentAddmissionBusiness.SemesterList();
                                ViewBag.StreamList = result2;
                                ViewBag.SemesterList = result6;
                                ViewBag.Success = true;
                                return View();//List View of Taken Books
                            }
                            else
                            {
                                return View();
                            }
                        }
                        else
                        {
                            List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
                            List<DropDownListModel> result6 = this._newStudentAddmissionBusiness.SemesterList();
                            ViewBag.StreamList = result2;
                            ViewBag.SemesterList = result6;
                            ViewBag.OutOfStock = true;
                            return View();
                        }
                    }
                    else
                    {

                        List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
                        List<DropDownListModel> result6 = this._newStudentAddmissionBusiness.SemesterList();
                        ViewBag.StreamList = result2;
                        ViewBag.SemesterList = result6;
                        ViewBag.UserPassedOut = true;
                        return View();
                    }
                }
                else
                {
                    List<DropDownListModel> result2 = this._newStudentAddmissionBusiness.StreamList();
                    List<DropDownListModel> result6 = this._newStudentAddmissionBusiness.SemesterList();
                    ViewBag.StreamList = result2;
                    ViewBag.SemesterList = result6;
                    ViewBag.AlreadyBookTaken = true;
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

        #region BookDeposit
        /// <summary>
        /// This Controller Is Used For Return Book In Library And Eligible Students For take new Book
        ///This Controller take BookSerialNumber and LibraryCardId as a Input Parameter and Automatically Complete further Actions.
        ///This Controller IS also Calculaet Fine Amount For Late Book Returning.
        /// </summary>
        /// <returns></returns>
        /*[HttpGet]
        public ActionResult BookDeposit()
        {
            return View();
        }*/
        [HttpPost]
        public ActionResult BookDeposit(LibraryRecordsEditModel libraryRecords)//querry not working
        {
            try
            {
                bool Result = this._libraryBusiness.DeposiTakenBoooks(libraryRecords);
                if(Result == true)
                {
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

        #region Request For Book
        public ActionResult RequestBook(RequestBookModel requestBook)
        {
            try
            {
                var result = this._libraryBusiness.RequestBook(requestBook);
                return View();
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region SearchBook
        [HttpGet]
        public ActionResult SearchBook()
        {
            return View();
        }
        #endregion

        #endregion
    }
}