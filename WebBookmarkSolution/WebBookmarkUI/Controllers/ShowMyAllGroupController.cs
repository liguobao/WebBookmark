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
    public class ShowMyAllGroupController : Controller
    {
        //
        // GET: /ShowMyHasPassGroup/

        public ActionResult Index()
        {
            var userID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            List<UIUserGroupInfo> lstUIUserGroupInfo = null;

            var lstGroupUser = BizGroupUser.LoadGroupUser(userID);



            if (lstGroupUser == null)
                return View();

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
                CreateUesrInfo = GroupInfoController.ToUIUserInfo(info.CreateUesrID, dicUserInfo),
            }).ToDictionary(model => model.GroupInfoID, model => model);
            lstUIUserGroupInfo = lstGroupUser.Select(model => new UIUserGroupInfo()
            {
                GroupInfo = dicGroupInfo.ContainsKey(model.GroupInfoID) ? dicGroupInfo[model.GroupInfoID] : null,
                GroupInfoID = model.GroupInfoID,
                GroupUserID = model.GroupUserID,
                CreateTime = model.CreateTime,
                IsPass = model.IsPass,
                UserInfoID = model.UserInfoID,
            }).ToList();


            return View(lstUIUserGroupInfo);
        }

        public ActionResult QuitGroup(long groupUserID)
        {
            BizResultInfo result = new BizResultInfo();


            var bizModel = BizGroupUser.LoadByGroupUserID(groupUserID);
            if (groupUserID == 0 || bizModel == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "取不到这个数据啊呀...不要逗我玩吧。";
                return Json(result);
            }

            var groupInfo = BizGroupInfo.LoadByGroupID(bizModel.GroupInfoID);
            if (UILoginHelper.GetUIDFromHttpContext(HttpContext) == groupInfo.CreateUesrID)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "不允许移除自己....";
                return Json(result);
            }
            bizModel.IsPass = (int)ApplyStatus.Quit;
            bizModel.Save();
            result.IsSuccess = true;
            return Json(result);
        }


        public ActionResult AddToGroupAgain(long groupUserID)
        {
            BizResultInfo result = new BizResultInfo();


            var bizModel = BizGroupUser.LoadByGroupUserID(groupUserID);
            if (groupUserID == 0 || bizModel == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "取不到这个数据啊呀...不要逗我玩吧。";
                return Json(result);
            }
            bizModel.IsPass = (int)ApplyStatus.Waiting;
            bizModel.Save();
            result.IsSuccess = true;
            return Json(result);
        }

    }
}
