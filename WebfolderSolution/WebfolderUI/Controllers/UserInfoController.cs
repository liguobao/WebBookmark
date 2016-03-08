using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebfolderBo.Model;
using WebfolderBo.Service;
using WebfolderUI.Models;

namespace WebfolderUI.Controllers
{
    public class UserInfoController : Controller
    {
        private static UILoginHelper uiLoginHelper = new UILoginHelper();
        // GET: UserInfo
        public ActionResult Index()
        {
            UIUserInfo uiUserInfo = null;

            if (!uiLoginHelper.CheckUserLogin(Request))
                return View(uiUserInfo);
          
            var result =  UserInfoBo.GetUserInfoByLoginName(uiLoginHelper.GetUIUserLoginNameOrEmail(Request));
            if (result.IsSuccess)
            {
                var bizUserInfo = result.Target as BizUserInfo;
                if (bizUserInfo == null || bizUserInfo.UserInfoID == 0)
                    return View(uiUserInfo);
                uiUserInfo = new UIUserInfo();
                uiUserInfo.UserEmail = bizUserInfo.UserEmail;
                uiUserInfo.UserName = bizUserInfo.UserName;
                uiUserInfo.LoginName = bizUserInfo.UserLoginName;
                uiUserInfo.Phone = bizUserInfo.UserPhone;
                uiUserInfo.LoginName = bizUserInfo.UserLoginName;
                uiUserInfo.UserInfoComment = bizUserInfo.UserInfoComment;
                uiUserInfo.UserImagURL = bizUserInfo.UserImagURL;
            }
            return View(uiUserInfo);
        }
    }
}