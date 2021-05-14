using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.IRepository.IMaster.IContactUs
{
    public interface IContactUsRepository
    {
        #region Post
        #region AddNewReport
        bool CreateReport(ContactUsModel contactUs);
        #endregion
        #endregion
    }
}
