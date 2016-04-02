using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;

namespace WebBookmarkUI.Commom
{
   /// <summary>
   /// 用户登陆权限验证
   /// </summary>
   public class SessionUserParameterAttribute : ActionFilterAttribute
{
       public override void OnActionExecuting(ActionExecutingContext filterContext)
       {
           if(filterContext!=null)
           {
               var loginUid = UILoginHelper.GetUIDFromHttpContext(filterContext.HttpContext);
               var userInfo = BizUserInfo.LoadByUserInfoID(loginUid);
               if(userInfo==null)
               {
                   throw new Exception("找不到你的登陆信息，不能让你继续玩下去.....");
               }
               var password = UILoginHelper.GetUIUserPassword(filterContext.HttpContext);

               if(userInfo.UserPassword != password)
               {
                   throw new Exception("找不到你的Token信息，不能让你继续玩下去.....");
               }
           }
       }
}
}