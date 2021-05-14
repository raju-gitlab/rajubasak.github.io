using CMS.BUSINESS.IBusiness.IMaster;
using CMS.MODEL.Master;
using CMS.MODEL.User;
using CMS.REPOSITORY.IRepository.IMaster;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.BUSINESS.Business.Master
{
    public class AccountBusiness : IAccountBusiness
    {
        #region Constructor And Parameters
        private readonly IAccountRepository _accountRepository;

        public AccountBusiness(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }
        #endregion

        #region Get
        #region CheckUserCredentials
        public bool UserCredentials(string EmailId)
        {
            try
            {
                if (EmailId != null && EmailId != string.Empty)
                {
                    return this._accountRepository.UserCredentials(EmailId);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region CheckUserExistance
        public bool UserExistance(string EmailId)
        {
            return this._accountRepository.UserExistance(EmailId);
        }
        #endregion

        #region CheckStudentCredentials
        public bool CheckStudentCredentials(string Email)
        {
            return this._accountRepository.CheckStudentCredentials(Email);
        }
        #endregion

        #region CheckUserACcountIsActiveOrNot
        public bool CheckActivation(string EmailId)
        {
            if(EmailId != null)
            {
                return this._accountRepository.CheckActivation(EmailId);
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region CheckStudentAccountIsActiveOrNot
        public bool CheckStudentAccountActivation(string UserRegId)
        {
            return this._accountRepository.CheckStudentAccountActivation(UserRegId);
        }
        #endregion

        #region CheckStudentExistance
        public bool CheckStudentExistance(string RegId)
        {
            return this._accountRepository.CheckStudentExistance(RegId);
        }
        #endregion

        #region UserLogin
        public StudentModel UserLogin(ItemCode credentials)
        {
            try
            {
               return this._accountRepository.UserLogin(credentials);
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region Student Login
        public StudentModel StudentLogin(ItemCode data)
        {
            return this._accountRepository.StudentLogin(data);
        }
        #endregion

        #region Checks
        #region CheckTeacherExistence
        public bool CheckTeacherExistence(string Email)
        {
            return this._accountRepository.CheckTeacherExistence(Email);
        }
        #endregion
        #endregion

        #region SetVerificationCodeAgain
        #region SetStudentVerificationCode
        public bool SetStudentVerificationCode(string RegId)
        {
            return this._accountRepository.SetStudentVerificationCode(RegId);
        }
        #endregion

        #region SetUserVerificationCodeAgain
        public bool SetUserVerificationCodeAgain(ItemCode StdId)
        {
            return this._accountRepository.SetUserVerificationCodeAgain(StdId);
        }
        #endregion
        #endregion

        #region GetVerificationCode
        public string GetVerificationCode(ItemCode code)
        {
            return this._accountRepository.GetVerificationCode(code);
        }
        #endregion
        #endregion

        #region Post
        #region User(Students Registration For WebSight)
        public bool RegisterNewStudent(StudentModel RegNewStudent)
        {
            try
            {
                return this._accountRepository.RegisterNewStudent(RegNewStudent);
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region CreateTeacherAccount
        public bool CreateTeacherAccount(ItemCode code)
        {
            return this._accountRepository.CreateTeacherAccount(code);
        }
        #endregion

        #region CreateStudentAccount
        public bool CreateStudentAccount(ItemCode code)
        {
            return this._accountRepository.CreateStudentAccount(code);
        }
        #endregion

        #region Verifies
        #region VerifyStudentAccount
        public bool VerifyStudentAccount(ItemCode data)
        {
            return this._accountRepository.VerifyStudentAccount(data);
        }
        #endregion
        #endregion
        #endregion

        #region Put
        #region Update Password
        public bool ForgetPassword(string UserId)
        {
            if(UserId != null && UserId != string.Empty)
            {
                return this._accountRepository.ForgetPassword(UserId);
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region UpdatePassword
        public bool UpdatePasword(BaseModel credentials)
        {
            return this._accountRepository.UpdatePasword(credentials);
        }
        #endregion
        #endregion
    }
}
