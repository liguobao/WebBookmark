using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;
using WebBookmarkBo.Service;
using WebBookmarkUI.Models;
using WebBookmarkBo;

namespace WebBookmarkUI.Controllers
{
    public class BookmarkRecommendController : Controller
    {
        //
        // GET: /BookmarkRecommend/

        public ActionResult Index()
        {
            var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            var lstBookmarkInfo = RecommendBo.GetRecommendBookmarkList(loginUID);
            if(lstBookmarkInfo==null || lstBookmarkInfo.Count==0)
            {
                lstBookmarkInfo = RecommendBo.GetRandomBookmarkList(0,200);
            }

            var dicUserInfo = UserInfoBo.GetListByUIDList(lstBookmarkInfo.Select(model => model.UserInfoID).ToList())
                .ToDictionary(model => model.UserInfoID, model => model);

            var lstModel = new List<UIBookmarkInfo>();
            lstModel.AddRange(lstBookmarkInfo.Select(model => new UIBookmarkInfo()
            {
                Href = model.Href,
                BookmarkName = model.BookmarkName,
                BookmarkInfoID = model.BookmarkInfoID,
                CreateTime = model.CreateTime,
                UserWebFolderID = model.UserWebFolderID,
                UserInfoID = model.UserInfoID,
                Host = model.Host,
                UserInfo = dicUserInfo.ContainsKey(model.UserInfoID) ? new UIUserInfo()
                {
                    UserEmail = dicUserInfo[model.UserInfoID].UserEmail,
                    UserInfoID = dicUserInfo[model.UserInfoID].UserInfoID,
                    UserName = dicUserInfo[model.UserInfoID].UserName,
                    UserImagURL = dicUserInfo[model.UserInfoID].UserImagURL,
                } : new UIUserInfo() { UserName = "这个人消失了", UserInfoID = 0 },
            }));
            lstModel = Extend.GetRandomList(lstModel);
          
            if(lstModel.Count>20)
            {
                var returnModel = lstModel.Take(20).DistinctBy(model => model.Href);
                return View(returnModel.ToList());
            }else
            {
                return View(lstModel);
            }
           
        }

        public ActionResult ShowSameHostBookmarkList(long bookmarkID)
        {

            var lstBookmarkInfo = RecommendBo.LoadSameHostBookmarkList(bookmarkID);
            if (lstBookmarkInfo == null)
                return PartialView("ShowSameHostBookmarkList",null);

            //每个用户只取两个书签数据
            var showBookmarkList = new List<BizBookmarkInfo>();
            var groupBookmark = lstBookmarkInfo.DistinctBy(model=>model.Href).GroupBy(model => model.UserInfoID);
            foreach(var group in groupBookmark)
            {
                if(group.Count()>=2)
                {
                    showBookmarkList.AddRange(group.Take(2));
                }else
                {
                    showBookmarkList.AddRange(group);
                }
            }

            var dicUserInfo = UserInfoBo.GetListByUIDList(showBookmarkList.Select(model => model.UserInfoID).ToList())
                .ToDictionary(model=>model.UserInfoID,model=>model);

            var lstModel = new List<UIBookmarkInfo>();
            lstModel.AddRange(showBookmarkList.Select(model => new UIBookmarkInfo()
            {
                Href = model.Href,
                BookmarkName = model.BookmarkName,
                BookmarkInfoID = model.BookmarkInfoID,
                CreateTime = model.CreateTime,
                UserWebFolderID = model.UserWebFolderID,
                UserInfoID = model.UserInfoID,
                Host = model.Host,
                UserInfo = dicUserInfo.ContainsKey(model.UserInfoID) ? new UIUserInfo()
                {
                    UserEmail = dicUserInfo[model.UserInfoID].UserEmail,
                    UserInfoID = dicUserInfo[model.UserInfoID].UserInfoID,
                    UserName = dicUserInfo[model.UserInfoID].UserName,
                    UserImagURL = dicUserInfo[model.UserInfoID].UserImagURL,
                } : new UIUserInfo() { UserName="这个人消失了",UserInfoID = 0},
            }));


            lstModel = Extend.GetRandomList(lstModel);

            return PartialView("ShowSameHostBookmarkList",lstModel.Count > 15 ? lstModel.Take(15).ToList():lstModel);
        }


    }
}
