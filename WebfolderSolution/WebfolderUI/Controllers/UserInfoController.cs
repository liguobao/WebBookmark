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
       
        // GET: UserInfo
        public ActionResult Index()
        {
            UIUserInfo uiUserInfo = null;

            if (!UILoginHelper.CheckUserLogin(Request))
                return View(uiUserInfo);
          
            var result =  UserInfoBo.GetUserInfoByLoginNameOrEmail(UILoginHelper.GetUIUserLoginNameOrEmail(Request));
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
                uiUserInfo.QQ = bizUserInfo.UserQQ;
                uiUserInfo.UserInfoComment = bizUserInfo.UserInfoComment;
                uiUserInfo.UserImagURL = bizUserInfo.UserImagURL;
            }
            return View(uiUserInfo);
        }


        public ActionResult SaveUserInfo(UIUserInfo uiUserInfo)
        {
            var loginEmail = UILoginHelper.GetUIUserLoginNameOrEmail(Request);
            return Json(SaveUserToDB(loginEmail, uiUserInfo));
        }


        public ActionResult CheckUserEmailOrLoginName(string emailOrLoginName)
        {
            var cookieLoginName= UILoginHelper.GetUIUserLoginNameOrEmail(Request);
            BizResultInfo result = new BizResultInfo();
            if (cookieLoginName.Equals(emailOrLoginName))
            {
                result.IsSuccess = true;
                result.SuccessMessage = "邮箱是有效的哦，可以使用。";
                
            }else
            {
                result = UserInfoBo.CheckUserEmailOrLoginName(emailOrLoginName);
            }
            return Json(result);
        }

        private BizResultInfo SaveUserToDB(String loginEmail, UIUserInfo uiUserInfo)
        {
           var result = UserInfoBo.GetUserInfoByLoginNameOrEmail(loginEmail);
            if (result.IsSuccess)
            {
                var userInfo = result.Target as BizUserInfo;
                userInfo.UserEmail = uiUserInfo.UserEmail;
                userInfo.UserInfoComment = uiUserInfo.UserInfoComment;
                userInfo.UserName = uiUserInfo.UserName;
                userInfo.UserQQ = uiUserInfo.QQ;
                userInfo.UserPhone = uiUserInfo.Phone;
                userInfo.UserLoginName = uiUserInfo.LoginName;
                userInfo.Save();
                result.IsSuccess = true;
                result.SuccessMessage = "保存成功了耶，你可以去别的地方玩了。";
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "没找到登陆信息呀呀呀！";
            }
            return (result);
        }


        public ActionResult SaveUserImag()
        {
            BizResultInfo result = UploadFileHelper.UploadFileToUserImg(Request);
            if(result.IsSuccess)
            {
                var loginEmail = UILoginHelper.GetUIUserLoginNameOrEmail(Request);
                var res=  UserInfoBo.GetUserInfoByLoginNameOrEmail(loginEmail);
                var bizUserInfo = res.Target as BizUserInfo;
                var path = Server.MapPath(bizUserInfo.UserImagURL);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                bizUserInfo.UserImagURL = result.ResultID;
                bizUserInfo.Save();
                result.SuccessMessage = "上传成功！";
            }
            return Json(result);
        }


        public ActionResult SaveIndex()
        {
            return View();
        }

    }
}