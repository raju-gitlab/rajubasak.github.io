using CMS.MODEL.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.IRepository.INotice
{
    public interface INoticeRepository
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
        #region Create New Text Notice
        bool CreateNewNotice(NoticeEditModel Createnotice);
        #endregion

        #region Create New Visual Notice
        bool CreateNewVisualNotice(VisualNoticeEditModel Createnotice);
        #endregion
        #endregion
    }
}
