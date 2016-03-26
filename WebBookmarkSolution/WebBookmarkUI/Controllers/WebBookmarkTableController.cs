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
            long uid = UILoginHelper.GetUIDInCookie(Request);
            if (folderID == 0)
            {
                var lst = BizUserWebFolder.LoadAllByUID(uid);
                if (lst != null)
                {
                    var firstFolder = lst.Where(folder => folder.ParentWebfolderID == 0);
                    if (firstFolder == null)
                        return View();
                    folderID = firstFolder.FirstOrDefault().UserWebFolderID;
                }
            }
            var folderInfo = BizUserWebFolder.LoadContainsChirdrenAndBookmark(folderID);
            var model = new UIWebFolderInfo(folderInfo);
            return View(model);
        }


        public ActionResult ShowFolderTable(long folderID)
        {
            long uid = UILoginHelper.GetUIDInCookie(Request);
            if (folderID == 0)
            {
                var lst = BizUserWebFolder.LoadAllByUID(uid);
                if (lst != null)
                {
                    var firstFolder = lst.Where(folder => folder.ParentWebfolderID == 0);
                    if (firstFolder == null)
                        return View();
                    folderID = firstFolder.FirstOrDefault().UserWebFolderID;
                }
            }
            var folderInfo = BizUserWebFolder.LoadContainsChirdrenAndBookmark(folderID);
            var model = new UIWebFolderInfo(folderInfo);
            return View("ShowFolderTable", model);
        }

        public ActionResult ShowAddFolderOrBookmarkView(long folderID)
        {
            long uid = UILoginHelper.GetUIDInCookie(Request);
            if (folderID == 0)
            {
                var lst = BizUserWebFolder.LoadAllByUID(uid);
                if (lst != null)
                {
                    var firstFolder = lst.Where(folder => folder.ParentWebfolderID == 0);
                    if (firstFolder == null)
                        return View();
                    folderID = firstFolder.FirstOrDefault().UserWebFolderID;
                }
            }
            var folderInfo = BizUserWebFolder.LoadContainsChirdrenAndBookmark(folderID);
            var model = new UIWebFolderInfo(folderInfo);
            return View("ShowAddFolderOrBookmarkView", model);
        }



        public ActionResult ConvertToUIWebFolderInfo(string strModel)
        {
            UIWebFolderInfo folderInfo = null;
            try
            {
                folderInfo = JsonConvert.DeserializeObject<UIWebFolderInfo>(strModel);
            }catch(Exception ex)
            {
                LogHelper.WriteException("UIWebFolderInfo反序列化失败", ex, new { Modle = strModel });
            }
            return Json(folderInfo);
        }



        public ActionResult ConvertToUIBookmarkInfo(string strModel)
        {
            UIBookmarkInfo bookmark = null;
            try
            {
                bookmark = JsonConvert.DeserializeObject<UIBookmarkInfo>(strModel);
            }
            catch (Exception ex)
            {
                LogHelper.WriteException("UIBookmarkInfo反序列化失败", ex, new { Modle = strModel });
            }
            return Json(bookmark);
        }


        public ActionResult AddBookmark(string name,string href,long folderID,string type,long infoID)
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

                    if(infoID!=0)
                    {
                        bookmark = BizBookmarkInfo.LoadByID(infoID);
                    }

                   
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
                    if(infoID!=0)
                    {
                        folder = BizUserWebFolder.LoadByID(infoID);
                    }

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


        public ActionResult DeleteBookmarkOrWebFolder(long infoID,string type)
        {
            BizResultInfo result = new BizResultInfo();
            try
            {
                if(type=="folder")
                {
                    result.IsSuccess = UserWebFolderBo.DeleteWebfolder(infoID);
                    result.SuccessMessage = "删除成功!";
                }
                else
                {
                    result.IsSuccess = BookmarkInfoBo.DeleteByBookmarkInfoID(infoID);
                    result.SuccessMessage = "删除成功!";
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteException("", ex, new { InfoID = infoID});
                result.IsSuccess =false;
                result.ErrorMessage = "操作失败，可能这就是命吧!要不你重试一下？";
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