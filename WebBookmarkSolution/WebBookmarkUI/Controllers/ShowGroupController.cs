using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;
using WebBookmarkBo.Service;
using WebBookmarkUI.Models;

namespace WebBookmarkUI.Controllers
{
    public class ShowGroupController : Controller
    {
        /// <summary>
        /// 用户创建的群组
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ActionResult ShowUserCreateGroupList(long userID)
        {
            if (userID == 0)
                return View("ShowUserGroupList", null);
            List<UIGroupInfo> lstModel = null;
            var lstGroupInfo = BizGroupInfo.LoadByCreateUserID(userID);
            var userInfo = BizUserInfo.LoadByUserInfoID(userID);

            if (lstGroupInfo != null)
                lstModel = lstGroupInfo.Select(info => new UIGroupInfo()
                {
                    CreateTime = info.CreateTime,
                    CreateUesrID = info.CreateUesrID,
                    GroupInfoID = info.GroupInfoID,
                    GroupIntro = info.GroupIntro,
                    GroupName = info.GroupName
                }).ToList();
            return View("ShowUserCreateGroupList", lstModel);
        }

        /// <summary>
        /// 展示用户所在的群组（已通过审核的）
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ActionResult ShowUserGroupListAndPass(long userID)
        {
            if (userID == 0)
                return View("ShowUserGroupListAndPass", null);
            List<UIUserGroupInfo> lstUIUserGroupInfo = null;

            var lstGroupUser = BizGroupUser.LoadGroupUser(userID);



            if (lstGroupUser == null)
                return View("ShowUserGroupListAndPass", null);

            var lstGroupInfo = BizGroupInfo.LoadByGroupIDList(lstGroupUser.Select(model => model.GroupInfoID).ToList());
            var lstUserInfo = UserInfoBo.GetListByUIDList(lstGroupInfo.Select(model => model.CreateUesrID).Distinct().ToList());
            Dictionary<long, BizUserInfo> dicUserInfo = new Dictionary<long, BizUserInfo>();
            if (lstUserInfo != null)
            {
                dicUserInfo = lstUserInfo.ToDictionary(model => model.UserInfoID, model => model);
            }

            var dicGroupInfo = lstGroupInfo.Select(info => new UIGroupInfo()
            {
                CreateTime = info.CreateTime,
                CreateUesrID = info.CreateUesrID,
                GroupInfoID = info.GroupInfoID,
                GroupIntro = info.GroupIntro,
                GroupName = info.GroupName,
                CreateUesrInfo = ToUIUserInfo(info.CreateUesrID, dicUserInfo),
            }).ToDictionary(model => model.GroupInfoID, model => model);
            lstUIUserGroupInfo = lstGroupUser.Where(model=>model.IsPass==(int)ApplyStatus.Pass).Select(model => new UIUserGroupInfo()
            {
                GroupInfo = dicGroupInfo.ContainsKey(model.GroupInfoID) ? dicGroupInfo[model.GroupInfoID] : null,
                GroupInfoID = model.GroupInfoID,
                GroupUserID = model.GroupUserID,
                CreateTime = model.CreateTime,
                IsPass = model.IsPass,
                UserInfoID = model.UserInfoID,
            }).ToList();


            return View("ShowUserGroupListAndPass", lstUIUserGroupInfo);
        }



        private UIUserInfo ToUIUserInfo(long userID, Dictionary<long, BizUserInfo> dicUserInfo)
        {
            return dicUserInfo.ContainsKey(userID) ? new UIUserInfo()
            {
                UserInfoID = dicUserInfo[userID].UserInfoID,
                UserEmail = dicUserInfo[userID].UserEmail,
                UserName = dicUserInfo[userID].UserName,
                UserImagURL = dicUserInfo[userID].UserImagURL,
                UserInfoComment = dicUserInfo[userID].UserInfoComment,
            } : null;
        }

        


    }
}
