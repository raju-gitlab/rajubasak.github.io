using CMS.BUSINESS.IBusiness.IMaster.IContactUs;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.IMaster.IContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.Master.ContactUs
{
    public class ContactUsBusiness : IContactUsBusiness
    {
        #region Parameter and Constructor
        private readonly IContactUsRepository _iContactUsRepository;
        public ContactUsBusiness(IContactUsRepository ContactUsRepository)
        {
            this._iContactUsRepository = ContactUsRepository;
        }
        #endregion

        #region Post
        #region AddNewReport
        public bool CreateReport(ContactUsModel contactUs)
        {
            return this._iContactUsRepository.CreateReport(contactUs);
        }
        #endregion
        #endregion
    }
}
