using CMS.MODEL.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.IBusiness.ILibrary
{
    public interface IBooksControlBusiness
    {
        #region Post
        #region AddNewBooks
        bool AddNewBooks(BooksModel books);
        #endregion

        #region AddNewParentBook
       
        #endregion
        #endregion
    }
}
