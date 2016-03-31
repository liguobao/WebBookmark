using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;
using WebBookmarkBo.Service;
using WebBookmarkUI.Models;
using WebfolderBo.Model;
using WebfolderBo.Service;

namespace WebBookmarkUI.Controllers
{
    public class InterestRecommendController : Controller
    {
        //
        // GET: /InterestRecommend/

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SearchUser(string nameOrEmail)
        {
            var dicBeFollowerID = UserRelationshipBo.GetByFollowUserID(UILoginHelper.GetUIDInCookie(Request));
            
            var lstUserInfoModel = new List<SearchUserInfo>();
            var lstModel = UserInfoBo.SearchUserList(nameOrEmail);
            if(lstModel!=null && lstModel.Count>0)
            {
                lstUserInfoModel.AddRange(lstModel.Select(model => new SearchUserInfo()
                {
                    UserImagURL = model.UserImagURL,
                    UserEmail = model.UserEmail,
                    UserInfoComment = model.UserInfoComment,
                    UserName = model.UserName,
                    CreateTime = model.CreateTime,
                    UserInfoID = model.UserInfoID,
                    IsFollow = dicBeFollowerID.ContainsKey(model.UserInfoID)
                }));
            }
            return PartialView("SearchUser", lstUserInfoModel);
        } 

        public ActionResult FollowUser(long beFollowUserID)
        {
            BizResultInfo result = new BizResultInfo();
            if(beFollowUserID==0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "关注者ID为空，这是一条垃圾数据呀。";
                return Json(result);
            }
            long userID = UILoginHelper.GetUIDInCookie(Request);
            var status = UserRelationshipBo.CheckFollowStatus(beFollowUserID,userID);

            if(status)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "您已经关注了此用户，不能重复关注。";
                return Json(result);
            }

            var userRelationship = new BizUserRelationship() 
            {
                BeFollwedUID = beFollowUserID,
                FollowerID = userID,
                CreateTime = DateTime.Now,
            };
            userRelationship.Save();
            result.IsSuccess = true;

            return Json(result);


        }

        public ActionResult UnFollowUser(long beFollowUserID)
        {
            BizResultInfo result = new BizResultInfo();
            if (beFollowUserID == 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "ID为空，这是一条垃圾数据呀。";
                return Json(result);
            }
            long userID = UILoginHelper.GetUIDInCookie(Request);
            result.IsSuccess = UserRelationshipBo.UnFollowUser(beFollowUserID, userID);
            return Json(result);
        }


        public ActionResult ShowUserInfo(long showUserInfoID)
        {
            UIShowUserInfo showUserInfo = null;
            var model = BizUserInfo.LoadByUserInfoID(showUserInfoID);

            if(showUserInfoID==0 || model==null)
                return PartialView("ShowUserInfo", showUserInfo);

            var uid = UILoginHelper.GetUIDInCookie(Request);
            var dicBeFollowerID = UserRelationshipBo.GetByFollowUserID(uid);
            showUserInfo = new UIShowUserInfo() 
            {
                UserImagURL = model.UserImagURL,
                UserEmail = model.UserEmail,
                UserInfoComment = model.UserInfoComment,
                UserName = model.UserName,
                CreateTime = model.CreateTime,
                UserInfoID = model.UserInfoID,
                IsFollow = dicBeFollowerID.ContainsKey(model.UserInfoID)
            };

            return PartialView("ShowUserInfo", showUserInfo);
        }
       
        public ActionResult ShowUserFolder(long showUserInfoID, long folderID = 0)
        {
            UIWebFolderInfo model = null;

            if (showUserInfoID != default(long))
            {
                if(folderID==0)
                {
                    var lst = BizUserWebFolder.LoadAllByUID(showUserInfoID);
                    if(lst!=null)
                    {
                        var firstFolder = lst.Where(folder=>folder.ParentWebfolderID==0);
                        if (firstFolder == null || firstFolder.Count()==0)
                            return View("ShowUserFolder", model);

                        folderID = firstFolder.FirstOrDefault().UserWebFolderID;
                    }
                }
               var folderInfo = BizUserWebFolder.LoadContainsChirdrenAndBookmark(folderID);
               model = new UIWebFolderInfo(folderInfo);
            }
            return View("ShowUserFolder", model);
        }


    }
}
