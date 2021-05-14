using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.IBusiness.IMaster.IExam
{
    public interface IExamBusiness
    {
        #region Get
        #region ExamPaper
        #region GetExamPaperByExamAnd Year Id
        List<ExamPaperModel> GetExamPaper(DropDownListModel data);
        #endregion
        #endregion
        #endregion

        #region Post
        #region Exampaper
        #region UploadPaper
        bool UploadPreviousYearQuestionpaper(ExamPaperModel Paper);
        #endregion
        #endregion
        #endregion
    }
}
