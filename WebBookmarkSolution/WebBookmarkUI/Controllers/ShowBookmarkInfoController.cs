using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;
using WebBookmarkBo.Service;
using WebBookmarkUI.Commom;
using WebBookmarkUI.Models;
using WebBookmarkService;
using System.Text;

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
                bookmarkInfo.LoadLikeLog();
                model = new UIBookmarkInfo();

                model.LikeCount = bookmarkInfo.LikeCount;
                model.LikeLogList = bookmarkInfo.LikeLogList;

                Dictionary<long, BizTagInfo> dicTagInfo = BizTagInfo.LoadByUserID(bookmarkInfo.UserInfoID).ToDictionary(tag => tag.TagInfoID, tag => tag);
              
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
           
            if(bookmarkID==0 || string.IsNullOrEmpty(url))
                return PartialView("ShowBookmarkHTML", model);


            var bookmarkInfo = BizBookmarkInfo.LoadByID(bookmarkID);
            if(bookmarkInfo==null)
                return PartialView("ShowBookmarkHTML", model);
            model = new UIBookmarkInfo();
            model.IsShowWithiframe = bookmarkInfo.IsShowWithiframe == 1;//是否可在iframe中展示

            if (string.IsNullOrEmpty(bookmarkInfo.HTML))
            {
                var result = HTTPHelper.GetHTML(url);
                if (string.IsNullOrEmpty(result.Item1))
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

                bookmarkInfo.HTML = result.Item1;

                if (string.IsNullOrEmpty(result.Item2) || result.Item2.ToUpper() == "ALLOW-FROM")
                {
                    bookmarkInfo.IsShowWithiframe = 1;
                    model.IsShowWithiframe = true;
                }

                bookmarkInfo.Save();
            }
           
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


        public ActionResult AddBookmarkLikeLog(long bookmarkID)
        {
            BizResultInfo result = new BizResultInfo();
            var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);

            var bookmarkInfo = BizBookmarkInfo.LoadByID(bookmarkID);
            
            if(bookmarkInfo!=null)
            {
                var likelog = BizLikeLog.LoadUserIDAndBookmarkID(loginUID,bookmarkID);
                if(likelog !=null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "你已经点赞过了呀，不要重复点赞了。";
                }else
                {
                    likelog = new BizLikeLog();
                    likelog.InfoID = bookmarkID;
                    likelog.InfoType = (int)InfoTypeEnum.Bookmark;
                    likelog.UserInfoID = loginUID;
                    likelog.Save();

                    bookmarkInfo.LoadLikeLog();
                    result.Target = bookmarkInfo.LikeCount;
                    result.IsSuccess = true;
                    result.SuccessMessage = "点赞成功。";
                }
            }else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "找不到书签呀，刷新一下再来试试？";
            }


            return Json(result);

        }


        public ActionResult CollectBookmarkToUserDefaultFolder(long bookmarkID)
        {
            BizResultInfo result = new BizResultInfo();
            var bookmarkInfo = BizBookmarkInfo.LoadByID(bookmarkID);
            if(bookmarkInfo==null || bookmarkInfo.BookmarkInfoID==0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "书签数据有误，可能是数据库被怪兽吃掉了，也可能是你娃误操作给了一个错误的书签ID。建议刷新重试吧。";
                return Json(result);
            }

            var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            if(bookmarkInfo.UserInfoID == loginUID)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "不用收藏自己的书签哦。";
                return Json(result);
            }
            long folderID =  GetUserDefaultFolderID(loginUID);

            BizBookmarkInfo newBookmark = new BizBookmarkInfo() 
            {
                BookmarkName = bookmarkInfo.BookmarkName,
                CreateTime = DateTime.Now,
                Host = bookmarkInfo.Host,
                Href = bookmarkInfo.Href,
                UserInfoID = loginUID,
                HTML = bookmarkInfo.HTML,
                IElementJSON = bookmarkInfo.IElementJSON,
                UserWebFolderID = folderID,
            };
            newBookmark.HashCode = newBookmark.GetHashCode();
            newBookmark.Save();
            result.IsSuccess = true;
            return Json(result);
        }

        private static long GetUserDefaultFolderID(long loginUID)
        {
            long folderID = 0;
            var folderConfiguration = BizUserConfiguration.LoadByKey(loginUID, "UserDefaultFolder");
            if (folderConfiguration == null)
            {
                var userFolderList = BizUserWebFolder.LoadAllByUID(loginUID);
                if (userFolderList != null && userFolderList.Count > 0)
                {
                    var firstFolder = userFolderList.Where(folder => folder.ParentWebfolderID == 0);
                    if (firstFolder != null && firstFolder.Count() > 0)
                    {
                        NewFolderAndConfigurationHasParentFolderID(loginUID, ref folderID, ref folderConfiguration, firstFolder);
                    }
                    else
                    {
                        NewFolderAndConfigurationNoParentFolderID(loginUID, ref folderID, ref folderConfiguration);
                    }
                }else
                {
                    NewFolderAndConfigurationNoParentFolderID(loginUID, ref folderID, ref folderConfiguration);
                }


            }else
            {
                folderID =Convert.ToInt64(folderConfiguration.UserConfigurationValue);
            }


            return folderID;
        }

        #region GetUserDefaultFolderID 私有方法

        private static void NewFolderAndConfigurationHasParentFolderID(long loginUID, ref long folderID, ref BizUserConfiguration folderConfiguration,
            IEnumerable<BizUserWebFolder> firstFolder)
        {
            BizUserWebFolder newFolder = new BizUserWebFolder();
            newFolder.UserInfoID = loginUID;
            newFolder.WebFolderName = "默认书签夹";
            newFolder.ParentWebfolderID = firstFolder.FirstOrDefault().UserWebFolderID;
            newFolder.CreateTime = DateTime.Now;
            newFolder.IElementJSON = "";
            newFolder.Grade = 0;
            newFolder.IntroContent = "默认书签夹";
            newFolder.Save();

            folderID = newFolder.UserWebFolderID;

            folderConfiguration = new BizUserConfiguration();
            folderConfiguration.UserConfigurationKey = "UserDefaultFolder";
            folderConfiguration.UserConfigurationNo = 1;
            folderConfiguration.UserConfigurationValue = newFolder.UserWebFolderID.ToString();
            folderConfiguration.UserInfoID = loginUID;
            folderConfiguration.Description = "用户默认书签夹配置信息";
            folderConfiguration.Save();
        }

        private static void NewFolderAndConfigurationNoParentFolderID(long loginUID, ref long folderID, ref BizUserConfiguration folderConfiguration)
        {
            BizUserWebFolder newFolder = new BizUserWebFolder();
            newFolder.UserInfoID = loginUID;
            newFolder.WebFolderName = "默认书签夹";
            newFolder.ParentWebfolderID = 0;
            newFolder.CreateTime = DateTime.Now;
            newFolder.IElementJSON = "";
            newFolder.IntroContent = "默认书签夹";
            newFolder.Grade = 0;
            newFolder.Save();

            folderID = newFolder.UserWebFolderID;

            folderConfiguration = new BizUserConfiguration();
            folderConfiguration.UserConfigurationKey = "UserDefaultFolder";
            folderConfiguration.UserConfigurationNo = 1;
            folderConfiguration.UserConfigurationValue = newFolder.UserWebFolderID.ToString();
            folderConfiguration.UserInfoID = loginUID;
            folderConfiguration.Description = "用户默认书签夹配置信息";
            folderConfiguration.Save();
        }

        #endregion
    }
}
