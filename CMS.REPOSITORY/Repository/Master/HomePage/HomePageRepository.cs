using ApplicationTools.Configuration;
using CMS.MODEL.Event;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.IMaster.IHomePage;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.Repository.Master.HomePage
{
    public class HomePageRepository : IHomePageRepository
    {
        #region Get
        #region Dropdown
        #region ImageTypeDropDown
        public List<ImageTypeEditModel> ImageTypes()
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ToString();
                string query = QueryConfig.BookQuerySettings["ImageTypeDropDownList"].ToString();
                List<ImageTypeEditModel> Images = new List<ImageTypeEditModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            Images.Add(new ImageTypeEditModel
                            {
                                TypeId = rdr[""].ToString(),
                                ImageType = rdr[""].ToString()
                            });
                        }
                        if(Images != null)
                        {
                            return Images;
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
        #endregion

        #region GetEventsFewDetails(Cards)
        public List<EventCardModel> Events()
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings[""].ToString();
                List<EventCardModel> Events = new List<EventCardModel>();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            Events.Add(new EventCardModel
                            {
                                EventName = rdr["EventName"].ToString(),
                                EventGuid = rdr["EventGuid"].ToString(),
                                EventShortDetails = rdr["EventShortDetails"].ToString(),
                                EventDate = DateTimeOffset.Parse(rdr["EventDate"].ToString()),
                                ImagePath = rdr["ImagePath"].ToString()
                            });
                        }
                        if(Events != null)
                        {
                            return Events;
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

        #region ImagesForImageSlider
        public List<EventCardModel> Images()
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string query = QueryConfig.BookQuerySettings[""].ToString();
                List<EventCardModel> IMages = new List<EventCardModel>();
                using(SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using(SqlCommand cmd = new SqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
                        {
                            IMages.Add(new EventCardModel { 
                                ImagePath = rdr["ImagePath"].ToString()
                            });
                        }
                        if(IMages != null)
                        {
                            return IMages;
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

        #region News

        #endregion
        #endregion

        #region Post
        #region Newsletter
        public bool Newsletter(string Email)
        {
            try
            {
                string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
                string CS = ConfigurationManager.ConnectionStrings[HostName].ConnectionString;
                string Query = QueryConfig.BookQuerySettings["AddNewNewsLetter"].ToString();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@EmailId", Email);
                        cmd.Parameters.AddWithValue("@IsSubscribed", true);
                        if(cmd.ExecuteNonQuery() > 0)
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
    }
}
