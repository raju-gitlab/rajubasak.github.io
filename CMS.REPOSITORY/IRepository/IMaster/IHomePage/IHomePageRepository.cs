using CMS.MODEL.Event;
using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.IRepository.IMaster.IHomePage
{
    public interface IHomePageRepository
    {
        #region Get
        #region Dropdown
        #region ImageTypeDropDown
        List<ImageTypeEditModel> ImageTypes();
        #endregion
        #endregion

        #region GetEventsFewDetails(Cards)
        List<EventCardModel> Events();
        #endregion

        #region ImagesForImageSlider
        List<EventCardModel> Images();
        #endregion

        #region News

        #endregion
        #endregion

        #region Post
        #region Newsletter
        bool Newsletter(string Email);
        #endregion
        #endregion
    }
}
