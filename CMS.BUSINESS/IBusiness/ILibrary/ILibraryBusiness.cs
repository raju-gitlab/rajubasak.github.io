using CMS.MODEL.Book;
using CMS.MODEL.Library;
using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.IBusiness.ILibrary
{
    public interface ILibraryBusiness
    {
        #region Get
        #region CheckStudenyISAlreadyPAssedOutOrNot
        bool CheckUserPassedOut(string LibraryCardserialId);
        #endregion

        #region CheckThatStudentIsAlreadyTakeBookOrNot
        bool CheckUserBookTaken(string LibraryCardSerialId);
        #endregion

        #region CheckStudentLibraryCardAlreadyExistsOrNot
        bool CheckExistance(string StudentRegId);
        #endregion

        #region GetAllBookListsAVailableInLibrary
        List<BooksModel> ListBooks();
        #endregion

        #region GetBooksListsAvailableinLibraryBySearchFilter
        List<BooksModel> ListFilteredBooks(string code);
        #endregion

        #region GetListOfStudentsWhoTakeBookFromLibraryButNotReturned
        List<BookBusinessModel> listNotReturnedBooks();
        #endregion

        /*#region GetListOfBooksWhichAreNot Returned By Name Or Id
        List<ListOfNotReuturnedBookModel> ListNotReturnedBooks(string BookName , string AuthorName);
        #endregion*/

        #region GetListBooksAvailableInLibraryByName OR Id
        List<ListOfBooksModel> ListAvailableBooksByNameAndAuthor(string BookName, string AuthorName);
        #endregion

        #region GetListOfRequestBooks
        List<RequestBookModel> ListRequestBooks();
        #endregion

        #region CheckLibraryBookData
        bool IsBookAvailableInLibrary(int BookId);
        #endregion

        #endregion

        #region Post
        #region CreateNewLibraryCard
        bool CreateNewCard(LibrarycardEditModel librarycard);
        #endregion

        #region BookWithdrawlControl
        bool BookWithDrawl(LibraryRecordsEditModel libraryRecordsEdit);
        #endregion

        #region Request For Book
        bool RequestBook(RequestBookModel requestBook);
        #endregion
        #endregion

        #region Update
        #region DepositTakenBooks
        bool DeposiTakenBoooks(LibraryRecordsEditModel libraryRecords);
        #endregion
        #endregion
    }
}
