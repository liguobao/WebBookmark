using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;
using WebBookmarkUI.Commom;
using WebBookmarkUI.Models;

namespace WebBookmarkUI.Controllers
{
    public class ShowBookmarkInfoController : Controller
    {
        //
        // GET: /ShowBookmarkInfo/

        public ActionResult Index(long bookmarkID)
        {
            if (bookmarkID == 0)
                return View();

            UIBookmarkInfo model = null;
            var bookmarkInfo = BizBookmarkInfo.LoadByID(bookmarkID);
            if (bookmarkInfo != null)
            {
                model = new UIBookmarkInfo();
                model.BookmarkInfoID = bookmarkInfo.BookmarkInfoID;
                model.BookmarkName = bookmarkInfo.BookmarkName;
                model.Host = bookmarkInfo.Host;
                model.Href = bookmarkInfo.Href;
                model.UserInfoID = bookmarkInfo.UserInfoID;
                model.UserWebFolderID = bookmarkInfo.UserWebFolderID;
                model.HTML = bookmarkInfo.HTML;
                model.CreateTime = bookmarkInfo.CreateTime;
            }
            return View(model);
        }

        public ActionResult ShowBookmarkHTML(long bookmarkID,string url)
        {
            UIBookmarkInfo model = null;
            string html = string.Empty;
            if(bookmarkID==0 || string.IsNullOrEmpty(url))
                return PartialView("ShowBookmarkHTML", model);


            var bookmarkInfo = BizBookmarkInfo.LoadByID(bookmarkID);
            if(bookmarkInfo==null)
                return PartialView("ShowBookmarkHTML", model);
            if (string.IsNullOrEmpty(bookmarkInfo.HTML))
            {
                html = HTTPHelper.GetHTML(url);
                if (string.IsNullOrEmpty(html))
                    return PartialView("ShowBookmarkHTML", model);
                bookmarkInfo.HTML = html;
                bookmarkInfo.Save();
            }
            model = new UIBookmarkInfo();
            model.HTML = bookmarkInfo.HTML;
            model.Href = bookmarkInfo.Href;
            model.UserInfoID = bookmarkInfo.UserInfoID;
            model.Host = bookmarkInfo.Host;
            model.BookmarkName = bookmarkInfo.BookmarkName;
            model.BookmarkInfoID = bookmarkInfo.BookmarkInfoID;
            return PartialView("ShowBookmarkHTML", model); 
        }



        public ActionResult ShowBookmarkComment(long bookmarkID)
        {
            return PartialView("ShowBookmarkComment", null); 
        }


    }
}
