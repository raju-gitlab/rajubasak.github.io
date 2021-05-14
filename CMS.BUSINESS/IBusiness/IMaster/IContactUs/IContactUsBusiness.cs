using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.IBusiness.IMaster.IContactUs
{
    public interface IContactUsBusiness
    {
        #region Post
        #region AddNewReport
        bool CreateReport(ContactUsModel contactUs);
        #endregion
        #endregion
    }
}
