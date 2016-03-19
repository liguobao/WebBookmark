using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;
using WebBookmarkBo.Service;
using WebBookmarkUI.Models;

namespace WebBookmarkUI.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterUser(UIUserInfo uiUserInfo)
        {
            var bizUserInfo = new BizUserInfo();
            bizUserInfo.UserEmail = uiUserInfo.UserEmail;
            bizUserInfo.UserLoginName = uiUserInfo.UserEmail;
            bizUserInfo.UserName = uiUserInfo.UserName;
            bizUserInfo.UserPassword = uiUserInfo.Password;
            bizUserInfo.UserPhone = uiUserInfo.Phone;
            bizUserInfo.UserQQ = uiUserInfo.QQ;
            var result=  UserInfoBo.RegisterUser(bizUserInfo);

            return Json(result);
        }


        public ActionResult CheckUserEmail(String email)
        {
            return Json(UserInfoBo.CheckUserEmailOrLoginName(email));
        }
    }
}