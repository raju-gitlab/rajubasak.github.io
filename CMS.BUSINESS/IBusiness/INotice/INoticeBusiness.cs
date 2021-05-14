using CMS.MODEL.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.IBusiness.INotice
{
    public interface INoticeBusiness
    {
        #region Get
        #region ViewNotices
        List<NoticeEditModel> GetNotices();
        #endregion

        #region View Full Notice
        string ViewNotice(string NgId); 
        #endregion
        #endregion

        #region Post
        #region Create New Notice
        bool CreateNewNotice(NoticeEditModel Createnotice);
        #endregion

        #region Create New Visual Notice
        bool CreateNewVisualNotice(VisualNoticeEditModel Createnotice);
        #endregion
        #endregion

    }
}
