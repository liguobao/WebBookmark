using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using WebBookmarkBo.Model;
using WebBookmarkService;
using WebBookmarkUI.Models;
using WebfolderBo.Service;
using WebfolderBo;

namespace WebBookmarkUI.Controllers
{
    public class WebBookmarkTableController : Controller
    {
        // GET: WebBookmarkTable
        public ActionResult Index(long folderID=0)
        {
            UIWebFolderInfo model = null;
            var uid = UILoginHelper.GetUIDInCookie(Request);
            if (uid != default(long))
            {
                var result = UserWebFolderBo.LoadWebfolderByUID(uid);
                if (!result.IsSuccess)
                    return View();
                model = new UIWebFolderInfo();
                var allWebFolderInfo = new List<UIWebFolderInfo>();
                foreach (var bizWebfolder in (result.Target as List<BizUserWebFolder>).OrderBy(folder => folder.ParentWebfolderID))
                {
                    UIWebFolderInfo uiModel = ToUIModel(bizWebfolder);
                    allWebFolderInfo.Add(uiModel);
                }
                model = allWebFolderInfo.Find(folder => folder.ParentWebfolderID == folderID);
            }
            return View(model);
        }

        private static UIWebFolderInfo ToUIModel(BizUserWebFolder bizWebfolder)
        {
            var uiModel = new UIWebFolderInfo();
            uiModel.ParentWebfolderID = bizWebfolder.ParentWebfolderID;
            uiModel.UserInfoID = bizWebfolder.UserInfoID;
            uiModel.UserWebFolderID = bizWebfolder.UserWebFolderID;
            uiModel.WebFolderName = bizWebfolder.WebFolderName;
            uiModel.Visible = bizWebfolder.Visible;
            //uiModel.IntroContent = bizWebfolder.IntroContent;
            //uiModel.IElementJSON = bizWebfolder.IElementJSON;
            uiModel.CreateTime = bizWebfolder.CreateTime;
            uiModel.ChildrenFolderList = new List<UIWebFolderInfo>();
            uiModel.UIBookmarkInfoList = new List<UIBookmarkInfo>();

            if (bizWebfolder.ChildrenFolderList!=null && bizWebfolder.ChildrenFolderList.Count!=0)
            {
                uiModel.ChildrenFolderList.AddRange(
                    bizWebfolder.ChildrenFolderList.
                    Select(childeren=>ToUIModel(childeren)));
            }

            if(bizWebfolder.BizBookmarkInfoList!=null && bizWebfolder.BizBookmarkInfoList.Count!=0)
            {
              

                foreach(var bookmark in bizWebfolder.BizBookmarkInfoList)
                {
                    var uiBookmarkInfo = new UIBookmarkInfo()
                    {
                        UserInfoID = bookmark.UserInfoID,
                        UserWebFolderID = bookmark.UserWebFolderID,
                        Host = bookmark.Host,
                        Href = bookmark.Href,
                        BookmarkName = bookmark.BookmarkName,
                        CreateTime = bookmark.CreateTime,
                        BookmarkInfoID = bookmark.BookmarkInfoID,
                    };

                    uiModel.UIBookmarkInfoList.Add(uiBookmarkInfo);
                } 
            }
            return uiModel;
        }

       

        public ActionResult AddBookmark(string name,string href,long folderID,string type)
        {
            BizResultInfo result = new BizResultInfo();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
            {
                result.ErrorMessage = "名称和类型必须填写呀。";
                result.IsSuccess = false;
                return Json(result);
            }

            try
            {
                if(type=="bookmark")
                {
                    BizBookmarkInfo bookmark = new BizBookmarkInfo();
                    bookmark.Href = href;
                    bookmark.BookmarkName = name;
                    bookmark.UserWebFolderID = folderID;
                    bookmark.CreateTime = DateTime.Now;
                    bookmark.Host = href.GetHost();
                    bookmark.UserInfoID = UILoginHelper.GetUIDInCookie(Request);
                    bookmark.Save();
                }else
                {
                    BizUserWebFolder folder = new BizUserWebFolder();
                    folder.UserInfoID = UILoginHelper.GetUIDInCookie(Request);
                    folder.WebFolderName = name;
                    folder.ParentWebfolderID = folderID;
                    folder.CreateTime = DateTime.Now;
                    folder.Save();
                
                }

               

                result.IsSuccess = true;
                result.SuccessMessage = "保存成功了耶，刷新一下页面就能看到了哦。";

            }catch(Exception ex)
            {
                result.IsSuccess = false;
                result.SuccessMessage = "保存失败，可能是数据库不开心了吧，重新保存试试。";
                LogHelper.WriteException("AddBookmark Exception", ex, new { Name = name,Href = href,FolderID =folderID });
            }
            return Json(result);

        }

        

        public ActionResult ImportWebBookmark()
        {
            return View();
        }

        public ActionResult ImportWebBookmarkToDB(string filePath)
        {
            BizResultInfo result = new BizResultInfo();

            if(string.IsNullOrEmpty(filePath))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "先上传文件呀。";
            }
            long uid = UILoginHelper.GetUIDInCookie(Request);

            var path = Server.MapPath(filePath);

            result = ImportBookmarkHelper.ImportBookmarkDataToDB(path,uid);


            return Json(new BizResultInfo() { IsSuccess = true,SuccessMessage="保存成功耶，你可以到别的地方玩了。" });
        }

      


        public ActionResult UploadWebBookmarkFile()
        {
            var result = UploadFileHelper.UploadFileToUserImportFile(Request);
            if(result.IsSuccess)
            {
                BizUserWebBookmarkImportLog importLog = new BizUserWebBookmarkImportLog();
                importLog.CreateTime = DateTime.Now;
                importLog.UserInfoID = UILoginHelper.GetUIDInCookie(Request);
                importLog.Path = result.ResultID;
                importLog.FileName = result.ResultID;
                importLog.Save();
            }

            return Json(result);
        }

        public FileResult PreView(string path)
        {
            return File(path, "text/html");
        }

    }
}