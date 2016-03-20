using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        /// <summary>
        /// 对外是否可见
        /// </summary>
        public sbyte Visible { get; set; }

        /// <summary>
        /// 父收藏夹ID
        /// </summary>
        public long ParentWebfolderID { get; set; }

        /// <summary>
        /// 收藏夹描述
        /// </summary>
        public string IntroContent { get; set; }

        /// <summary>
        /// IElement 序列化数据
        /// </summary>
        public string IElementJSON { get; set; }


        public int IElementHashcode { get; set; }


        public List<UIBookmarkInfo> UIBookmarkInfoList { get; set; }


        public List<UIWebFolderInfo> ChildrenFolderList { get; set; }
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
        /// 导入XML信息
        /// </summary>
        public string IElementJSON { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BookmarkName { get; set; }


    }
}