using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookmarkBo.Model;

namespace WebBookmarkUI.Models
{

    /// <summary>
    /// 书签夹
    /// </summary>
    public class UIWebFolderInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public long UserWebFolderID { get; set; }

        /// <summary>
        /// 收藏夹名称
        /// </summary>
        public string WebFolderName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserInfoID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

      
        public int Grade { get; set; }

        /// <summary>
        /// 父收藏夹ID
        /// </summary>
        public long ParentWebfolderID { get; set; }


        public List<UIBookmarkInfo> UIBookmarkInfoList { get; set; }


        public List<UIWebFolderInfo> ChildrenFolderList { get; set; }


        public UIWebFolderInfo()
        {

        }

        public UIWebFolderInfo(BizUserWebFolder folderInfo)
        {
           if (folderInfo == null)
                return;
           UserWebFolderID = folderInfo.UserWebFolderID;
           UserInfoID = folderInfo.UserInfoID;
           ParentWebfolderID = folderInfo.ParentWebfolderID;
           CreateTime = folderInfo.CreateTime;
           Grade = folderInfo.Grade;
           WebFolderName = folderInfo.WebFolderName;

           UIBookmarkInfoList = folderInfo.BizBookmarkInfoList != null ?
                folderInfo.BizBookmarkInfoList.Select(info => new UIBookmarkInfo()
                {
                    UserInfoID = info.UserInfoID,
                    UserWebFolderID = info.UserWebFolderID,
                    BookmarkInfoID = info.BookmarkInfoID,
                    BookmarkName = info.BookmarkName,
                    CreateTime = info.CreateTime,
                    Href = info.Href,
                    Host = info.Host,
                }).ToList() : null;

            ChildrenFolderList = folderInfo.ChildrenFolderList != null ?
                folderInfo.ChildrenFolderList.Select(info => new UIWebFolderInfo()
                {
                    WebFolderName = info.WebFolderName,
                    ParentWebfolderID = info.ParentWebfolderID,
                    UserWebFolderID = info.UserWebFolderID,
                    CreateTime = info.CreateTime,
                    UserInfoID = info.UserInfoID,
                    Grade = info.Grade,
                }).ToList() : null;

        }

    }

    /// <summary>
    /// 书签内容
    /// </summary>
    public class UIBookmarkInfo
    {
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long BookmarkInfoID { get; set; }

        /// <summary>
        /// 网页收藏夹ID
        /// </summary>
        public long UserWebFolderID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserInfoID { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 网页HTML
        /// </summary>
        public string HTML { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BookmarkName { get; set; }

        public UIUserInfo UserInfo { get; set; }


    }
}