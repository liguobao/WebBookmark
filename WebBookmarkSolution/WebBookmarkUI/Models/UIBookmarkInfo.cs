using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookmarkBo.Model;

namespace WebBookmarkUI.Models
{
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


        public bool IsShowWithiframe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BookmarkName { get; set; }

        public UIUserInfo UserInfo { get; set; }


        public List<UIBookmarkTagInfo> TagInfoList { get; set; }


        public List<BizLikeLog> LikeLogList { get; set; }

        public int LikeCount { get; set; }

    }
}