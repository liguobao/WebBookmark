using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkBo.Model;
using WebBookmarkBo.Service;
using WebBookmarkService;
using WebBookmarkService.DAL;

namespace WebBookmarkBo.Service
{
    public class UserDynamicInfoBo
    {

        private static readonly UserDynamicInfoDAL DAL = new UserDynamicInfoDAL();

        public static void CreateDynamicInfoMessage(long userInfoID,DynamicInfoType type,object info)
        {
            Task.Factory.StartNew(() => 
            {
                try
                {
                    switch (type)
                    {
                        case DynamicInfoType.NewBookmark:
                            AddNewBookmarkInfo(userInfoID, info);
                            return;
                        case DynamicInfoType.NewBookmarkComment:
                            AddNewBookmarkComment(userInfoID, info);
                            return;
                        case DynamicInfoType.NewFollower:
                            AddNewFollower(userInfoID, info);

                            return;
                        case DynamicInfoType.NewWebFolder:
                            AddNewFolder(userInfoID, info);
                            return;

                        case DynamicInfoType.NewLikeBookmark:
                            AddNewLikeBookmark(userInfoID, info);
                            return;
                    }

                }catch(Exception ex)
                {
                    LogHelper.WriteException("CreateDynamicInfoMessage Exception", ex, new { UserID = userInfoID, DynamicInfoType = type, ObjectInfo = info });
                }
            });
           
        }

        private static void AddNewFollower(long userInfoID, object info)
        {
            var befollower = info as BizUserInfo;
            if (befollower != null)
            {
                BizUserDynamicInfo userDynamicInfo = new BizUserDynamicInfo();
                userDynamicInfo.UserInfoID = userInfoID;
                userDynamicInfo.CreateTime = DateTime.Now;
                userDynamicInfo.UserDynamicType = (int)DynamicInfoType.NewFollower;
                userDynamicInfo.UserDynamicContent = string.Format("关注了【{0}】。", befollower.UserName);
                userDynamicInfo.UserDynamicURL = "~/UserInfo/ShowUserDetail?uid=" + befollower.UserInfoID;
                userDynamicInfo.Save();
            }
        }

        private static void AddNewBookmarkComment(long userInfoID, object info)
        {
            var bookmarkInfo = info as BizBookmarkInfo;
            if (bookmarkInfo != null)
            {
                BizUserDynamicInfo userDynamicInfo = new BizUserDynamicInfo();
                userDynamicInfo.UserInfoID = userInfoID;
                userDynamicInfo.CreateTime = DateTime.Now;
                userDynamicInfo.UserDynamicType = (int)DynamicInfoType.NewBookmarkComment;
                userDynamicInfo.UserDynamicContent = string.Format("评论了书签【{0}】。", bookmarkInfo.BookmarkName);
                userDynamicInfo.UserDynamicURL = "~/ShowBookmarkInfo?bookmarkID=" + bookmarkInfo.BookmarkInfoID;
                userDynamicInfo.Save();
            }
        }

        private static void AddNewFolder(long userInfoID, object info)
        {
            var folderInfo = info as BizUserWebFolder;
            if (folderInfo != null)
            {
                BizUserDynamicInfo userDynamicInfo = new BizUserDynamicInfo();
                userDynamicInfo.UserInfoID = userInfoID;
                userDynamicInfo.CreateTime = DateTime.Now;
                userDynamicInfo.UserDynamicType = (int)DynamicInfoType.NewFollower;
                userDynamicInfo.UserDynamicContent = string.Format("添加了新书签夹【{0}】。", folderInfo.WebFolderName);
                userDynamicInfo.UserDynamicURL = "~/UserInfo/ShowUserDetail?uid=" + folderInfo.UserInfoID;
                userDynamicInfo.Save();
            }
        }

        private static void AddNewBookmarkInfo(long userInfoID, object info)
        {
            var bookmarkInfo = info as BizBookmarkInfo;
            if (bookmarkInfo != null)
            {
                BizUserDynamicInfo userDynamicInfo = new BizUserDynamicInfo();
                userDynamicInfo.UserInfoID = userInfoID;
                userDynamicInfo.CreateTime = DateTime.Now;
                userDynamicInfo.UserDynamicType = (int)DynamicInfoType.NewBookmark;
                userDynamicInfo.UserDynamicContent = string.Format("添加了新书签【{0}】。", bookmarkInfo.BookmarkName);
                userDynamicInfo.UserDynamicURL = "~/ShowBookmarkInfo?bookmarkID=" + bookmarkInfo.BookmarkInfoID;
                userDynamicInfo.Save();
            }
        }


        private static void AddNewLikeBookmark(long userInfoID, object info)
        {
            var bookmarkInfo = info as BizBookmarkInfo;
            if (bookmarkInfo != null)
            {
                BizUserDynamicInfo userDynamicInfo = new BizUserDynamicInfo();
                userDynamicInfo.UserInfoID = userInfoID;
                userDynamicInfo.CreateTime = DateTime.Now;
                userDynamicInfo.UserDynamicType = (int)DynamicInfoType.NewLikeBookmark;
                userDynamicInfo.UserDynamicContent = string.Format("给书签【{0}】 点了赞。", bookmarkInfo.BookmarkName);
                userDynamicInfo.UserDynamicURL = "~/ShowBookmarkInfo?bookmarkID=" + bookmarkInfo.BookmarkInfoID;
                userDynamicInfo.Save();
            }
        }


        public static List<BizUserDynamicInfo> LoadDynamicLog(long loginUserID)
        {
            var dicUserRelationship = UserRelationshipBo.GetByFollowUserID(loginUserID);
            var userIDs = dicUserRelationship.Keys.ToList();
            var lstModel = DAL.GetListByUserIDs(userIDs);
            if (lstModel == null)
                return new List<BizUserDynamicInfo>();
            var dicUserInfo = UserInfoBo.GetListByUIDList(userIDs).ToDictionary(model=>model.UserInfoID,model=>model);
            return lstModel.Select(model => new BizUserDynamicInfo(model) 
            {
                UserInfo = dicUserInfo.ContainsKey(model.UserInfoID) ? dicUserInfo[model.UserInfoID] : null,
            }).ToList();
        }

    }


    public enum DynamicInfoType 
    {
        NewBookmark = 0,

        NewWebFolder = 1,

        NewFollower = 2,

        NewBookmarkComment = 3,

        NewLikeBookmark = 4,

    }
}
