using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.MODEL.Book
{
    public class BooksLists : DropDownListModel
    {
        public string BooksSerialnumber { get; set; }
        public int[] Bookserialnumber { get; set; }
        public string BooksGuid { get; set; }
    }
    public class BooksModel : BooksLists
    {
        public string BookAuthorName { get; set; }
        public string BookImagePath { get; set; }
        public HttpPostedFileBase FileUplaod { get; set; }
        public int TotalBooksQuantity { get; set; }
        public int CurrentQuantity { get; set; }
    }
}
