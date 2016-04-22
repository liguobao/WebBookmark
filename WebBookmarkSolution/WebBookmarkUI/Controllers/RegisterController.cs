using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;
using WebBookmarkBo.Service;
using WebBookmarkUI.Models;
using WebBookmarkBo;

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
            bizUserInfo.UserInfoComment = uiUserInfo.UserInfoComment;
            string token = (uiUserInfo.UserEmail + uiUserInfo.UserName).ConvertToCiphertext();
            bizUserInfo.ActivateAccountToken = token;
            bizUserInfo.AccountStatus = 0;
            var result=  UserInfoBo.RegisterUser(bizUserInfo);

            if(result.IsSuccess)
            {
                var activateAccountEmailContent = BizConfigurationInfo.LoadByKey(Conts.ActivateAccountEmailContentKey).ConfigurationValue;
                EmailInfo activateAccountEmail = new EmailInfo();
                activateAccountEmail.Receiver = uiUserInfo.UserEmail;
                activateAccountEmail.Subject = "激活WebBookmark账号";
                activateAccountEmail.Body = string.Format(activateAccountEmailContent, token, token);
                activateAccountEmail.Send();
            }
            return Json(result);
        }


        public ActionResult CheckUserEmail(String email)
        {
            return Json(UserInfoBo.CheckUserEmail(email));
        }


        public ActionResult ActivateAccount(string token)
        {
            BizResultInfo result = new BizResultInfo();
            var userInfo = BizUserInfo.LoadByActivateAccountToken(token);
            if(userInfo!=null && userInfo.UserInfoID!=0)
            {
                if (userInfo.AccountStatus==1)
                {
                    result.IsSuccess = true;
                    result.SuccessMessage = "账号已激活。";
                }else
                {
                    userInfo.AccountStatus = 1;
                    userInfo.Save();
                    result.IsSuccess = true;
                    result.SuccessMessage = "激活账号成功！";
                }
               
            }else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "找不到账号信息，请检查激活链接是否完整！";
            }

            return View(result);
        }
    }
}