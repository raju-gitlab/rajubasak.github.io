using CMS.BUSINESS.IBusiness.ILibrary;
using CMS.MODEL.Book;
using CMS.MODEL.Library;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.ILibrary;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.Library
{
    public class LibraryBusiness : ILibraryBusiness
    {
        #region Parameters And Constructor
        private readonly ILibraryRepository _libraryRepository;
        public LibraryBusiness(ILibraryRepository libraryRepository)
        {
            this._libraryRepository = libraryRepository;
        }
        #endregion

        #region Get
        /*#region DropDownListData
        public List<ItemCode> AuthorNames()
        {
            return this._libraryRepository.AuthorNames();
        }
        public List<ItemCode> BookNames()
        {
            return this._libraryRepository.BookNames();
        }
        #endregion*/

        #region CheckStudenyISAlreadyPAssedOutOrNot
        public bool CheckUserPassedOut(string LibraryCardserialId)
        {
            if (LibraryCardserialId != null && LibraryCardserialId != string.Empty)
            {
                return this._libraryRepository.CheckUserPassedOut(LibraryCardserialId);
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region CheckThatStudentIsAlreadyTakeBookOrNot
        public bool CheckUserBookTaken(string LibraryCardSerialId)
        {
            return this._libraryRepository.CheckUserBookTaken(LibraryCardSerialId);
        }
        #endregion

        #region CheckStudentLibraryCardAlreadyExistsOrNot
        public bool CheckExistance(string StudentRegId)
        {
            if (StudentRegId != null && StudentRegId != string.Empty)
            {
                return this._libraryRepository.CheckExistance(StudentRegId);
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region GetAllBookListsAVailableInLibrary
        public List<BooksModel> ListBooks()
        {
            return this._libraryRepository.ListBooks();
        }
        #endregion

        #region GetBooksListsAvailableinLibraryBySearchFilter
        public List<BooksModel> ListFilteredBooks(string code)
        {
            return this._libraryRepository.ListFilteredBooks(code);
        }
        #endregion

        #region GetListOfStudentsWhoTakeBookFromLibraryButNotReturned
        public List<BookBusinessModel> listNotReturnedBooks()
        {
            try
            {
                return this._libraryRepository.listNotReturnedBooks();
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        /*#region GetListOfBooksWhichAreNot Returned By Name Or Id
        public List<ListOfNotReuturnedBookModel> ListNotReturnedBooks(string BookName , string AuthorName)
        {
            try
            {
                return this._libraryRepository.ListNotReturnedBooks(BookName , AuthorName);
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion*/

        #region GetListBooksAvailableInLibraryByName OR Id
        public List<ListOfBooksModel> ListAvailableBooksByNameAndAuthor(string BookName, string AuthorName)
        {
            try
            {
                if (BookName != null && AuthorName != null && BookName != string.Empty && AuthorName != string.Empty)
                {
                    return this._libraryRepository.ListAvailableBooksByNameAndAuthor(BookName, AuthorName);
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

        #region GetListOfRequestBooks
        public List<RequestBookModel> ListRequestBooks()
        {
            return this._libraryRepository.ListRequestBooks();
        }
        #endregion

        #region CheckLibraryBookData
        public bool IsBookAvailableInLibrary(int BookId)
        {
            return this._libraryRepository.IsBookAvailableInLibrary(BookId);
        }
        #endregion

        #endregion

        #region Post
        #region CreateNewLibraryCard
        public bool CreateNewCard(LibrarycardEditModel librarycard)
        {
            try
            {
                if (this._libraryRepository.CheckExistance(librarycard.StudentRegId))
                {
                    return this._libraryRepository.CreateNewCard(librarycard);
                }
                else
                {
                    return false;
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
        public bool BookWithDrawl(LibraryRecordsEditModel libraryRecordsEdit)
        {
            return this._libraryRepository.BookWithDrawl(libraryRecordsEdit);
        }
        #endregion

        #region Request For Book
        public bool RequestBook(RequestBookModel requestBook)
        {
            if (requestBook.BookSerialNumber != null || requestBook.BookSerialNumber != string.Empty && requestBook.LibraryCardSerialNumber != null || requestBook.LibraryCardSerialNumber != string.Empty)
            {
                return this._libraryRepository.RequestBook(requestBook);
            }
            else
            {
                return false;
            }
        }
        #endregion
        #endregion

        #region Update
        #region DepositTakenBooks
        public bool DeposiTakenBoooks(LibraryRecordsEditModel libraryRecords)
        {
            if (libraryRecords != null)
            {
                return this._libraryRepository.DeposiTakenBoooks(libraryRecords);
            }
            else
            {
                return false;
            }
        }
        #endregion
        #endregion
    }
}
