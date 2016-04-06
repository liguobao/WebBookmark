using System;
using System.Collections.Generic;
using System.Linq;
using WebBookmarkBo.Model;
using WebBookmarkService;
using WebBookmarkService.DAL;

namespace WebBookmarkBo.Service
{
    public class UserWebFolderBo
    {
        private static UserWebFolderDAL webFolderDAL = new UserWebFolderDAL();

        public static BizResultInfo BatchAddUserWebfolder(List<BizUserWebFolder> lstBizWebfolder)
        {
            BizResultInfo result = new BizResultInfo();
            if(lstBizWebfolder == null || lstBizWebfolder.Count==0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "插入数据不能为空。";
            }
            var lstModel = lstBizWebfolder.Select(biz=>biz.ToModel()).ToList();  
            var isSuccess = webFolderDAL.BatchAdd(lstModel);
            if(isSuccess)
            {
                result.IsSuccess = isSuccess;
                result.SuccessMessage = "插入成功。";
                Dictionary<int, int> dicHash = new Dictionary<int, int>();
                foreach(var w in lstBizWebfolder)
                {
                    if (!dicHash.ContainsKey(w.IElementHashcode))
                        dicHash.Add(w.IElementHashcode,w.IElementHashcode);
                }
                var uid = lstBizWebfolder.FirstOrDefault().UserInfoID;
                var lstDataInfo = BizUserWebFolder.LoadAllByUID(uid);

                var dicDataInfo = lstDataInfo.Where(datainfo => dicHash.ContainsKey(datainfo.IElementHashcode)).ToDictionary(model=>model.IElementHashcode,model=>model);
                result.Target = dicDataInfo;

            }else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "插入数据失败，可能是外星人劫持了数据库了，也可能是海底电缆被可爱的鲨鱼咬断了....反正重试一下就是。";
            }

            return result;
        } 


        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="lstBizWebfolder"></param>
        /// <returns></returns>
        public static BizResultInfo BatchUpdateWebfolder(List<BizUserWebFolder> lstBizWebfolder)
        {
            BizResultInfo result = new BizResultInfo();
            if (lstBizWebfolder == null || lstBizWebfolder.Count == 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "插入数据不能为空。";
            }
            var lstModel = lstBizWebfolder.Select(biz => biz.ToModel()).ToList();
            var isSuccess = webFolderDAL.BatchUpdata(lstModel);
            result.IsSuccess = isSuccess;
            if (isSuccess)
            {
                result.SuccessMessage = "更新成功！";
            }else
            {
                result.ErrorMessage = "更新失败，可能是海底光纤断了...待会再试试咯。";
            }

            return result;
        }

        /// <summary>
        /// 加载书签夹数据
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static BizResultInfo LoadWebfolderByUID(long uid)
        {
            var result = new BizResultInfo();

            try
            {
                var lstWebFolder = BizUserWebFolder.LoadAllByUID(uid);
                var lstBookmark = BookmarkInfoBo.LoadByUID(uid);
                Dictionary<long, List<BizBookmarkInfo>> dicWebfolderIDToBookmarkList = new Dictionary<long, List<BizBookmarkInfo>>();
                Dictionary<long, List<BizUserWebFolder>> dicParentWebfolderIDToWebfolder = new Dictionary<long, List<BizUserWebFolder>>();

                FillDictionary(lstWebFolder, lstBookmark, dicWebfolderIDToBookmarkList, dicParentWebfolderIDToWebfolder);

                foreach (var webfolder in lstWebFolder)
                {
                    if (dicWebfolderIDToBookmarkList.ContainsKey(webfolder.UserWebFolderID))
                    {
                        webfolder.BizBookmarkInfoList = dicWebfolderIDToBookmarkList[webfolder.UserWebFolderID];
                    }
                    if (dicParentWebfolderIDToWebfolder.ContainsKey(webfolder.UserWebFolderID))
                    {
                        webfolder.ChildrenFolderList = dicParentWebfolderIDToWebfolder[webfolder.UserWebFolderID];
                    }
                }

                result.IsSuccess = true;
                result.Target = lstWebFolder;

            }catch(Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "加载数据失败了，可能是网络挂了，可能这就是命吧。";
                result.Target = null;
                LogHelper.WriteException("LoadWebfolderByUID Exception", ex,new {UID =uid });
            }

            return result;
        }

        


        private static void FillDictionary(List<BizUserWebFolder> lstWebFolder, List<BizBookmarkInfo> lstBookmark, Dictionary<long, List<BizBookmarkInfo>> dicWebfolderIDToBookmarkList, Dictionary<long, List<BizUserWebFolder>> dicParentWebfolderIDToWebfolder)
        {
            if (lstBookmark != null)
            {
                foreach (var bookmark in lstBookmark)
                {
                    if (dicWebfolderIDToBookmarkList.ContainsKey(bookmark.UserWebFolderID))
                        dicWebfolderIDToBookmarkList[bookmark.UserWebFolderID].Add(bookmark);
                    else
                        dicWebfolderIDToBookmarkList.Add(bookmark.UserWebFolderID, new List<BizBookmarkInfo>() { bookmark });
                }
            }

            if (lstWebFolder != null)
            {
                foreach (var folder in lstWebFolder)
                {
                    if (dicParentWebfolderIDToWebfolder.ContainsKey(folder.ParentWebfolderID))
                    {
                        dicParentWebfolderIDToWebfolder[folder.ParentWebfolderID].Add(folder);
                    }
                    else
                    {
                        dicParentWebfolderIDToWebfolder.Add(folder.ParentWebfolderID, new List<BizUserWebFolder>()
                        {
                            folder
                        });
                    }

                }
            }
        }


        public static bool DeleteWebfolder(long webFolderID)
        {
            return BookmarkInfoBo.DeleteByWebFolderID(webFolderID) && webFolderDAL.DeleteByUserWebFolderID(webFolderID) >0;
        }

    }
}
