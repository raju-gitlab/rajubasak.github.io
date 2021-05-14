using CMS.BUSINESS.IBusiness.ILibrary;
using CMS.MODEL.Book;
using CMS.REPOSITORY.IRepository.ILibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.Library
{
    public class BooksControlBusiness : IBooksControlBusiness
    {
        #region Constructor And Parameter
        private readonly IBooksControlRepository _booksControlRepository;
        public BooksControlBusiness(IBooksControlRepository booksControlRepository)
        {
            this._booksControlRepository = booksControlRepository;
        }
        #endregion

        #region Post
        #region AddNewBooks
        public bool AddNewBooks(BooksModel books)
        {
            return this._booksControlRepository.AddNewBooks(books);
        }
        #endregion
        #endregion
    }
}
