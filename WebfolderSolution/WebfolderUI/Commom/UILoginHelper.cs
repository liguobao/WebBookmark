using System.Web;
using System.Web.Mvc;
using WebfolderBo.Service;

namespace WebfolderUI
{
    public class UILoginHelper
    {
        public string  GetUIUserLoginNameOrEmail(HttpRequestBase requestBase)
        {
            string loginNameOrEmail = string.Empty;
            var userInfo = requestBase.Cookies["UserInfo"];
            if (userInfo != null)
                loginNameOrEmail = userInfo.Values["LoginNameOrEmail"].ConvertToPlaintext();
            return loginNameOrEmail;
        }


        public string GetUIUserPassword(HttpRequestBase requestBase)
        {
            string password = string.Empty;
            var userInfo = requestBase.Cookies["UserInfo"];
            if (userInfo != null)
                password = userInfo.Values["Token"].ConvertToPlaintext();
            return password;
        }

        public void WriteUserInfo(string loginNameOrEmail,string password,HttpResponseBase responseBase)
        {
            HttpCookie cookie = new HttpCookie("UserInfo");
            cookie.Values.Add("LoginNameOrEmail", loginNameOrEmail.ConvertToCiphertext());
            cookie.Values.Add("Token",password.ConvertToCiphertext());
            responseBase.Cookies.Add(cookie);
        }



        public bool CheckUserLogin(HttpRequestBase requestBase)
        {
            var rsp = UserInfoBo.UserLogin(GetUIUserLoginNameOrEmail(requestBase), GetUIUserPassword(requestBase));
            return rsp.IsSuccess;  
        }
    }
}