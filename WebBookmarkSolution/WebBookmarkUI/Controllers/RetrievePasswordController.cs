using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo;
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

                string token = (userInfo.UserInfoID+ userInfo.UserEmail + DateTime.Now).ConvertToCiphertext();
                var retrievePasswordLog = new BizRetrievePasswordLog();
                retrievePasswordLog.CreateTime = DateTime.Now;
                retrievePasswordLog.LogStatus = 0;
                retrievePasswordLog.Token = token;
                retrievePasswordLog.UserInfoID = userInfo.UserInfoID;
                retrievePasswordLog.LastTime = DateTime.Now;
                retrievePasswordLog.Save();
                
                
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

                var retrievePasswordLog = BizRetrievePasswordLog.LoadByToken(token);
                if(retrievePasswordLog==null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "找不到重置密码记录，不要逗我玩啦。";
                    return Json(result);
                }
                if ((DateTime.Now - retrievePasswordLog.CreateTime).TotalHours > 24)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "重置密码链接已超时，请重新申请。";
                    return Json(result);
                }
                if (retrievePasswordLog.LogStatus ==1)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "重置密码链接已使用，请重新申请。";
                    return Json(result);
                }




                var userInfo = BizUserInfo.LoadByUserInfoID(retrievePasswordLog.UserInfoID);
                userInfo.UserPassword = newPassword;
                userInfo.Save();
                retrievePasswordLog.LogStatus = 1;
                retrievePasswordLog.LastTime = DateTime.Now;
                retrievePasswordLog.Save();

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
