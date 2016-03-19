using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Service;

namespace WebBookmarkUI
{
    public class UILoginHelper
    {
        public static string  GetUIUserLoginNameOrEmail(HttpRequestBase requestBase)
        {
            string loginNameOrEmail = string.Empty;
            var userInfo = requestBase.Cookies["UserInfo"];
            if (userInfo != null)
                loginNameOrEmail = userInfo.Values["LoginNameOrEmail"].ConvertToPlaintext();
            return loginNameOrEmail;
        }


        public static string GetUIUserPassword(HttpRequestBase requestBase)
        {
            string password = string.Empty;
            var userInfo = requestBase.Cookies["UserInfo"];
            if (userInfo != null)
                password = userInfo.Values["Token"].ConvertToPlaintext();
            return password;
        }


        public static long GetUIDInCookie(HttpRequestBase requestBase)
        {
            long uid = 0;
            var userInfo = requestBase.Cookies["UserInfo"];
            if (userInfo != null)
                uid = userInfo.Values["UID"].ConvertToPlainLong();
            return uid;
        }

        public static void WriteUserInfo(string uid, string loginNameOrEmail,string password,HttpResponseBase responseBase)
        {
            HttpCookie cookie = new HttpCookie("UserInfo");
            cookie.Values.Add("UID", uid);
            cookie.Values.Add("LoginNameOrEmail", loginNameOrEmail.ConvertToCiphertext());
            cookie.Values.Add("Token",password.ConvertToCiphertext());
            responseBase.Cookies.Add(cookie);
        }



        public static bool CheckUserLogin(HttpRequestBase requestBase)
        {
            var rsp = UserInfoBo.UserLogin(GetUIUserLoginNameOrEmail(requestBase), GetUIUserPassword(requestBase));
            return rsp.IsSuccess;  
        }
    }
}