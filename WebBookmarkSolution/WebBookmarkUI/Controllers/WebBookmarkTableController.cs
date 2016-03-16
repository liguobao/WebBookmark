using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using WebBookmarkBo.Model;
using WebfolderBo.Service;

namespace WebBookmarkUI.Controllers
{
    public class WebBookmarkTableController : Controller
    {
        // GET: WebBookmarkTable
        public ActionResult Index()
        {
            return View();
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


            Dictionary<int, IElement> dicWebBookmarkElement = new Dictionary<int, IElement>();
            Dictionary<IElement, List<IElement>> dicWebBookmarkNameToHrefList = new Dictionary<IElement, List<IElement>>();
            Dictionary<int, BizUserWebFolder> dicHashcodeToModel = new Dictionary<int, BizUserWebFolder>();

            var path = Server.MapPath(filePath);
            var allText = System.IO.File.ReadAllText(path);
            if(!string.IsNullOrEmpty(allText))
            {
                allText = allText.Replace("\n", "").Replace("<p>", "");
                var tree = new HtmlParser().Parse(allText);
                if (tree != null)
                {
                    FillDictionaryWebBookmarkAndHrefInfo(tree.FirstElementChild,
                        dicWebBookmarkElement, dicWebBookmarkNameToHrefList);
                }
                result = SaveToDBAndFillHashModel(uid, dicWebBookmarkElement, ref dicHashcodeToModel);

                List<BizBookmarkInfo> lstBizHrefInfo = new List<BizBookmarkInfo>();

                FillHrefListAndWebfolderInfo(tree.FirstElementChild,uid,lstBizHrefInfo,dicHashcodeToModel);

                Task.Factory.StartNew(()=> 
                {
                    try
                    {
                        UserWebFolderBo.BatchUpdateWebfolder(dicHashcodeToModel.Values.ToList());
                    }
                    catch(Exception ex)
                    {
                       
                    }

                });
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        BizBookmarkInfoBo.BatchSaveToDB(lstBizHrefInfo);
                    }
                    catch (Exception ex)
                    {

                    }

                });
            }

            return Json(new BizResultInfo() { IsSuccess = true,SuccessMessage="保存成功耶，你可以到别的地方玩了。" });
        }

        private static BizResultInfo SaveToDBAndFillHashModel(long uid, Dictionary<int, IElement> dicWebBookmarkElement,
            ref Dictionary<int, BizUserWebFolder> dicHashcodeToModel)
        {
            BizResultInfo result;
            List<BizUserWebFolder> lstWebfolder = new List<BizUserWebFolder>();

            foreach (var element in dicWebBookmarkElement.Values)
            {
                lstWebfolder.Add(new BizUserWebFolder()
                {
                    WebFolderName = element.TextContent,
                    UserInfoID = uid,
                    Visible = 0,
                    CreateTime = DateTime.Now,
                    IntroContent = "",
                    ParentWebfolderID = 0,
                    IElementJSON = element.OuterHtml,
                    IElementHashcode = element.GetHashCode(),
                });
            }

            result = UserWebFolderBo.BatchAddUserWebfolder(lstWebfolder);
            if (result.IsSuccess)
            {
                dicHashcodeToModel = result.Target as Dictionary<int, BizUserWebFolder>;
            }

            return result;
        }

        private void FillDictionaryWebBookmarkAndHrefInfo(IElement tree,
           Dictionary<int, IElement> dicWebBookmarkElement,
           Dictionary<IElement, List<IElement>> dicWebBookmarkNameToHrefList)
        {
            
            foreach (var one in tree.Children)
            {
               
                if (one.Children.Count() != 0)
                {
                    FillDictionaryWebBookmarkAndHrefInfo(one, dicWebBookmarkElement, dicWebBookmarkNameToHrefList);
                }
                else
                {
                    if (string.IsNullOrEmpty(one.TagName))
                        continue;
                    var tagName = one.TagName.ToUpper();
                    if (tagName == "H3")
                    {
                        if (dicWebBookmarkElement.ContainsKey(one.GetHashCode()))
                            continue;
                        dicWebBookmarkNameToHrefList.Add(one, tree.Children.ToList());
                        dicWebBookmarkElement.Add(one.GetHashCode(), one);
                    }
                    
                }
            }
        }

       private static void FillWebBookmarkAndHrefInfo(IElement tree,
            List<BizUserWebFolder> lstWebBookmark, List<BizBookmarkInfo> lstHrefInfo)
        {
            var firstOne = tree.FirstElementChild;

            foreach (var one in tree.Children)
            {
                if (one.Children.Count() != 0)
                {
                    FillWebBookmarkAndHrefInfo(one,lstWebBookmark,lstHrefInfo);
                }
                else
                {
                    if (string.IsNullOrEmpty(one.TagName))
                        continue;

                    var tagName = one.TagName.ToUpper();
                    if(tagName=="H3")
                    {
                        BizUserWebFolder bizWebBookmark = new BizUserWebFolder();
                        bizWebBookmark.CreateTime = DateTime.Now;
                        bizWebBookmark.WebFolderName = one.TextContent;
                        lstWebBookmark.Add(bizWebBookmark);
                    }
                    else if(tagName =="A")
                    {
                        var oneInfo = (IHtmlAnchorElement)one;
                        var hrefInfo = new BizBookmarkInfo();
                        hrefInfo.Href = oneInfo.Href;
                        hrefInfo.Host = oneInfo.Host;
                        hrefInfo.IElementJSON = one.InnerHtml;
                        hrefInfo.CreateTime = DateTime.Now;
                        lstHrefInfo.Add(hrefInfo);

                    }
                }
            }
        }

        private void FillHrefListAndWebfolderInfo(IElement tree,long uid,
            List<BizBookmarkInfo> lstBizHrefInfo, Dictionary<int, BizUserWebFolder> dicHashcodeToModel)
        {
            foreach (var element in tree.Children)
            {
                if (element.Children.Count() != 0)
                {
                    FillHrefListAndWebfolderInfo(element, uid, lstBizHrefInfo, dicHashcodeToModel);
                }
                else
                {
                    if (string.IsNullOrEmpty(element.TagName))
                        continue;

                    var tagName = element.TagName.ToUpper();
                   

                    if (tagName == "H3")
                    {
                        var parentHashcode = element.ParentElement.ParentElement.ParentElement.FirstElementChild.GetHashCode();
                        if (dicHashcodeToModel.ContainsKey(parentHashcode) && dicHashcodeToModel.ContainsKey(element.GetHashCode()))
                        {
                            dicHashcodeToModel[element.GetHashCode()].ParentWebfolderID
                                = dicHashcodeToModel[parentHashcode].UserWebFolderID;
                        }

                    }
                    else if(tagName == "A")
                    {
                        var parentHashcode = element.ParentElement.ParentElement.ParentElement.FirstElementChild.GetHashCode();
                        if (dicHashcodeToModel.ContainsKey(parentHashcode))
                        {
                            var hrefInfo = element as IHtmlAnchorElement;
                            if (hrefInfo == null)
                                continue;
                            var bizUserWebFolder = dicHashcodeToModel[parentHashcode];
                            var bizBookmarkInfo = new BizBookmarkInfo();
                            bizBookmarkInfo.BookmarkName = hrefInfo.Text;
                            bizBookmarkInfo.UserInfoID = uid;
                            bizBookmarkInfo.Host = hrefInfo.Host;
                            bizBookmarkInfo.Href = hrefInfo.Href;
                            bizBookmarkInfo.IElementJSON = hrefInfo.OuterHtml;
                            bizBookmarkInfo.UserWebFolderID = bizUserWebFolder.UserWebFolderID;
                            bizBookmarkInfo.CreateTime = DateTime.Now;
                            lstBizHrefInfo.Add(bizBookmarkInfo);
                        }
                    }
                }
            }
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