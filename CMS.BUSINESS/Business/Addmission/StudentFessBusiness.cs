using CMS.BUSINESS.IBusiness.IAddmission;
using CMS.MODEL.Addmission;
using CMS.MODEL.Master;
using CMS.REPOSITORY.IRepository.IAddmission;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.Addmission
{
    public class StudentFessBusiness : IStudentFessBusiness
    {
        #region Parameters And Constructors
        private readonly IStudentFessRepository _studentFessRepository;
        public StudentFessBusiness(IStudentFessRepository studentFessRepository)
        {
            this._studentFessRepository = studentFessRepository;
        }
        #endregion

        #region Get
        #region GetStudentDetailsById
        public List<StudentFessDetails> GetStudentDetails(StudentFessEditModel studentFess)
        {
            try
            {
                return this._studentFessRepository.GetStudentDetails(studentFess);
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw new Exception("");
            }
        }
        #endregion

        #region GetStudentById
        public Tuple<List<StudentFessDetails>, List<StudentFessEditModel>> GetStudentFeesDetails(string StudentGuid)
        {
            try
            {
                if(StudentGuid != null || StudentGuid != string.Empty)
                {
                    return this._studentFessRepository.GetStudentFeesDetails(StudentGuid);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion
        #endregion

        #region Put
        #region UpdateFess
        public bool UpdateFess(StudentFessEditModel studentFess)
        {
            return this._studentFessRepository.UpdateFess(studentFess);
        }
        #endregion

        #region UpdateFine
        public List<StudentFineModel> UpdateFine(StudentFineModel studentFess)
        {
            return this._studentFessRepository.UpdateFine(studentFess);
        }
        #endregion

        #region UpdateFine
        public bool DepositFine(StudentFineModel finedata)
        {
            return this._studentFessRepository.DepositFine(finedata);
        }
        #endregion 
        #endregion
    }
}
