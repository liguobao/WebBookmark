using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Service;
using WebBookmarkUI.Models;

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
            var lstUserInfoModel = new List< UIUserInfo>();
            var lstModel = UserInfoBo.SearchUserList(nameOrEmail);
            if(lstModel!=null && lstModel.Count>0)
            {
                lstUserInfoModel.AddRange( lstModel.Select(model => new UIUserInfo()
                {
                    UserImagURL = model.UserImagURL,
                    UserEmail = model.UserEmail,
                    UserInfoComment = model.UserInfoComment,
                    UserName = model.UserName,
                    CreateTime = model.CreateTime,
                }));
            }
            return PartialView("ViewForSearchUser", lstUserInfoModel);
        } 

    }
}
