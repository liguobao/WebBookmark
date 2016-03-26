﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Service;
using WebBookmarkUI.Models;
using WebfolderBo.Service;

namespace WebBookmarkUI.Controllers
{
    public class FollowerController : Controller
    {
        //
        // GET: /Follower/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 用户所关注的人
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowUserFollow(long uid=0)
        {
            if(uid==0)//当前登录用户
            {
                uid = UILoginHelper.GetUIDInCookie(Request);
            }

            var dicFollower = UserRelationshipBo.GetByFollowUserID(uid);

            var dicLoginUserFollower = UserRelationshipBo.GetByFollowUserID(UILoginHelper.GetUIDInCookie(Request));

            var lstUserInfoModel = new List<SearchUserInfo>();
            var lstModel = UserInfoBo.GetListByUIDList(dicFollower.Keys.ToList());
            if (lstModel != null && lstModel.Count > 0)
            {
                lstUserInfoModel.AddRange(lstModel.Select(model => new SearchUserInfo()
                {
                    UserImagURL = model.UserImagURL,
                    UserEmail = model.UserEmail,
                    UserInfoComment = model.UserInfoComment,
                    UserName = model.UserName,
                    CreateTime = model.CreateTime,
                    UserInfoID = model.UserInfoID,
                    IsFollow = dicLoginUserFollower.ContainsKey(model.UserInfoID)
                }));
            }
            return PartialView("ShowUserFollow", lstUserInfoModel);
        }

        /// <summary>
        /// 关注用户的人
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowUserBeFollwed(long uid = 0)
        {

            if (uid == 0)//当前登录用户
            {
                uid = UILoginHelper.GetUIDInCookie(Request);
            }
            var dicLoginUserFollower = UserRelationshipBo.GetByFollowUserID(UILoginHelper.GetUIDInCookie(Request));

            var dicBeFollower = UserRelationshipBo.GetByBeFollwedUID(uid);

            var lstUserInfoModel = new List<SearchUserInfo>();
            var lstModel = UserInfoBo.GetListByUIDList(dicBeFollower.Keys.ToList());
            if (lstModel != null && lstModel.Count > 0)
            {
                lstUserInfoModel.AddRange(lstModel.Select(model => new SearchUserInfo()
                {
                    UserImagURL = model.UserImagURL,
                    UserEmail = model.UserEmail,
                    UserInfoComment = model.UserInfoComment,
                    UserName = model.UserName,
                    CreateTime = model.CreateTime,
                    UserInfoID = model.UserInfoID,
                    IsFollow = dicLoginUserFollower.ContainsKey(model.UserInfoID),
                }));
            }
            return PartialView("ShowUserBeFollwed", lstUserInfoModel);
        }
    }
}
