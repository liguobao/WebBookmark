using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBookmarkUI.Models
{
    public class UICommentInfo
    {
        public UIUserInfo CriticsUserInfo { get; set; }



        /// <summary>
        /// 主键
        /// </summary>
		public long CommentID{get;set;}
            
        /// <summary>
        /// 标题
        /// </summary>
		public string CommentTitle{get;set;}
            
        /// <summary>
        /// 点评内容
        /// </summary>
		public string CommentContent{get;set;}
            
        /// <summary>
        /// 书签/书签夹ID
        /// </summary>
		public long InfoID{get;set;}


        public UICommentType InfoType { get; set; }
            
        /// <summary>
        /// 点评者ID
        /// </summary>
		public long CriticsUserID{get;set;}
            
        /// <summary>
        /// 点评时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 拥有者ID
        /// </summary>
		public long BookmarkUserID{get;set;}



    }


    public enum UICommentType
    {
        Bookmark=1,

        WebFolder=2,
    }
}