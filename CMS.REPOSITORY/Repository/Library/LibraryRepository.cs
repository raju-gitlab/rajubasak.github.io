using ApplicationTools.Configuration;
using ApplicationTools.EmailSender;
using CMS.MODEL.Book;
using CMS.MODEL.Library;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.ILibrary;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.Repository.Library
{
    public class LibraryRepository : ILibraryRepository
    {
        #region Parameters
        public int Item { get; set; }
        #endregion

        #region Get
        #region CheckStudenyISAlreadyPAssedOutOrNot
        public bool CheckUserPassedOut(string LibraryCardserialId)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["CheckUserPassedout"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@LibraryCardserialId", LibraryCardserialId));
                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region CheckThatStudentIsAlreadyTakeBookOrNot
        public bool CheckUserBookTaken(string LibraryCardSerialId)
        {
            try
            {
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["CheckUserAlreadyTakenBookOrNot"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@LibraryCardSerialNumber", LibraryCardSerialId));
                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region CheckStudentLibraryCardAlreadyExistsOrNot
        public bool CheckExistance(string StudentRegId)
        {
            try
            {
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["CheckLibraryCardExistance"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@StudentId", StudentRegId));
                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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
        public List<BooksModel> ListBooks()
        {
            try
            {
                List<BooksModel> books = new List<BooksModel>();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetAllTypesOfBook"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            books.Add(new BooksModel
                            {
                                BookName = rdr["BookName"].ToString(),
                                BookAuthorName = rdr["AuthorName"].ToString(),
                                BookImagePath = !string.IsNullOrEmpty(Convert.ToString(rdr["BookImagePath"])) ? rdr["BookImagePath"].ToString().Substring(1) : "null",
                                TotalBooksQuantity = Convert.ToInt32(rdr["TotalBooksQuantity"]),
                                CurrentQuantity = Convert.ToInt32(rdr["CurrentQuantity"]),
                                BookGuid = rdr["BookGuid"].ToString(),
                                Course = rdr["CourseName"].ToString()
                            });
                        }
                        if (books != null)
                        {
                            return books;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region GetBooksListsAvailableinLibraryBySearchFilter
        public List<BooksModel> ListFilteredBooks(string code)
        {
            try
            {
                List<BooksModel> books = new List<BooksModel>();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetBooksByFilter"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Code", code));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            books.Add(new BooksModel
                            {
                                BookName = rdr["BookName"].ToString(),
                                BookAuthorName = rdr["AuthorName"].ToString(),
                                BookImagePath = string.IsNullOrEmpty(Convert.ToString(rdr["BookImagePath"])) ? rdr["BookImagePath"].ToString() : string.Empty,
                                TotalBooksQuantity = Convert.ToInt32(rdr["TotalBooksQuantity"]),
                                CurrentQuantity = Convert.ToInt32(rdr["CurrentQuantity"]),
                                BookGuid = rdr["BookGuid"].ToString(),
                                BooksSerialnumber = rdr["BooksSerialNumber"].ToString()
                            });
                        }
                        if (books != null)
                        {
                            return books;
                        }
                        else
                        {
                            return null;
                        }
                    }
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
        public List<BookBusinessModel> listNotReturnedBooks()
        {
            try
            {
                List<BookBusinessModel> NotReturnedBooks = new List<BookBusinessModel>();
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["GetNotReturnedBookDetails"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            NotReturnedBooks.Add(new BookBusinessModel
                            {
                                StudentName = Convert.ToString(rdr["FirstName"].ToString() + rdr["LastName"].ToString()),
                                BookName = rdr["BookName"].ToString(),
                                AuthorName = rdr["AuthorName"].ToString(),
                                BookSerialNumber = Convert.ToInt32(rdr["BooksSerialNumber"]),
                                BookGuid = rdr["BookGuid"].ToString(),
                                DepositOn = DateTimeOffset.Parse(rdr["BookDepositDate"].ToString())
                            });
                        }
                        if (NotReturnedBooks != null)
                        {
                            return NotReturnedBooks;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region Comment Action
        /*#region GetListOfBooksWhichAreNot Returned By Name Or Id
        public List<ListOfNotReuturnedBookModel> ListNotReturnedBooks(string BookName , string AuthorName)
        {
            try
            {
                List<ListOfNotReuturnedBookModel> ListBook = new List<ListOfNotReuturnedBookModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["GetNotRetunrndListOfBooksByBookNameAndAuthorName"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(Query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@BookName", BookName));
                        cmd.Parameters.Add(new SqlParameter("@AuthorName", AuthorName));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            ListBook.Add(new ListOfNotReuturnedBookModel
                            {
                                BookName = !string.IsNullOrEmpty(Convert.ToString(rdr["BookName"])) ? rdr["BookName"].ToString(): null,
                                BookGuid = !string.IsNullOrEmpty(Convert.ToString(rdr["BookGuid"])) ? rdr["BookGuid"].ToString(): null,
                                BookAuthorName = !string.IsNullOrEmpty(Convert.ToString(rdr["AuthorName"])) ? rdr["AuthorName"].ToString(): null,
                                BookSerialNumber = !string.IsNullOrEmpty(Convert.ToString(rdr["BooksSerialNumber"])) ? Convert.ToInt32(rdr["BooksSerialNumber"]) : 0
                            });
                        }
                        if(ListBook != null)
                        {
                            return ListBook;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion*/
        #endregion

        #region GetListBooksAvailableInLibraryByName OR Id
        public List<ListOfBooksModel> ListAvailableBooksByNameAndAuthor(string BookName, string AuthorName)
        {
            try
            {
                List<ListOfBooksModel> ListBooks = new List<ListOfBooksModel>();
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["GetListOfBooksByBookNameAndAuthorName"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@BookName", BookName));
                        cmd.Parameters.Add(new SqlParameter("@BookAuthorName", AuthorName));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            ListBooks.Add(new ListOfBooksModel
                            {
                                BookName = !string.IsNullOrEmpty(Convert.ToString(rdr["BookName"])) ? rdr["BookName"].ToString() : null,
                                BookGuid = !string.IsNullOrEmpty(Convert.ToString(rdr["BookGuid"])) ? rdr["BookGuid"].ToString() : null,
                                BookAuthorName = !string.IsNullOrEmpty(Convert.ToString(rdr["AuthorName"])) ? rdr["AuthorName"].ToString() : null,
                                BookSerialNumber = !string.IsNullOrEmpty(Convert.ToString(rdr["BooksSerialNumber"])) ? Convert.ToInt32(rdr["BooksSerialNumber"]) : 0
                            });
                        }
                        if (ListBooks != null)
                        {
                            return ListBooks;
                        }
                        else
                        {
                            return null;
                        }
                    }
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
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["RequestBooksLists"].ToString();
                List<RequestBookModel> requestBooks = new List<RequestBookModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            requestBooks.Add(new RequestBookModel
                            {
                                StudentName = rdr["FirstName"].ToString() + rdr["LastName"].ToString(),
                                LibraryCardSerialNumber = rdr["LibraryCardSerialNumber"].ToString(),
                                BookName = !string.IsNullOrEmpty(rdr["BookName"].ToString()) ? rdr["BookName"].ToString() : string.Empty,
                                BookGuid = !string.IsNullOrEmpty(rdr["BookGuid"].ToString()) ? rdr["BookGuid"].ToString() : string.Empty,
                                BookSerialNumber = !string.IsNullOrEmpty(rdr["BooksSerialNumber"].ToString()) ? rdr["BooksSerialNumber"].ToString() : string.Empty,
                                StudentGuid = !string.IsNullOrEmpty(rdr["StudentGuid"].ToString()) ? rdr["StudentGuid"].ToString() : string.Empty,
                                BookRequestDate = !string.IsNullOrEmpty(rdr["BookRequestDate"].ToString()) ? DateTimeOffset.Parse(rdr["BookRequestDate"].ToString()) : DateTimeOffset.Now,
                                BookTakenDate = !string.IsNullOrEmpty(rdr["BookTakenDate"].ToString()) ? DateTimeOffset.Parse(rdr["BookTakenDate"].ToString()) : DateTimeOffset.Now,

                            });
                        }
                        if (requestBooks != null)
                        {
                            return requestBooks;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region CheckLibraryBookData
        public bool IsBookAvailableInLibrary(int BookId)
        {
            try
            {
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["CheckBookIsAvailableOrNot"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@BookId", BookId));
                        if(Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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
        public bool CreateNewCard(LibrarycardEditModel librarycard)
        {
            try
            {
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["CreateNewLibraryCard"].ToString();
                string CardNo = Guid.NewGuid().ToString().Substring(0, 8).Replace("-", string.Empty).ToUpper();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@StudentRegId", librarycard.StudentRegId));//studentGuidOnRegistrationTable
                        cmd.Parameters.Add(new SqlParameter("@CourseGuid", librarycard.Stream));
                        cmd.Parameters.AddWithValue("@LibraryCardSerialNumber", CardNo);
                        cmd.Parameters.AddWithValue("@CreatedBy", librarycard.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", DateTimeOffset.UtcNow);
                        cmd.Parameters.AddWithValue("@IsDeleted", false);
                        cmd.Parameters.AddWithValue("@IsBookTaken", false);
                        cmd.Parameters.AddWithValue("@FineAmount", 0);
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region LibraryBookWithdrawlControl
        public bool BookWithDrawl(LibraryRecordsEditModel libraryRecordsEdit)
        {
            try
            {
                string Hostname = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[Hostname].ConnectionString;
                string query = QueryConfig.BookQuerySettings["TakeBooksFromLibrary"].ToString();
                string _query = QueryConfig.BookQuerySettings["UpdateTakenBooksCurrentQuantity"].ToString();
                string _query_ = QueryConfig.BookQuerySettings["UpdateAttribute1"].ToString();
                SqlCommand cmd;
                SqlConnection con;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction("First");
                    try
                    {
                        if (CheckUserBookTaken(libraryRecordsEdit.LibraryCardSerialNumber))
                        {
                            using (cmd = new SqlCommand(query, con, transaction))
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.Add(new SqlParameter("@CourseGuid", libraryRecordsEdit.StreamGuid));//CourseGuid
                                cmd.Parameters.Add(new SqlParameter("@SemesterGuid", libraryRecordsEdit.SemesterGuid));//SemesterGuid
                                /*cmd.Parameters.Add(new SqlParameter("@StudentRegId", libraryRecordsEdit.StudentRegId));*///It Is Not a dropdown element
                                cmd.Parameters.Add(new SqlParameter("@BooksSerialNumber", libraryRecordsEdit.BookSerialNumber));
                                cmd.Parameters.Add(new SqlParameter("@LibraryCardSerialNumber", libraryRecordsEdit.LibraryCardSerialNumber));//It Is Not a dropdown element
                                cmd.Parameters.AddWithValue("@BookTakenOn", DateTimeOffset.UtcNow);
                                cmd.Parameters.AddWithValue("@FineAmount", 0);
                                cmd.Parameters.AddWithValue("@IsReturned", string.IsNullOrEmpty(Convert.ToString(libraryRecordsEdit.IsReturned)) ? libraryRecordsEdit.IsReturned : false);
                                if (cmd.ExecuteNonQuery() > 0)
                                {
                                    using (cmd = new SqlCommand(_query, con, transaction))
                                    {
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Parameters.Add(new SqlParameter("@BooksSerialNumber", libraryRecordsEdit.BookSerialNumber));
                                        if (cmd.ExecuteNonQuery() > 0)
                                        {
                                            using (cmd = new SqlCommand(_query_, con, transaction))
                                            {
                                                cmd.CommandType = CommandType.Text;
                                                cmd.Parameters.Add(new SqlParameter("@LibraryCardSerialNumber", libraryRecordsEdit.LibraryCardSerialNumber));
                                                if (cmd.ExecuteNonQuery() > 0)
                                                {
                                                    transaction.Commit();
                                                    return true;
                                                }
                                                else
                                                {
                                                    transaction.Rollback();
                                                    return false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            transaction.Rollback();
                                            return false;
                                        }
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        LogManager.ErrorEntry(ex);
                        throw;
                    }
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
        public bool RequestBook(RequestBookModel requestBook)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings[""].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("", requestBook.BookGuid));
                        cmd.Parameters.Add(new SqlParameter("", requestBook.LibraryCardSerialNumber));
                        cmd.Parameters.AddWithValue("@", requestBook.BookTakenDate);
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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

        #region Update
        #region DepositTakenBooks
        public bool DeposiTakenBoooks(LibraryRecordsEditModel libraryRecords)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ToString();
                string query_ = QueryConfig.BookQuerySettings["UpdateReturnAndDepositDeta"].ToString();
                string _query = QueryConfig.BookQuerySettings["ReadBookTakenDetails"].ToString();
                string _query_ = QueryConfig.BookQuerySettings["ResetBookTakenValue"].ToString();
                string query = QueryConfig.BookQuerySettings["UpdateFine"].ToString();
                int FineAmount = 5;
                DateTimeOffset DepositDate = DateTimeOffset.UtcNow;
                SqlConnection con;
                SqlCommand cmd;
                SqlDataReader rdr;
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction sqlTransaction = con.BeginTransaction("First");
                    try
                    {
                        using (cmd = new SqlCommand(_query, con, sqlTransaction))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new SqlParameter("@LibraryCardSerialNumber", libraryRecords.LibraryCardSerialNumber));
                            cmd.Parameters.Add(new SqlParameter("@BooksSerialNumber", libraryRecords.BookSerialNumber));
                            rdr = cmd.ExecuteReader();
                            if (rdr.Read())
                            {
                                libraryRecords.BookDepositDate = DateTimeOffset.Parse(rdr["BookDepositDate"].ToString());
                                libraryRecords.FineAmount = !string.IsNullOrEmpty(Convert.ToString(rdr["FineAmount"])) ? Convert.ToInt32(rdr["FineAmount"]) : 0;
                            }
                            rdr.Close();
                        }
                        if (libraryRecords.BookDepositDate != DateTimeOffset.Parse("1/1/0001 12:00:00 AM +00:00"))
                        {
                            using (cmd = new SqlCommand(query_, con, sqlTransaction))
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.Add(new SqlParameter("@BookSerialNumber", libraryRecords.BookSerialNumber));
                                cmd.Parameters.Add(new SqlParameter("@LibraryCardSerialNumber", libraryRecords.LibraryCardSerialNumber));
                                cmd.Parameters.AddWithValue("@IsReturned", true);
                                cmd.Parameters.AddWithValue("@BookDepositOn", DepositDate);
                            }
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                if (libraryRecords.BookDepositDate == DepositDate)
                                {
                                    using (cmd = new SqlCommand(_query_, con, sqlTransaction))
                                    {
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Parameters.Add(new SqlParameter("@LibraryCardSerialNumber", libraryRecords.LibraryCardSerialNumber));
                                        cmd.Parameters.AddWithValue("@IsBookTaken", false);
                                    }
                                    if (cmd.ExecuteNonQuery() > 0)
                                    {
                                        using (cmd = new SqlCommand(query, con, sqlTransaction))
                                        {
                                            cmd.Parameters.Add(new SqlParameter("@LibraryCardSerialNumber", libraryRecords.LibraryCardSerialNumber));
                                            cmd.Parameters.Add(new SqlParameter("@FineAmount", 0));
                                        }
                                        if (cmd.ExecuteNonQuery() > 0)
                                        {
                                            sqlTransaction.Commit();
                                            return true;
                                        }
                                        else
                                        {
                                            sqlTransaction.Rollback();
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        sqlTransaction.Rollback();
                                        return false;
                                    }
                                }
                                else if (libraryRecords.BookDepositDate > DepositDate)
                                {
                                    using (cmd = new SqlCommand(_query_, con, sqlTransaction))
                                    {
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Parameters.Add(new SqlParameter("@LibraryCardSerialNumber", libraryRecords.LibraryCardSerialNumber));
                                        cmd.Parameters.AddWithValue("@IsBookTaken", false);
                                    }
                                    if (cmd.ExecuteNonQuery() > 0)
                                    {
                                        using (cmd = new SqlCommand(query, con, sqlTransaction))
                                        {
                                            cmd.Parameters.Add(new SqlParameter("@LibraryCardSerialNumber", libraryRecords.LibraryCardSerialNumber));
                                            cmd.Parameters.AddWithValue("@FineAmount", 0);
                                        }
                                        if (cmd.ExecuteNonQuery() > 0)
                                        {
                                            sqlTransaction.Commit();
                                            return true;
                                        }
                                        else
                                        {
                                            sqlTransaction.Rollback();
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        sqlTransaction.Rollback();
                                        return false;
                                    }
                                }
                                else
                                {
                                    using (cmd = new SqlCommand(_query_, con, sqlTransaction))
                                    {
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Parameters.Add(new SqlParameter("@LibraryCardSerialNumber", libraryRecords.LibraryCardSerialNumber));
                                        cmd.Parameters.AddWithValue("@IsBookTaken", false);
                                    }
                                    if (cmd.ExecuteNonQuery() > 0)
                                    {
                                        using (cmd = new SqlCommand(query, con, sqlTransaction))
                                        {
                                            var datediff = ((DepositDate.DayOfYear) - (libraryRecords.BookDepositDate.DayOfYear));
                                            var FAmount = (datediff * FineAmount);
                                            cmd.Parameters.Add(new SqlParameter("@LibraryCardSerialNumber", libraryRecords.LibraryCardSerialNumber));
                                            cmd.Parameters.Add(new SqlParameter("@FineAmount", (FAmount + libraryRecords.FineAmount)));
                                        }
                                        if (cmd.ExecuteNonQuery() > 0)
                                        {
                                            sqlTransaction.Commit();
                                            return true;
                                        }
                                        else
                                        {
                                            sqlTransaction.Rollback();
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        sqlTransaction.Rollback();
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        sqlTransaction.Rollback();
                        LogManager.ErrorEntry(ex);
                        throw;
                    }
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
    }
}
