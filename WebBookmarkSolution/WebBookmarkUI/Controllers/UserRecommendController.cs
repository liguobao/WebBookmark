using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Service;
using WebBookmarkUI.Models;

namespace WebBookmarkUI.Controllers
{
    public class UserRecommendController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            var loginUid = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            var dicBeFollowerID = UserRelationshipBo.GetByFollowUserID(loginUid);
            var lstUserID = RecommendBo.GetRecommendUserIDListDependHost(loginUid);

            var lstUserInfoModel = new List<SearchUserInfo>();
            var lstModel = UserInfoBo.GetListByUIDList(lstUserID);
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
                    IsFollow = dicBeFollowerID.ContainsKey(model.UserInfoID)
                }));
            }
            return PartialView("Index", lstUserInfoModel);
        }

    }
}
