using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBookmarkUI.Commom
{
   public class SessionUserParameterAttribute : ActionFilterAttribute
{
       public override void OnActionExecuting(ActionExecutingContext filterContext)
       {
           if(filterContext!=null)
           {
               var loginUid = UILoginHelper.GetUIDFromHttpContext(filterContext.HttpContext);
           }
       }
}
}