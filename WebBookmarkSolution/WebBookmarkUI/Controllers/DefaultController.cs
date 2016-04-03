using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Service;
using WebBookmarkUI.Commom;

namespace WebBookmarkUI.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            var lstModel = UserDynamicInfoBo.LoadDynamicLog(loginUID);
            return View(lstModel);
        }
    }
}