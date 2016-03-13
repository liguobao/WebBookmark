using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using WebfolderBo.Model;

namespace WebfolderUI.Controllers
{
    public class WebfolderTableController : Controller
    {
        // GET: WebfolderTable
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ImportWebfolder()
        {
            return View();
        }

        

        public ActionResult ImportWebfolderToDB(string filePath)
        {
            BizResultInfo result = new BizResultInfo();

            if(string.IsNullOrEmpty(filePath))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "先上传文件呀。";
            }
            long uid = UILoginHelper.GetUIDInCookie(Request);


            Dictionary<IElement, IElement> dicWebfolderElement = new Dictionary<IElement, IElement>();
            Dictionary<IElement, List<IElement>> dicWebfolderNameToHrefList = new Dictionary<IElement, List<IElement>>();


            var path = Server.MapPath(filePath);
            var allText = System.IO.File.ReadAllText(path);
            if(!string.IsNullOrEmpty(allText))
            {
                allText = allText.Replace("\n", "").Replace("<p>", "");
                var tree = new HtmlParser().Parse(allText);
                if (tree != null)
                {
                    FillDictionaryWebFolderAndHrefInfo(tree.FirstElementChild, 
                        dicWebfolderElement, dicWebfolderNameToHrefList);
                }
                var count = dicWebfolderElement.Count;
                count = dicWebfolderNameToHrefList.Count;

            }


            return Json(result);
        }


       private void FillDictionaryWebFolderAndHrefInfo(IElement tree,
           Dictionary<IElement, IElement> dicWebfolderElement,
           Dictionary<IElement, List<IElement>> dicWebfolderNameToHrefList)
        {
            
            foreach (var one in tree.Children)
            {
               
                if (one.Children.Count() != 0)
                {
                    FillDictionaryWebFolderAndHrefInfo(one, dicWebfolderElement, dicWebfolderNameToHrefList);
                }
                else
                {
                    if (string.IsNullOrEmpty(one.TagName))
                        continue;
                    var tagName = one.TagName.ToUpper();
                    if (tagName == "H3")
                    {
                        if (dicWebfolderElement.ContainsKey(one))
                            continue;
                        dicWebfolderNameToHrefList.Add(one, tree.Children.ToList());
                        dicWebfolderElement.Add(one,one);
                    }
                    
                }
            }
        }

       private static void FillWebFolderAndHrefInfo(IElement tree,
            List<BizUserWebFolder> lstWebFolder, List<BizHrefInfo> lstHrefInfo)
        {
            var firstOne = tree.FirstElementChild;

            foreach (var one in tree.Children)
            {
                if (one.Children.Count() != 0)
                {
                    FillWebFolderAndHrefInfo(one,lstWebFolder,lstHrefInfo);
                }
                else
                {
                    if (string.IsNullOrEmpty(one.TagName))
                        continue;

                    var tagName = one.TagName.ToUpper();
                    if(tagName=="H3")
                    {
                        BizUserWebFolder bizWebfolder = new BizUserWebFolder();
                        bizWebfolder.CreateTime = DateTime.Now;
                        bizWebfolder.WebFolderName = one.TextContent;
                        lstWebFolder.Add(bizWebfolder);
                    }
                    else if(tagName =="A")
                    {
                        var oneInfo = (IHtmlAnchorElement)one;
                        var hrefInfo = new BizHrefInfo();
                        hrefInfo.Href = oneInfo.Href;
                        hrefInfo.Host = oneInfo.Host;
                        hrefInfo.ImportXML = one.InnerHtml;
                        hrefInfo.CreateTime = DateTime.Now;
                        lstHrefInfo.Add(hrefInfo);

                    }
                }
            }
        }

        private void GetWebFolderList(IElement tree, List<BizUserWebFolder> lstWebFolder,long uid)
        {
            foreach (var one in tree.Children)
            {
                if (one.Children.Count() != 0)
                {
                    GetWebFolderList(one, lstWebFolder,uid);
                }
                else
                {
                    if (string.IsNullOrEmpty(one.TagName))
                        continue;

                    var tagName = one.TagName.ToUpper();
                    if (tagName == "H3")
                    {
                        BizUserWebFolder bizWebfolder = new BizUserWebFolder();
                        bizWebfolder.CreateTime = DateTime.Now;
                        bizWebfolder.WebFolderName = one.TextContent;
                        bizWebfolder.UserInfoID = uid;
                        bizWebfolder.Visible =0;
                        bizWebfolder.ParentWebfolderID = 0;
                        lstWebFolder.Add(bizWebfolder);
                    }
                }
            }
        }



        public ActionResult UploadWebfolderFile()
        {
            var result = UploadFileHelper.UploadFileToUserImportFile(Request);
            if(result.IsSuccess)
            {
                BizUserWebFolderImportLog importLog = new BizUserWebFolderImportLog();
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