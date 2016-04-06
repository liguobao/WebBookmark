using System;
using WebBookmarkBo.Model;
using WebBookmarkService.DAL;
using System.Linq;
using System.Collections.Generic;


namespace WebBookmarkBo.Service
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
                    result.ResultID = newUserInfo.UserInfoID.ConvertToCiphertext();
                    result.SuccessMessage = "注册成功";
                    MessageBo.CreateMessage(newUserInfo.UserInfoID, MessageTypeEnum.WelcomeToWebBookmark, newUserInfo);
                }
                else
                {
                    result.IsSuccess = false;
                    result.SuccessMessage = "注册失败，请重试。";
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
        /// 检查邮箱/登陆账号是否有效
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static BizResultInfo CheckUserEmailOrLoginName(string email)
        {
            var result = new BizResultInfo();
            if(string.IsNullOrEmpty(email))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "邮箱/登陆账号不能为空。";
                return result;
            }

            var newUserInfo = BizUserInfo.LoadByUserEmailOrUserLoginName(email);
            if (newUserInfo == null || newUserInfo.UserInfoID == 0)
            {
                result.IsSuccess = true;
                result.ResultID = newUserInfo.UserInfoID.ConvertToCiphertext();
                result.SuccessMessage = "邮箱/登陆账号有效，可以注册。";
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "此邮箱/登陆账号已被占用，请输入新邮箱/登陆账号/找回密码。";
              
            }
            return result;
        }


        public static BizResultInfo CheckUserEmail(string email)
        {
            var result = new BizResultInfo();
            if (string.IsNullOrEmpty(email))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "邮箱不能为空。";
                return result;
            }

            var newUserInfo = BizUserInfo.LoadByUserEmailOrUserLoginName(email);
            if (newUserInfo == null || newUserInfo.UserInfoID == 0)
            {
                result.IsSuccess = true;
                result.ResultID = newUserInfo.UserInfoID.ConvertToCiphertext();
                result.SuccessMessage = "邮箱有效，可以注册。";
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "此邮箱已被占用。";

            }
            return result;
        }


        public static BizResultInfo GetUserInfoByUID(long uid)
        {
            var userInfo = BizUserInfo.LoadByUserInfoID(uid);
            var result = new BizResultInfo();
            if(userInfo!=null && userInfo.UserInfoID!=0)
            {
                result.ResultID = uid.ConvertToCiphertext();
                result.Target = userInfo;
                result.IsSuccess = true;
            }else
            {
                result.ResultID = "";
                result.Target = null;
                result.IsSuccess = false;
            }
            return result;
        }


        public static BizResultInfo GetUserInfoByLoginNameOrEmail(string loginName)
        {
            BizResultInfo result = new BizResultInfo();
            var newUserInfo = BizUserInfo.LoadByUserEmailOrUserLoginName(loginName);
            if(newUserInfo!=null && newUserInfo.UserInfoID!=0)
            {
                result.IsSuccess = true;
                result.Target = newUserInfo;
                result.ResultID = newUserInfo.UserInfoID.ConvertToCiphertext();
            }else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "找遍了数据库都没找到这个人呀...要不，你换个号？";
            }
            return result;
        }

        public static BizResultInfo UserLogin(string loginNameOrEmail,string password)
        {
            BizResultInfo result = new BizResultInfo();
            var rsp = GetUserInfoByLoginNameOrEmail(loginNameOrEmail);
            if(rsp.IsSuccess)
            {
                var userInfo = rsp.Target as BizUserInfo;
                if(userInfo==null)
                {
                    result.ErrorMessage = "数据库可能被外星人劫持了，稍等片刻....";
                    result.IsSuccess = false;
                    return result;
                }
                
                if(userInfo.UserPassword != password)
                {
                    result.ErrorMessage = "亲，密码不对哦，麻烦重新输入密码....";
                    result.IsSuccess = false;
                    return result;
                }else
                {
                    result.SuccessMessage="登陆成功，正在前往目的地....";
                    result.IsSuccess = true;
                    result.ResultID = userInfo.UserInfoID.ConvertToCiphertext();
                    return result;
                }
            }
            result.ErrorMessage = "账号不存在呀，麻烦点击旁边去注册一下咯。";
            return result;


        }

        /// <summary>
        /// 邮箱/用户名搜索用户
        /// </summary>
        /// <param name="nameOrEmail"></param>
        /// <returns></returns>
        public static List<BizUserInfo> SearchUserList(string nameOrEmail)
        {
            var list = new UserInfoDAL().SearchByNameOrEmail(nameOrEmail);

            if (list == null || list.Count() == 0)
                return new List<BizUserInfo>();
            return list.Select(model => new BizUserInfo(model)).ToList();
        }

        /// <summary>
        /// 用户ID获取用户信息
        /// </summary>
        /// <param name="lstUID"></param>
        /// <returns></returns>
        public static List<BizUserInfo> GetListByUIDList(List<long> lstUID)
        {
            if (lstUID == null || lstUID.Count == 0)
                return new List<BizUserInfo>();
            var list = new UserInfoDAL().SearchByUID(lstUID);
            if (list == null || list.Count() == 0)
                return new List<BizUserInfo>();
            return list.Select(model => new BizUserInfo(model)).ToList();
        }


    }
}
