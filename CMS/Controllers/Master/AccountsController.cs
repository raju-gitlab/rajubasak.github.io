using ApplicationTools.PasswordHasher;
using CMS.BUSINESS.IBusiness.IMaster;
using CMS.Filter;
using CMS.MODEL.Master;
using CMS.MODEL.User;
using CMS.UTILITIES.AuthenticationAndAuthorization;
using CMS.UTILITIES.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.Master
{
    public class AccountsController : Controller
    {
        #region Parameters And Constructor
        private readonly IAccountBusiness _accountBusiness;
        public AccountsController(IAccountBusiness accountBusiness)
        {
            this._accountBusiness = accountBusiness;
        }
        #endregion

        #region Get
        #region CheckUserCredentials
        [NonAction]
        public ActionResult CheckUserCredentials(string EmailId)
        {
            try
            {
                bool result = this._accountBusiness.UserCredentials(EmailId);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region Login

        #region UserLogin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(ItemCode Credentials)
        {
            try
            {
                if(this._accountBusiness.UserExistance(Credentials.Value))
                {
                    if(this._accountBusiness.CheckActivation(Credentials.Value))
                    {
                        var result = this._accountBusiness.UserLogin(Credentials);
                        if (result != null)
                        {
                            if (PasswordHasher.PasswordHash(Credentials.Code, result.PasswordSalt) == result.Password)
                            {
                                Session["LogedInStatus"] = "true";
                                Session["StudentImagePath"] = result.StudentImagePath;
                                Session["UserName"] = result.StudentName;
                                Session["Password"] = result.PasswordSalt;
                                Session["Email"] = Credentials.Value;
                                Session["UserGuid"] = result.StudentGuid;
                                Session["UserRole"] = result.StudentRole;
                                var i = Session["UserName"].ToString();
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.CredentialsError = true;
                                return View();
                            }
                        }
                        else
                        {
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.AccountNotActive = true;
                        return View("Login", Credentials);
                    }
                }
                else
                {
                    ViewBag.UserNotFound = true;
                    return View();
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region Student Login
        [HttpPost]
        public ActionResult StudentLoginPortal(ItemCode data)
        {
            try
            {
                if(this._accountBusiness.CheckStudentExistance(data.Value))
                {
                    if (this._accountBusiness.CheckStudentAccountActivation(data.Value))
                    {
                        StudentModel result = this._accountBusiness.StudentLogin(data);
                        if (result != null)
                        {
                            Session["LogedInStatus"] = "true";
                            Session["StudentImagePath"] = result.StudentImagePath;
                            Session["UserName"] = result.StudentName;
                            Session["Email"] = result.Email;
                            Session["UserGuid"] = result.StudentGuid;
                            Session["UserRole"] = result.RoleName;
                            Session["StudentRegId"] = data.Value;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.WrongCredentials = true;
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.VarifyCancel = true;
                        return View();
                    }
                }
                else
                {
                    ViewBag.StudentNotFound = true;
                    return View("StudentsLogin", data);
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

        #region LogOut
        public ActionResult LogOut()
        {
            try
            {
                Session.Clear();   //clearing the session
                Session.Abandon();
                Request.Cookies.Clear();
                Response.Cookies.Clear();
                foreach (var cookie in Request.Cookies.AllKeys)  // removing all cookies
                {
                    Request.Cookies.Remove(cookie);
                }
                foreach (var cookie in Response.Cookies.AllKeys)
                {
                    Response.Cookies.Remove(cookie);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #endregion

        #region Post
        #region AddNewStudentRegistration for websight
        [HttpGet]
        public ActionResult RegisterNewStudent()//Need To Bind Dropdown For Course Entry
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterNewUser(StudentModel RegNewStudent)
        {
            try
            {
                if(this._accountBusiness.UserCredentials(RegNewStudent.Email))
                {
                    bool result = this._accountBusiness.RegisterNewStudent(RegNewStudent);
                    if (result == true)
                    {
                        ViewBag.AccountCreated = true;
                        return View("Login");
                    }
                    else
                    {
                        ViewBag.AccountCreated = false;
                        return View("Login");
                    }
                }
                else
                {
                    ViewBag.UserAlreadyExists = true;
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region Students Login
        /// <summary>
        /// Create New Student Login Credentials
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StudentsLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentsLogin(ItemCode data)
        {
            try
            {
                if (this._accountBusiness.CheckStudentCredentials(data.Value))
                {
                    if (this._accountBusiness.CreateStudentAccount(data))
                    {
                        ViewBag.IsSuccess = true;
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region ForgetPassword
        #region ResetUsingExistingPassword

        #endregion

        #region PerformForgetPassword
        #region Forget
        //[HttpGet]
        //public ActionResult ForgetPassword()
        //{
        //    ViewBag.ModalShow = false;
        //    return View();
        //}
        public ActionResult ResetPassword(string UserId)
        {
            try
            {
                bool result = this._accountBusiness.ForgetPassword(UserId);
                if (result == true)
                {
                    return View();
                }
                else
                {
                    ViewBag.ModalShow = true;
                    return View("ForgetPassword");
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region Check Verification Code
        [HttpGet]
        public ActionResult Verify(ItemCode scode)
        {
            try
            {
                string result = this._accountBusiness.GetVerificationCode(scode);
                if (result == scode.Code)
                {
                    ViewBag.CheckSuccess = true;
                    return View("Login");
                }
                else
                {
                    ViewBag.CheckSuccess = true;
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                throw;
            }
        }
        #endregion

        #region Update Password
        [HttpPost]
        public ActionResult UpdatePassword(BaseModel credentials)
        {
            try
            {
                bool result = this._accountBusiness.UpdatePasword(credentials);
                if(result == true)
                {
                    return RedirectToAction("Login", "Accounts");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                LogManager.ErrorEntry(ex);
                return View();
            }
        }
        #endregion
        #endregion

        #endregion

        #region SetVerificationCodeAgain
        #region SetStudentVerificationCode
        public ActionResult SetnewStudentVerificationCode(string RegId)
        {
            var result = this._accountBusiness.SetStudentVerificationCode(RegId);
            if(result == true)
            {
                ViewBag.NewCodeUpdated = true;
                return View("StudentsLogin");
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region SetUserVerificationCodeAgain
        public ActionResult SetUserVerificationCodeAgain(ItemCode data)
        {
            bool result = this._accountBusiness.SetUserVerificationCodeAgain(data);
            if(result == true)
            {
                ViewBag.VerificationEmail = true;
                return View("Login");
            }
            else
            {
                return View("Login");
            }
        }
        #endregion
        #endregion

        #region Verifies
        #region VerifyStudentAccount
        public ActionResult VerifyStudentAccount(ItemCode data)
        {
            bool result = this._accountBusiness.VerifyStudentAccount(data);
            if(result == true)
            {
                ViewBag.SuccessFullyVerified = true;
                return View("StudentsLogin");
            }
            else
            {
                return View();
            }
        }
        #endregion
        #endregion
        #endregion
    }
}