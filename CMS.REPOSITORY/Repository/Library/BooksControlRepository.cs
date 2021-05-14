using ApplicationTools.Configuration;
using CMS.MODEL.Book;
using CMS.REPOSITORY.IRepository.ILibrary;
using CMS.UTILITIES.FolderCeration;
using CMS.UTILITIES.ImageUpload;
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
    public class BooksControlRepository : IBooksControlRepository
    {
        #region Post
        #region AddNewBooks
        public bool AddNewBooks(BooksModel books)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ToString();
                string query = QueryConfig.BookQuerySettings["AddNewBooksInlibrary"].ToString();
                string query_ = "declare @BId int = (select Id from Books where BookGuid = @BGuid)" +
                " insert into BooksLists(BookId, BooksSerialNumber, CreatedBy, CreatedOn, BookGuid)" +
                " Values" +
                " (@BId, @BooksSerialNumber, @CreatedBy, @CreatedOn, @BookGuid)";//QueryConfig.BookQuerySettings["AddCopiesOfParentsBooksInLibrary"].ToString();
                string Path = ConfigurationManager.AppSettings["UploadBookImagePath"].ToString();
                string FolderPath = CreateFolder.UploadEventImagePath(books.BookName,Path);
                string ImagePath = UploadImage.Upload(books.FileUplaod, FolderPath);
                string BookGuid = Guid.NewGuid().ToString().ToUpper();
                string BooksGuid = Guid.NewGuid().ToString().ToUpper();
                SqlCommand cmd = new SqlCommand();
                SqlConnection con;
                int j = 0; 
                using (con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction("First");
                    try
                    {
                        using (cmd = new SqlCommand(query, con, transaction))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new SqlParameter("@CourseGuid", books.Stream));
                            cmd.Parameters.AddWithValue("@BookName", books.BookName);
                            cmd.Parameters.AddWithValue("@AuthorName", books.BookAuthorName);
                            cmd.Parameters.AddWithValue("@BookImagePath", ImagePath);
                            cmd.Parameters.AddWithValue("@TotalBooksQuantity", books.TotalBooksQuantity);
                            cmd.Parameters.AddWithValue("@CurrentQuantity", books.TotalBooksQuantity);
                            cmd.Parameters.AddWithValue("@CreatedBy", books.CreatedBy);
                            cmd.Parameters.AddWithValue("@CreatedOn", DateTimeOffset.UtcNow);
                            cmd.Parameters.AddWithValue("@BookGuid", BookGuid);
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                foreach (var i in books.Bookserialnumber)
                                {
                                    using (cmd = new SqlCommand(query_, con, transaction))
                                    {
                                        cmd.CommandType = CommandType.Text;

                                        cmd.Parameters.Add(new SqlParameter("@BGuid", BookGuid));
                                        cmd.Parameters.AddWithValue("@BooksSerialnumber", Convert.ToInt32(i));
                                        cmd.Parameters.AddWithValue("@CreatedBy", books.CreatedBy);
                                        cmd.Parameters.AddWithValue("@CreatedOn", DateTimeOffset.UtcNow);
                                        cmd.Parameters.AddWithValue("@BookGuid", Guid.NewGuid().ToString());
                                        j = cmd.ExecuteNonQuery();
                                    } 
                                }
                                if (j > 0)
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
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
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
        #endregion
    }
}
