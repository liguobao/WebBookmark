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
            return View();
        }

        public ActionResult ShowAllMessage()
        {
            var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            var lstModel = BizMessageInfo.LoadByUserID(loginUID);
            return View("ShowAllMessage", lstModel);
        }

        public ActionResult ShowNotReadMessage()
        {
            var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            var lstModel = BizMessageInfo.LoadNotReadListByUserID(loginUID);
            return View("ShowNotReadMessage", lstModel);
        }

        public ActionResult ShowHasReadMessage()
        {
            var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            var lstModel = BizMessageInfo.LoadHasReadListByUserID(loginUID);
            return View("ShowHasReadMessage", lstModel);
        }


        public ActionResult ShowMessageContent(long messageID)
        {
            var model = BizMessageInfo.LoadByMessageID(messageID);
            if(model!=null && model.IsRead ==(int) MessageReadStatus.NotRead)
            {
                model.SetToHasRead();
            }
            return View(model);
        }

    }
}
