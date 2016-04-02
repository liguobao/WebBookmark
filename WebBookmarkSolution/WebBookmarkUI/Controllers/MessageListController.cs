using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;

namespace WebBookmarkUI.Controllers
{
    public class MessageListController : Controller
    {
        //
        // GET: /MessageList/

        public ActionResult Index()
        {
            var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            var lstModel = BizMessageInfo.LoadByUserID(loginUID);
            return View(lstModel);
        }

    }
}
