using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;
using WebBookmarkBo.Service;
using WebBookmarkService.Model;
using WebBookmarkUI.Commom;
using WebBookmarkUI.Models;
using WebBookmarkService;

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
                bookmarkInfo.LoadBookmarkTagInfo();
                Dictionary<long, BizTagInfo> dicTagInfo = BizTagInfo.LoadByUserID(bookmarkInfo.UserInfoID).ToDictionary(tag => tag.TagInfoID, tag => tag);

                //var tagInfos = BizTagInfo.LoadByUserID(bookmarkInfo.UserInfoID);
                //if(tagInfos!=null && tagInfos.Count>0)
                //{
                //  dicTagInfo= tagInfos.ToDictionary(tag => tag.TagInfoID, tag => tag);
                //}
                model = new UIBookmarkInfo();
                model.BookmarkInfoID = bookmarkInfo.BookmarkInfoID;
                model.BookmarkName = bookmarkInfo.BookmarkName;
                model.Host = bookmarkInfo.Host;
                model.Href = bookmarkInfo.Href;
                model.UserInfoID = bookmarkInfo.UserInfoID;
                model.UserWebFolderID = bookmarkInfo.UserWebFolderID;
                //model.HTML = bookmarkInfo.HTML;
                model.CreateTime = bookmarkInfo.CreateTime;
                var bookmarkUserInfo = BizUserInfo.LoadByUserInfoID(bookmarkInfo.UserInfoID);
                model.UserInfo = new UIUserInfo() 
                { 
                    UserName = bookmarkUserInfo.UserName,
                    UserEmail = bookmarkUserInfo.UserEmail,
                    UserInfoID = bookmarkUserInfo.UserInfoID,
                };
                model.TagInfoList = bookmarkInfo.BizBookmarkTagInfoList.Select(btg => new UIBookmarkTagInfo() 
                {
                    BookmarkTagInfoID = btg.BookmarkTagInfoID,
                    BookmarkInfoID = btg.BookmarkInfoID,
                    TagInfoID = btg.TagInfoID,
                    CreateTime = btg.CreateTime,
                    TagInfo = dicTagInfo.ContainsKey(btg.TagInfoID) ? dicTagInfo[btg.TagInfoID] : null,
                }).ToList();
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
                {
                    model = new UIBookmarkInfo();
                    model.HTML = bookmarkInfo.HTML;
                    model.Href = bookmarkInfo.Href;
                    model.UserInfoID = bookmarkInfo.UserInfoID;
                    model.Host = bookmarkInfo.Host;
                    model.BookmarkName = bookmarkInfo.BookmarkName;
                    model.BookmarkInfoID = bookmarkInfo.BookmarkInfoID;
                    return PartialView("ShowBookmarkHTML", model);
                }
                   
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
            var lstBookmarkComment = BizBookmarkComment.LoadByBookmarkInfoID(bookmarkID);

            var loginUserInfo = BizUserInfo.LoadByUserInfoID(UILoginHelper.GetUIDFromHttpContext(HttpContext));

            UIUserInfo uiLoginUserInfo = new UIUserInfo()
            {
                UserName = loginUserInfo.UserName,
                UserInfoID = loginUserInfo.UserInfoID,
                UserImagURL = loginUserInfo.UserImagURL,
                UserEmail = loginUserInfo.UserEmail,
            };

            var lstUserID = lstBookmarkComment.Select(model => model.CriticsUserID).Distinct().ToList();
            var dicUserInfo = UserInfoBo.GetListByUIDList(lstUserID).ToDictionary(model=>model.UserInfoID,model=>model);

            IEnumerable<UICommentInfo> lstComment = new List<UICommentInfo>();
            lstComment = lstBookmarkComment.Select(model => new UICommentInfo()
            {
                BookmarkUserID = model.BookmarkUserID,
                CommentContent = model.CommentContent,
                CommentTitle = model.CommentTitle,
                CommentID = model.BookmarkCommentID,
                CreateTime = model.CreateTime,
                CriticsUserID = model.CriticsUserID,
                InfoID = model.BookmarkInfoID,
                InfoType = UICommentType.Bookmark,
                CriticsUserInfo = dicUserInfo.ContainsKey(model.CriticsUserID) ?  new UIUserInfo() 
                {
                    UserName = dicUserInfo[model.CriticsUserID].UserName,
                    UserEmail = dicUserInfo[model.CriticsUserID].UserEmail,
                    UserImagURL = dicUserInfo[model.CriticsUserID].UserImagURL,
                    UserInfoID = dicUserInfo[model.CriticsUserID].UserInfoID,
                }:new UIUserInfo(),

            });


            return PartialView("ShowBookmarkComment", Tuple.Create<IEnumerable<UICommentInfo>,UIUserInfo>(lstComment,uiLoginUserInfo)); 
        }


        public ActionResult SaveBookmarkComment(long bookmarkID, string content)
        {

            BizResultInfo result = new BizResultInfo();
            try
            {
                var bookmark = BizBookmarkInfo.LoadByID(bookmarkID);
                if (bookmark == null || bookmark.BookmarkInfoID==0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "序列化书签数据失败，目测你要重新加载页面。";
                    return Json(result);
                }
                BizBookmarkComment comment = new BizBookmarkComment();
                comment.CriticsUserID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
                comment.CommentTitle = "";
                comment.CommentContent = content;
                comment.BookmarkInfoID = bookmark.BookmarkInfoID;
                comment.CreateTime = DateTime.Now;
                comment.BookmarkUserID = bookmark.UserInfoID;
                comment.Save();
                var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
                MessageBo.CreateMessage(bookmark.UserInfoID, MessageTypeEnum.NewBookmarkComment, comment);
                UserDynamicInfoBo.CreateDynamicInfoMessage(loginUID, DynamicInfoType.NewBookmarkComment, bookmark);

                result.IsSuccess = true;
                result.SuccessMessage = "提交成功。";
            }catch(Exception ex)
            {
                LogHelper.WriteException("SaveBookmarkComment",ex,new {
                    BookmarkID = bookmarkID,
                    SubmitUser = UILoginHelper.GetUIDFromHttpContext(HttpContext),
                    Content = content,
                });
                result.ErrorMessage = "提交失败，目测网络挂了或者别的....";
                result.IsSuccess = false;
            }
            return Json(result);
        }


        public ActionResult SaveBookmarkTag(long bookmarkID,long tagInfoID=0,string tagName="")
        {
            BizResultInfo result = new BizResultInfo();
            var bookmarkInfo = BizBookmarkInfo.LoadByID(bookmarkID);
            if(bookmarkInfo==null || bookmarkInfo.BookmarkInfoID==0)
            {
                result.ErrorMessage = "书签数据为空，不要逗我玩啦...";
                return Json(result);
            }
            bookmarkInfo.LoadBookmarkTagInfo();

            if(tagInfoID!=0 && bookmarkInfo.BizBookmarkTagInfoList.All(mode=>mode.TagInfoID!=tagInfoID))
            {
                bookmarkInfo.AddBookmarkTag(tagInfoID);
            }
            var errorMessage = string.Empty;

            if (!string.IsNullOrEmpty(tagName))
            {
               errorMessage = bookmarkInfo.AddBookmarkTag(tagName);
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                result.ErrorMessage = errorMessage;
                result.IsSuccess = false;
            }else
            {
                result.SuccessMessage = "保存成功！";
                result.IsSuccess = true;
            }
            return Json(result);
        }


        public ActionResult RemoveBookmarkTag(long bookmarkID,long bookmarkTagInfoID=0,string tagName="")
        {
            BizResultInfo result = new BizResultInfo();
            var bookmarkInfo = BizBookmarkInfo.LoadByID(bookmarkID);
           
            if(bookmarkInfo==null || bookmarkInfo.BookmarkInfoID==0)
            {
                result.ErrorMessage = "书签数据为空，不要逗我玩啦...";
                return Json(result);
            }
            bookmarkInfo.LoadBookmarkTagInfo();
            if (!string.IsNullOrEmpty(tagName))
            {
                bookmarkInfo.RemoveByTagName(tagName);
            }

            if(bookmarkTagInfoID!=0)
            {
                bookmarkInfo.RemoveByBookmarkTagID(bookmarkTagInfoID);
            }
            result.SuccessMessage = "移除成功！";
            result.IsSuccess = true;
          
            return Json(result);
        }
    }
}
