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
    public class GroupInfoController : Controller
    {
        //
        // GET: /MyGroup/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 展示用户创建的群组
        /// </summary>
        /// <param name="createUserID"></param>
        /// <returns></returns>
        public ActionResult ShowMyGroupList(long createUserID)
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
            return View("ShowMyGroupList", lstModel);
        }

        /// <summary>
        /// 保存群组信息
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="groupIntro"></param>
        /// <returns></returns>
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
            groupInfo.ObjectHascode = groupInfo.GetHashCode();
            groupInfo.Save();

            BizGroupUser groupUser = new BizGroupUser();
            groupUser.UserInfoID = uid;
            groupUser.GroupInfoID = groupInfo.GroupInfoID;
            groupUser.IsPass = (int)ApplyStatus.Pass;
            groupUser.CreateTime = DateTime.Now;
            groupUser.Save();
            result.IsSuccess = true;
            result.SuccessMessage = "创建成功！";
            return Json(result);

        }


        /// <summary>
        /// 修改群组信息
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="groupIntro"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 删除群组
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public ActionResult DeleteUserGroupInfo(long groupID)
        {
            BizResultInfo result = new BizResultInfo();

            result.IsSuccess = GroupInfoBo.DeleteGroupInfo(groupID);
            if(result.IsSuccess)
            {
                result.SuccessMessage = "删除成功！";
            }else
            {
                result.ErrorMessage = "可能是哪里出问题了吧...刷新一下再看看。";
            }

            return Json(result);
        }

        /// <summary>
        /// 展示用户的所有群组（包括未通过申请的）
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ActionResult ShowALLUserGroupList(long userID)
        {
            if (userID == 0)
                return View("ShowALLUserGroupList", null);
            List<UIUserGroupInfo> lstUIUserGroupInfo = null;

            var lstGroupUser = BizGroupUser.LoadGroupUser(userID);

            

            if (lstGroupUser == null)
                return View("ShowALLUserGroupList", null);

            var lstGroupInfo = BizGroupInfo.LoadByGroupIDList(lstGroupUser.Select(model => model.GroupInfoID).ToList());
            var lstUserInfo = UserInfoBo.GetListByUIDList(lstGroupInfo.Select(model => model.CreateUesrID).Distinct().ToList());
            Dictionary<long, BizUserInfo> dicUserInfo = new Dictionary<long, BizUserInfo>();
            if(lstUserInfo!=null)
            {
                dicUserInfo = lstUserInfo.ToDictionary(model=>model.UserInfoID,model=>model);
            }

            var dicGroupInfo = lstGroupInfo.Select(info => new UIGroupInfo()
            {
                CreateTime = info.CreateTime,
                CreateUesrID = info.CreateUesrID,
                GroupInfoID = info.GroupInfoID,
                GroupIntro = info.GroupIntro,
                GroupName = info.GroupName,
                CreateUesrInfo = ToUIUserInfo(info.CreateUesrID,dicUserInfo),
            }).ToDictionary(model=>model.GroupInfoID,model=>model);
            lstUIUserGroupInfo = lstGroupUser.Select(model => new UIUserGroupInfo() 
            {
                GroupInfo = dicGroupInfo.ContainsKey(model.GroupInfoID) ? dicGroupInfo[model.GroupInfoID]:null,
                GroupInfoID = model.GroupInfoID,
                GroupUserID = model.GroupUserID,
                CreateTime =model.CreateTime,
                IsPass = model.IsPass,
                UserInfoID = model.UserInfoID,
            }).ToList();


            return View("ShowALLUserGroupList", lstUIUserGroupInfo);
        }

        /// <summary>
        /// 用户已通过的群组
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ActionResult ShowUserGroupListHasPass(long userID)
        {
            if (userID == 0)
                return View("ShowALLUserGroupList", null);
            List<UIUserGroupInfo> lstUIUserGroupInfo = null;

            var lstGroupUser = BizGroupUser.LoadGroupUser(userID);



            if (lstGroupUser == null)
                return View("ShowALLUserGroupList", null);

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
            lstUIUserGroupInfo = lstGroupUser.Where(model => model.IsPass == (int)ApplyStatus.Pass).Select(model => new UIUserGroupInfo()
            {
                GroupInfo = dicGroupInfo.ContainsKey(model.GroupInfoID) ? dicGroupInfo[model.GroupInfoID] : null,
                GroupInfoID = model.GroupInfoID,
                GroupUserID = model.GroupUserID,
                CreateTime = model.CreateTime,
                IsPass = model.IsPass,
                UserInfoID = model.UserInfoID,
            }).ToList();


            return View("ShowALLUserGroupList", lstUIUserGroupInfo);
        }


        public ActionResult ShowUserGroupListNotPass(long userID)
        {
            if (userID == 0)
                return View("ShowALLUserGroupList", null);
            List<UIUserGroupInfo> lstUIUserGroupInfo = null;

            var lstGroupUser = BizGroupUser.LoadGroupUser(userID);



            if (lstGroupUser == null)
                return View("ShowALLUserGroupList", null);

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
            lstUIUserGroupInfo = lstGroupUser.Where(model => model.IsPass == (int)ApplyStatus.Waiting).Select(model => new UIUserGroupInfo()
            {
                GroupInfo = dicGroupInfo.ContainsKey(model.GroupInfoID) ? dicGroupInfo[model.GroupInfoID] : null,
                GroupInfoID = model.GroupInfoID,
                GroupUserID = model.GroupUserID,
                CreateTime = model.CreateTime,
                IsPass = model.IsPass,
                UserInfoID = model.UserInfoID,
            }).ToList();


            return View("ShowALLUserGroupList", lstUIUserGroupInfo);
        }


        /// <summary>
        /// 展示群组当前用户
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public ActionResult ShowGroupUserList(long groupID)
        {

            if (groupID == 0)
                return View("ShowGroupUserList", null);
            List<UIUserGroupInfo> lstUIUserGroupInfo = null;

            var lstGroupUser = BizGroupUser.LoadByGroupID(groupID);
            var groupInfo = BizGroupInfo.LoadByGroupID(groupID);

            var lstUID = lstGroupUser.Select(model => model.UserInfoID).ToList();
            lstUID.Add(groupInfo.CreateUesrID);
            var lstUserInfo = UserInfoBo.GetListByUIDList(lstUID.Distinct().ToList());
            Dictionary<long, BizUserInfo> dicUserInfo = new Dictionary<long, BizUserInfo>();
            if (lstUserInfo != null)
            {
                dicUserInfo = lstUserInfo.ToDictionary(model => model.UserInfoID, model => model);
            }


            lstUIUserGroupInfo = lstGroupUser.Where(model => model.IsPass == (int)ApplyStatus.Pass).Select(model => new UIUserGroupInfo()
            {
                GroupInfo = new UIGroupInfo() 
                {
                    CreateTime = groupInfo.CreateTime,
                    CreateUesrID = groupInfo.CreateUesrID,
                    CreateUesrInfo = ToUIUserInfo(groupInfo.CreateUesrID,dicUserInfo),
                    GroupInfoID = groupInfo.GroupInfoID,
                    GroupName = groupInfo.GroupName,
                },
                GroupInfoID = model.GroupInfoID,
                GroupUserID = model.GroupUserID,
                CreateTime = model.CreateTime,
                IsPass = model.IsPass,
                UserInfoID = model.UserInfoID,
                GroupUserInfo = ToUIUserInfo(model.UserInfoID,dicUserInfo),
            }).ToList();

            return View("ShowGroupUserList", lstUIUserGroupInfo);

        }

        /// <summary>
        /// 未通过审核的群组用户
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public ActionResult ShowGroupUserListNotPass(long groupID)
        {

            if (groupID == 0)
                return View("ShowGroupUserList", null);
            List<UIUserGroupInfo> lstUIUserGroupInfo = null;

            var lstGroupUser = BizGroupUser.LoadByGroupID(groupID);
            var groupInfo = BizGroupInfo.LoadByGroupID(groupID);

            var lstUID = lstGroupUser.Select(model => model.UserInfoID).ToList();
            lstUID.Add(groupInfo.CreateUesrID);
            var lstUserInfo = UserInfoBo.GetListByUIDList(lstUID.Distinct().ToList());
            Dictionary<long, BizUserInfo> dicUserInfo = new Dictionary<long, BizUserInfo>();
            if (lstUserInfo != null)
            {
                dicUserInfo = lstUserInfo.ToDictionary(model => model.UserInfoID, model => model);
            }


            lstUIUserGroupInfo = lstGroupUser.Where(model => model.IsPass == (int)ApplyStatus.Waiting).Select(model => new UIUserGroupInfo()
            {
                GroupInfo = new UIGroupInfo()
                {
                    CreateTime = groupInfo.CreateTime,
                    CreateUesrID = groupInfo.CreateUesrID,
                    CreateUesrInfo = ToUIUserInfo(groupInfo.CreateUesrID, dicUserInfo),
                    GroupInfoID = groupInfo.GroupInfoID,
                    GroupName = groupInfo.GroupName,
                },
                GroupInfoID = model.GroupInfoID,
                GroupUserID = model.GroupUserID,
                CreateTime = model.CreateTime,
                IsPass = model.IsPass,
                UserInfoID = model.UserInfoID,
                GroupUserInfo = ToUIUserInfo(model.UserInfoID, dicUserInfo),
            }).ToList();

            return View("ShowGroupUserList", lstUIUserGroupInfo);

        }


        /// <summary>
        /// 群组详细信息
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public ActionResult ShowGroupDetail(long groupID)
        {
            UIGroupInfo groupInfo = null;
            if (groupID == 0)
                return View(groupInfo);
            var bizGroupInfo = BizGroupInfo.LoadByGroupID(groupID);
            if(bizGroupInfo==null)
                return View(groupInfo);
            var createUser = BizUserInfo.LoadByUserInfoID(bizGroupInfo.CreateUesrID);
            groupInfo = new UIGroupInfo()
            {
                GroupName = bizGroupInfo.GroupName,
                GroupInfoID = bizGroupInfo.GroupInfoID,
                GroupIntro = bizGroupInfo.GroupIntro,
                CreateTime = bizGroupInfo.CreateTime,
                CreateUesrInfo = new UIUserInfo()
                {
                    UserEmail = createUser.UserEmail,
                    UserName = createUser.UserName,
                    UserImagURL = createUser.UserImagURL,
                    UserInfoComment = createUser.UserInfoComment,
                },
                CreateUesrID = bizGroupInfo.CreateUesrID,
            };
            return View(groupInfo);
        }


        public static UIUserInfo ToUIUserInfo(long userID ,Dictionary<long, BizUserInfo> dicUserInfo)
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


        /// <summary>
        /// 申请加入群组
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public ActionResult ApplyToGroup(long groupID)
        {
            BizResultInfo result = new BizResultInfo();
            if (groupID == 0 || BizGroupInfo.LoadByGroupID(groupID)==null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "群组ID不能为空呀，这个数据肯定有问题...不要逗我玩吧。";
                return Json(result);
            }
            var lstGroupUser = BizGroupUser.LoadGroupUser(UILoginHelper.GetUIDInCookie(Request));
            if(lstGroupUser!=null && lstGroupUser.Any(model=>model.GroupInfoID == groupID))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "已经申请加入了，再等等吧....";
                return Json(result);
            }


            BizGroupUser groupUser = new BizGroupUser();
            groupUser.GroupInfoID = groupID;
            groupUser.IsPass = (int)ApplyStatus.Waiting;
            groupUser.CreateTime = DateTime.Now;
            groupUser.UserInfoID = UILoginHelper.GetUIDInCookie(Request);
            groupUser.Save();

            result.IsSuccess = true;
            result.SuccessMessage = "申请成功....";
          

            return Json(result);


        }

        /// <summary>
        /// 通过申请
        /// </summary>
        /// <param name="groupUserID"></param>
        /// <returns></returns>
        public ActionResult PassGroupUser(long groupUserID)
        {
            BizResultInfo result = new BizResultInfo();
           
            var bizModel = BizGroupUser.LoadByGroupUserID(groupUserID);
             if (groupUserID == 0 || bizModel==null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "取不到这个数据啊呀...不要逗我玩吧。";
                return Json(result);
            }
             bizModel.IsPass = (int)ApplyStatus.Pass;
             bizModel.Save();

             result.IsSuccess = true;
             result.SuccessMessage = "审核成功！";
             return Json(result );
            
        }

        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="groupUserID"></param>
        /// <returns></returns>
        public ActionResult RemoveGroupUser(long groupUserID)
        {
            BizResultInfo result = new BizResultInfo();
          

            var bizModel = BizGroupUser.LoadByGroupUserID(groupUserID);
            if (groupUserID == 0 || bizModel == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "取不到这个数据啊呀...不要逗我玩吧。";
                return Json(result);
            }

            if (UILoginHelper.GetUIDInCookie(Request) == bizModel.UserInfoID)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "不允许移除自己....";
                return Json(result);
            }
            bizModel.IsPass = (int)ApplyStatus.Remove;
            bizModel.Save();
            result.IsSuccess = true;
            return Json(result);
        }
    }
}
