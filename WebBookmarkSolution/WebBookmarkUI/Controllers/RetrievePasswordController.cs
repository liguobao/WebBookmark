using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo;
using WebBookmarkBo.BizModel;
using WebBookmarkBo.Model;
using WebBookmarkBo.Service;
using WebBookmarkService;

namespace WebBookmarkUI.Controllers
{
    public class RetrievePasswordController : Controller
    {
        //
        // GET: /RetrievePassword/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendRetrievePasswordEmail(string emailAccount)
        {

            BizResultInfo result = new BizResultInfo();
            if(string.IsNullOrEmpty(emailAccount))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "邮箱地址不能为空。";
                return Json(result);
            }

            result = UserInfoBo.GetUserInfoByLoginNameOrEmail(emailAccount);
            if (result.IsSuccess)
            {
                var retrievePasswordEmailContent = BizConfigurationInfo.LoadByKey(Conts.RetrievePasswordEmailContentKey).ConfigurationValue;
                var userInfo = result.Target as BizUserInfo;
                if (userInfo == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "找不到对象了...可能这就是命吧。";
                    return Json(result);
                }

                string token = userInfo.UserInfoID.ConvertToCiphertext();
                
                EmailInfo emailInfo = new EmailInfo();
                emailInfo.Subject = "WebBookmark找回密码";
                emailInfo.Body = string.Format(retrievePasswordEmailContent,token,token);
                emailInfo.Receiver = emailAccount;
                emailInfo.Send();


                
            }
            result.IsSuccess = true;
            
            return Json(result);
        }

        public ActionResult ModifyIndex(string token)
        {
            return View();
        }

        public ActionResult RetrievePassword(string token, string newPassword)
        {
            BizResultInfo result = new BizResultInfo();
            if(string.IsNullOrEmpty(token))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Token不能为空，请重新获取重置密码邮件。";
                return Json(result);
            }
            try
            {
                var userID = token.ConvertToPlainLong();
                var userInfo = BizUserInfo.LoadByUserInfoID(userID);
                userInfo.UserPassword = newPassword;
                userInfo.Save();
                result.IsSuccess = true;
                result.SuccessMessage = "重置密码成功了，现在赶紧去登录吧。";
                return Json(result);
                
            }
            catch(Exception ex)
            {
                LogHelper.WriteException("RetrievePassword ModifyPassword Exception", ex, new { Token = token, Password = newPassword });
                result.IsSuccess = false;
                result.ErrorMessage = "重置密码挂掉了，还是重新获取重置密码邮件再来试试吧。";
                return Json(result);
            }
            
        }



        public ActionResult CheckUserEmail(String email)
        {
            return Json(UserInfoBo.CheckUserEmail(email));
        }
    }
}
