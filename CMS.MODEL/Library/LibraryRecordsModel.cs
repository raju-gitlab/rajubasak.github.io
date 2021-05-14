using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.MODEL.Library
{
    public class LibraryRecordsModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public int BookId { get; set; }
        public int LibraryCardId { get; set; }
    }
    public class LibraryRecordsEditModel : DropDownListModel
    {
        public DateTimeOffset BookTakenOn { get; set; }
        public DateTimeOffset BookDepositDate { get; set; }
        public DateTimeOffset BookDepositOn { get; set; }
        public int FineAmount { get; set; }
        public bool IsReturned { get; set; }
        public string LibraryCardSerialNumber { get; set; }
        public int BookSerialNumber { get; set; }
        public bool IsBookTaken { get; set; }
        public string StudentName { get; set; }
    }
    public class BookBusinessModel : DropDownListModel
    {
        public string StudentName { get; set; }
        public string AuthorName { get; set; }
        public int FineAmount { get; set; }
        public int BookSerialNumber { get; set; }
        public DateTimeOffset TakenOn { get; set; }
        public DateTimeOffset DepositOn { get; set; }
        public string CourseName { get; set; }
        public string LibraryCardSerialNumber { get; set; }
    }
    public class RequestBookModel : BaseModel
    {
        public string LibraryCardSerialNumber { get; set; }
        public string BookSerialNumber { get; set; }
        public string BookGuid { get; set; }
        public DateTimeOffset BookTakenDate { get; set; }
        public string BookName { get; set; }
        public DateTimeOffset BookRequestDate { get; set; }
        public string StudentGuid { get; set; }
        public string StudentName { get; set; }
    }
    public class ListOfNotReuturnedBookModel : BaseModel
    {
        public string BookName { get; set; }
        public string BookGuid { get; set; }
        public int BookSerialNumber { get; set; }
        public string BookDepositDate { get; set; }
        public string BookAuthorName { get; set; }
    }
    public class ListOfBooksModel : ListOfNotReuturnedBookModel
    { }
}
