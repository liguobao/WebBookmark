using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebfolderBo.Model;
using WebfolderService.BLL;
using WebfolderService.Model;

namespace WebfolderBo.Service
{
    public class UserInfoBo
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static BizResultInfo RegisterUser(BizUserInfo userInfo)
        {
            var result = new BizResultInfo();
            try
            {
              
                userInfo.Save();
                var newUserInfo = BizUserInfo.LoadByUserEmailOrUserLoginName(userInfo.UserEmail);
                if (newUserInfo != null && newUserInfo.UserInfoID != 0)
                {
                    result.IsSuccess = true;
                    result.ResultID = newUserInfo.UserInfoID;
                    result.ResultMessage = "注册成功";
                }
                else
                {
                    result.IsSuccess = false;
                    result.ResultMessage = "注册失败，请重试。";
                }
            }
            catch(Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.ToString();
              
            }
            return result;
        }


        /// <summary>
        /// 检查邮箱是否有效
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static BizResultInfo CheckUserEmail(string email)
        {
            var result = new BizResultInfo();
            if(string.IsNullOrEmpty(email))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "邮箱不能为空。";
                return result;
            }

            var newUserInfo = BizUserInfo.LoadByUserEmailOrUserLoginName(email);
            if (newUserInfo == null || newUserInfo.UserInfoID == 0)
            {
                result.IsSuccess = true;
                result.ResultID = 0;
                result.ResultMessage = "邮箱有效，可以注册。";
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "此邮箱已被占用，请输入新邮箱/找回密码。";
                result.ResultMessage = "此邮箱已被占用，请输入新邮箱/找回密码。";
            }
            return result;
        }

    }
}
