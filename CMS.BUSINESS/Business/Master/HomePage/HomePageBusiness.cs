using CMS.BUSINESS.IBusiness.IMaster.IHomePage;
using CMS.MODEL.Event;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.IMaster.IHomePage;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.Master.HomePage
{
    public class HomePageBusiness : IHomePageBusiness
    {
        #region Properties And Constructor
        private readonly IHomePageRepository _homePageRepository;
        public HomePageBusiness(IHomePageRepository homePageRepository)
        {
            this._homePageRepository = homePageRepository;
        }
        #endregion

        #region Get
        #region Dropdown
        #region ImageTypeDropDown
        public List<ImageTypeEditModel> ImageTypes()
        {
            try
            {
                return this._homePageRepository.ImageTypes();
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
                return this._homePageRepository.Events();
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
                return this._homePageRepository.Images();
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
            return this._homePageRepository.Newsletter(Email);
        }
        #endregion
        #endregion
    }
}
