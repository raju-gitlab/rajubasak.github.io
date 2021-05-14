using CMS.MODEL.Addmission;
using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.IRepository.IAddmission
{
    public interface IStudentFessRepository
    {
        #region Get
        #region GetStudentDetailsById
        List<StudentFessDetails> GetStudentDetails(StudentFessEditModel studentFess);
        #endregion

        #region GetStudentById
        Tuple<List<StudentFessDetails>, List<StudentFessEditModel>> GetStudentFeesDetails(string StudentGuid);
        #endregion
        #endregion

        #region Put
        #region UpdateFess
        bool UpdateFess(StudentFessEditModel studentFess);
        #endregion

        #region UpdateFine
        List<StudentFineModel> UpdateFine(StudentFineModel studentFess);
        #endregion

        #region UpdateFine
        bool DepositFine(StudentFineModel finedata);
        #endregion
        #endregion
    }
}
