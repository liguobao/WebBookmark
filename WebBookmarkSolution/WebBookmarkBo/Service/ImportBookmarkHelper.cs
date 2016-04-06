using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkBo.Model;
using WebBookmarkBo.Service;
using WebBookmarkService;

namespace WebBookmarkBo.Service
{
    public class ImportBookmarkHelper
    {

        /// <summary>
        ///  遍历书签数据，构建bookmark和webfolder 对应关系，落地到库
        /// </summary>
        /// <param name="path">绝对地址</param>
        /// <param name="uid">用户UID</param>
        /// <returns></returns>
        public static BizResultInfo ImportBookmarkDataToDB(string path,long uid)
        {

            Dictionary<int, IElement> dicWebfolderElement = new Dictionary<int, IElement>();
            Dictionary<IElement, List<IElement>> dicWebfolderToBookmarkList = new Dictionary<IElement, List<IElement>>();
            Dictionary<int, BizUserWebFolder> dicHashcodeToModel = new Dictionary<int, BizUserWebFolder>();
            List<BizBookmarkInfo> lstBizHrefInfo = new List<BizBookmarkInfo>();

            if(!System.IO.File.Exists(path))
                return (new BizResultInfo() { IsSuccess = false, ErrorMessage = "文件不存在哦。" });

            var allText = System.IO.File.ReadAllText(path);
            if (string.IsNullOrEmpty(allText))
                return (new BizResultInfo() { IsSuccess = false, ErrorMessage = "书签文件是空的，换一个文件呀。" });

            try
            {
                allText = allText.Replace("\n", "").Replace("<p>", "");
                var tree = new HtmlParser().Parse(allText);

                FillDictionarydicWebfolderAndBookmarkList(tree.FirstElementChild,
                        dicWebfolderElement, dicWebfolderToBookmarkList);

                SaveWebFolderToDBAndFillHashModel(uid, dicWebfolderElement, ref dicHashcodeToModel);

                FillBookmarkModelAndWebfolderInfo(tree.FirstElementChild, uid, lstBizHrefInfo, dicHashcodeToModel);

                AsyncSaveBookmarkAndUpdataWebfolderToDB(dicHashcodeToModel, lstBizHrefInfo);
                MessageBo.CreateMessage(uid, MessageTypeEnum.ImportBookmarkSuccess, null);
            }
            catch (Exception ex)
            {
                MessageBo.CreateMessage(uid, MessageTypeEnum.ImportBookmarkFail, null);
                LogHelper.WriteException("遍历书签文件失败",ex,new {UserInfoID= uid,Path= path });
                return (new BizResultInfo() { IsSuccess = false, ErrorMessage = "发生了一些意外，反正你是看不懂的...." });
            }


            return (new BizResultInfo() { IsSuccess = true, SuccessMessage = "保存成功耶，你可以到别的地方玩了。" });
        }


        /// <summary>
        /// 遍历书签数据，构建bookmark和webfolder 对应关系，落地到库
        /// </summary>
        /// <param name="bookmarkText">书签数据</param>
        /// <param name="uid">用户UID</param>
        /// <returns></returns>
        public static BizResultInfo ImportBookmarkStringDataToDB(string bookmarkText,long uid)
        {
            Dictionary<int, IElement> dicWebfolderElement = new Dictionary<int, IElement>();
            Dictionary<IElement, List<IElement>> dicWebfolderToBookmarkList = new Dictionary<IElement, List<IElement>>();
            Dictionary<int, BizUserWebFolder> dicHashcodeToModel = new Dictionary<int, BizUserWebFolder>();
            List<BizBookmarkInfo> lstBizHrefInfo = new List<BizBookmarkInfo>();


            if (string.IsNullOrEmpty(bookmarkText))
                return (new BizResultInfo() { IsSuccess = false, ErrorMessage = "书签文件是空的，换一个文件呀。" });

            try
            {
                bookmarkText = bookmarkText.Replace("\n", "").Replace("<p>", "");
                var tree = new HtmlParser().Parse(bookmarkText);

                FillDictionarydicWebfolderAndBookmarkList(tree.FirstElementChild,
                        dicWebfolderElement, dicWebfolderToBookmarkList);

                SaveWebFolderToDBAndFillHashModel(uid, dicWebfolderElement, ref dicHashcodeToModel);

                FillBookmarkModelAndWebfolderInfo(tree.FirstElementChild, uid, lstBizHrefInfo, dicHashcodeToModel);

                AsyncSaveBookmarkAndUpdataWebfolderToDB(dicHashcodeToModel, lstBizHrefInfo);
            }
            catch (Exception ex)
            {
                LogHelper.WriteException("遍历书签文件失败", ex, new { UserInfoID = uid, BookmarkText = bookmarkText });

                return (new BizResultInfo() { IsSuccess = false, ErrorMessage = "发生了一些意外，反正你是看不懂的...." });
            }


            return (new BizResultInfo() { IsSuccess = true, SuccessMessage = "保存成功耶，你可以到别的地方玩了。" });
        }



        /// <summary>
        /// 保存书签和更新书签夹到数据库（异步）
        /// </summary>
        /// <param name="dicHashcodeToModel"></param>
        /// <param name="lstBizHrefInfo"></param>
        private static void AsyncSaveBookmarkAndUpdataWebfolderToDB(Dictionary<int, BizUserWebFolder> dicHashcodeToModel, List<BizBookmarkInfo> lstBizHrefInfo)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    UserWebFolderBo.BatchUpdateWebfolder(dicHashcodeToModel.Values.ToList());

                }
                catch (Exception ex)
                {
                    LogHelper.WriteException("BatchUpdateWebfolder Exception", ex,
                        new {DicHashcodeToModel = dicHashcodeToModel,});
                }

            });
            Task.Factory.StartNew(() =>
            {
                try
                {
                    BookmarkInfoBo.BatchSaveToDB(lstBizHrefInfo);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteException("BookmarkInfo BatchSaveToDB Exception", ex,
                       new
                       {  
                           BizHrefInfoList = lstBizHrefInfo
                       });
                }

            });
        }

        /// <summary>
        /// 书签夹数据落地到库，填充对应的Model数据以备下一步使用
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="dicWebBookmarkElement"></param>
        /// <param name="dicHashcodeToModel"></param>
        private static void SaveWebFolderToDBAndFillHashModel(long uid, Dictionary<int, IElement> dicWebBookmarkElement,
            ref Dictionary<int, BizUserWebFolder> dicHashcodeToModel)
        {
            List<BizUserWebFolder> lstWebfolder = new List<BizUserWebFolder>();

            foreach (var element in dicWebBookmarkElement.Values)
            {
                lstWebfolder.Add(new BizUserWebFolder()
                {
                    WebFolderName = element.TextContent,
                    UserInfoID = uid,
                    Grade = 0,
                    CreateTime = DateTime.Now,
                    IntroContent = "",
                    ParentWebfolderID = 0,
                    IElementJSON = element.OuterHtml,
                    IElementHashcode = element.GetHashCode(),
                });
            }

            var result = UserWebFolderBo.BatchAddUserWebfolder(lstWebfolder);
            if (result.IsSuccess)
            {
                dicHashcodeToModel = result.Target as Dictionary<int, BizUserWebFolder>;
            }


        }

        /// <summary>
        /// 遍历数据树，填充数据到Dictionary
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="dicWebfolderElement"></param>
        /// <param name="dicWebfolderToBookmarkList"></param>
        private static void FillDictionarydicWebfolderAndBookmarkList(IElement tree,
           Dictionary<int, IElement> dicWebfolderElement,
           Dictionary<IElement, List<IElement>> dicWebfolderToBookmarkList)
        {

            foreach (var one in tree.Children)
            {

                if (one.Children.Count() != 0)
                {
                    FillDictionarydicWebfolderAndBookmarkList(one, dicWebfolderElement, dicWebfolderToBookmarkList);
                }
                else
                {
                    if (string.IsNullOrEmpty(one.TagName))
                        continue;
                    var tagName = one.TagName.ToUpper();
                    if (tagName == "H3")
                    {
                        if (dicWebfolderElement.ContainsKey(one.GetHashCode()))
                            continue;
                        dicWebfolderToBookmarkList.Add(one, tree.Children.ToList());
                        dicWebfolderElement.Add(one.GetHashCode(), one);
                    }

                }
            }
        }

        /// <summary>
        /// 遍历数据树，构建书签和书签夹的对应关系
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="uid"></param>
        /// <param name="lstBookmarkInfo"></param>
        /// <param name="dicHashcodeToModel"></param>
        private static void FillBookmarkModelAndWebfolderInfo(IElement tree, long uid,
            List<BizBookmarkInfo> lstBookmarkInfo, Dictionary<int, BizUserWebFolder> dicHashcodeToModel)
        {
            foreach (var element in tree.Children)
            {
                if (element.Children.Count() != 0)
                {
                    FillBookmarkModelAndWebfolderInfo(element, uid, lstBookmarkInfo, dicHashcodeToModel);
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
                    else if (tagName == "A")
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
                            lstBookmarkInfo.Add(bizBookmarkInfo);
                        }
                    }
                }
            }
        }

      
    }
}
