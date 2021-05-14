using CMS.BUSINESS.IBusiness.INotice;
using CMS.MODEL.Notice;
using CMS.REPOSITORY.IRepository.INotice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.Notice
{
    public class NoticeBusiness : INoticeBusiness
    {
        #region Parameters And Constructor
        private readonly INoticeRepository _noticeRepository;
        public NoticeBusiness(INoticeRepository noticeRepository)
        {
            this._noticeRepository = noticeRepository;
        }
        #endregion

        #region Get
        #region ViewNotices
        public List<NoticeEditModel> GetNotices()
        {
            return this._noticeRepository.GetNotices();
        }
        #endregion

        #region View Full Notice
        public string ViewNotice(string NgId)
        {
            if(NgId != null || NgId != string.Empty)
            {
                return this._noticeRepository.ViewNotice(NgId);
            }
            else
            {
                return null;
            }
        }
        #endregion
        #endregion

        #region Post
        #region Create New Notice
        public bool CreateNewNotice(NoticeEditModel Createnotice)
        {
            if(Createnotice != null)
            {
                return this._noticeRepository.CreateNewNotice(Createnotice);
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Create New Vusial Notice
        public bool CreateNewVisualNotice(VisualNoticeEditModel Createnotice)
        {
            if (Createnotice != null)
            {
                return this._noticeRepository.CreateNewVisualNotice(Createnotice);
            }
            else
            {
                return false;
            }
        }
        #endregion
        #endregion
    }
}
