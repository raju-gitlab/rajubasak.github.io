using CMS.MODEL.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.IRepository.ILibrary
{
    public interface IBooksControlRepository
    {
        #region AddNewBooks
        bool AddNewBooks(BooksModel books);
        #endregion
    }
}
