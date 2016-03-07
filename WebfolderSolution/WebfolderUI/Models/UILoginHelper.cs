using System.Web;
using System.Web.Mvc;
using WebfolderBo.Service;

namespace WebfolderUI
{
    public class UILoginHelper: Controller
    {
        public string  GetUIUserLoginNameOrEmail()
        {
            string loginNameOrEmail = string.Empty;
            var userInfo = Request.Cookies["UserInfo"];
            if (userInfo != null)
                loginNameOrEmail = userInfo.Values["LoginNameOrEmail"].ConvertToPlaintext();
            return loginNameOrEmail;
        }


        public string GetUIUserPassword()
        {
            string password = string.Empty;
            var userInfo = Request.Cookies["UserInfo"];
            if (userInfo != null)
                password = userInfo.Values["Token"].ConvertToPlaintext();
            return password;
        }

        public void WriteUserInfo(string loginNameOrEmail,string password)
        {
            HttpCookie cookie = new HttpCookie("UserInfo");
            cookie.Values.Add("LoginNameOrEmail", loginNameOrEmail.ConvertToCiphertext());
            cookie.Values.Add("Token",password.ConvertToCiphertext());
            Response.Cookies.Add(cookie);
        }

    }
}