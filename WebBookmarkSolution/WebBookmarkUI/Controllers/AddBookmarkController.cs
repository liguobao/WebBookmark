using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;
using WebBookmarkBo;
using WebBookmarkUI.Commom;
using HtmlAgilityPack;
using WebBookmarkService;

namespace WebBookmarkUI.Controllers
{
    public class AddBookmarkController : Controller
    {
        //
        // GET: /AddBookmark/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string url,long uid,long folderID)
        {
            BizResultInfo result = new BizResultInfo();
            var userInfo = BizUserInfo.LoadByUserInfoID(uid);

            if (string.IsNullOrEmpty(url) || userInfo==null)
            {
                LogHelper.WriteInfo("AddBookmark失败", "AddBookmark失败", new { UserInfoID = uid, URL = url, FolderID = folderID });
                return Json(result);
            }

            //不存在书签夹，创建一个默认的书签夹
            var folderInfo = BizUserWebFolder.LoadByID(folderID);
            if(folderInfo.UserWebFolderID==0)
            {
              
                folderInfo.UserInfoID = uid;
                folderInfo.WebFolderName = userInfo.UserName + "的默认书签夹";
                folderInfo.IntroContent = "";
                folderInfo.IElementJSON = "";
                folderInfo.Grade = 0;
                folderInfo.CreateTime = DateTime.Now;
                folderInfo.IElementHashcode = folderInfo.GetHashCode();
                folderInfo.Save();
                folderID = folderInfo.UserWebFolderID;
            }

            BizBookmarkInfo bookmark = new BizBookmarkInfo();
            bookmark.Href = url;
            var res = HTTPHelper.GetHTML(url);
            if (!string.IsNullOrEmpty(res.Item1))
            {
                bookmark.HTML = res.Item1;
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(res.Item1);
                var title = htmlDoc.DocumentNode.SelectSingleNode("//title").InnerText;
                bookmark.BookmarkName = !string.IsNullOrEmpty(title) ? title:url ;
            }else
            {
                bookmark.BookmarkName = url;
            }

            if (string.IsNullOrEmpty(res.Item2) || res.Item2.ToUpper() == "ALLOW-FROM")
            {
                bookmark.IsShowWithiframe = 1;
            }

            bookmark.UserWebFolderID = folderID;
            bookmark.CreateTime = DateTime.Now;
            bookmark.Host = url.GetHost();
            bookmark.UserInfoID = uid;
            bookmark.HashCode = bookmark.GetHashCode();
            bookmark.Save();
            return Json(result);
        }

    }
}
