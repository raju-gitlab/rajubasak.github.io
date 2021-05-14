using CMS.MODEL.Master;
using CMS.MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.REPOSITORY.IRepository.IMaster
{
    public interface IAccountRepository
    {
        #region Get
        #region CheckUserCredentials
        bool UserCredentials(string EmailId);
        #endregion

        #region CheckUserExistance
        bool UserExistance(string EmailId);
        #endregion

        #region CheckStudentCredentials
        bool CheckStudentCredentials(string Email);
        #endregion

        #region CheckUserACcountIsActiveOrNot
        bool CheckActivation(string EmailId);
        #endregion

        #region CheckStudentAccountIsActiveOrNot
        bool CheckStudentAccountActivation(string UserRegId);
        #endregion

        #region CheckStudentExistance
        bool CheckStudentExistance(string RegId);
        #endregion

        #region UserLogin
        StudentModel UserLogin(ItemCode credentials);
        #endregion

        #region Student Login
        StudentModel StudentLogin(ItemCode data);
        #endregion

        #region SetVerificationCodeAgain
        #region SetStudentVerificationCode
        bool SetStudentVerificationCode(string RegId);
        #endregion

        #region SetUserVerificationCodeAgain
        bool SetUserVerificationCodeAgain(ItemCode StdId);
        #endregion
        #endregion

        #region GetVerificationCode
        string GetVerificationCode(ItemCode code);
        #endregion

        #region Checks
        #region CheckTeacherExistence
        bool CheckTeacherExistence(string Email);
        #endregion
        #endregion
        #endregion

        #region Post
        #region User(Students Registration For WebSight)
        bool RegisterNewStudent(StudentModel RegNewStudent);
        #endregion

        #region CreateTeacherAccount
        bool CreateTeacherAccount(ItemCode code);
        #endregion

        #region CreateStudentAccount
        bool CreateStudentAccount(ItemCode code);
        #endregion

        #region Verifies
        #region VerifyStudentAccount
        bool VerifyStudentAccount(ItemCode data);
        #endregion
        #endregion
        /*#region ForgetPassword
        #region ResetUsingExistingPassword
        bool ResetUsingExistingPassword();
        #endregion
        #region PerformForgetPassword

        #endregion
        #endregion*/

        #endregion

        #region Put
        #region Forget Password
        bool ForgetPassword(string UserId);
        #endregion

        #region UpdatePassword
        bool UpdatePasword(BaseModel credentials);
        #endregion

        #endregion
    }
}
