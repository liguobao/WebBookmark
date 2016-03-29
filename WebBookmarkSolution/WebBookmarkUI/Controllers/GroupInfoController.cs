using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;
using WebBookmarkUI.Models;
using WebfolderBo.Model;

namespace WebBookmarkUI.Controllers
{
    public class GroupInfoController : Controller
    {
        //
        // GET: /MyGroup/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowUserGroupList(long createUserID)
        {
            if(createUserID==0)
                return View("ShowUserGroupList",null);
             List< UIGroupInfo> lstModel = null;
            var lstGroupInfo = BizGroupInfo.LoadByCreateUserID(createUserID);
            var userInfo = BizUserInfo.LoadByUserInfoID(createUserID);

            if (lstGroupInfo != null)
                lstModel = lstGroupInfo.Select(info => new UIGroupInfo()
                {
                    CreateTime = info.CreateTime,
                    CreateUesrID = info.CreateUesrID,
                    GroupInfoID = info.GroupInfoID,
                    GroupIntro = info.GroupIntro,
                    GroupName = info.GroupName
                }).ToList();
            return View("ShowUserGroupList", lstModel);
        }


        public ActionResult SaveUserGroupInfo(string groupName,string groupIntro)
        {
            long uid = UILoginHelper.GetUIDInCookie(Request);
            BizResultInfo result = new BizResultInfo();

            if(string.IsNullOrEmpty(groupName))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "群组名称不能为空呀....";
                return Json(result);
            }
            BizGroupInfo groupInfo = new BizGroupInfo();
            groupInfo.CreateTime = DateTime.Now;
            groupInfo.CreateUesrID = uid;
            groupInfo.GroupName = groupName;
            groupInfo.GroupIntro = !string.IsNullOrEmpty(groupIntro) ? groupIntro : "";
            groupInfo.Save();



            result.IsSuccess = true;
            result.SuccessMessage = "创建成功！";
            return Json(result);

        }


        public ActionResult ModifyUserGroupInfo(string groupName, string groupIntro,long groupID)
        {
            long uid = UILoginHelper.GetUIDInCookie(Request);
            BizResultInfo result = new BizResultInfo();

            if (string.IsNullOrEmpty(groupName))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "群组名称不能为空呀....";
                return Json(result);
            }


            if (groupID==0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "群组ID不能为空呀,这个数据目测有问题....";
                return Json(result);
            }

            BizGroupInfo groupInfo = BizGroupInfo.LoadByGroupID(groupID);
            if (groupInfo!=null)
            {
                groupInfo.GroupName = groupName;
                groupInfo.GroupIntro = !string.IsNullOrEmpty(groupIntro) ? groupIntro : "";
                groupInfo.Save();
            }

            result.IsSuccess = true;
            result.SuccessMessage = "保存成功！";
            return Json(result);

        }


        public ActionResult DeleteUserGroupInfo(long groupID)
        {
            BizResultInfo result = new BizResultInfo();




            return Json(result);
        }

    }
}
