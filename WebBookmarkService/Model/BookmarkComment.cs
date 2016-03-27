//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace WebBookmarkService.Model
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class BookmarkComment
	{
        /// <summary>
        /// 主键
        /// </summary>
		public long BookmarkCommentID{get;set;}
            
        /// <summary>
        /// 标题
        /// </summary>
		public string CommentTitle{get;set;}
            
        /// <summary>
        /// 点评内容
        /// </summary>
		public string CommentContent{get;set;}
            
        /// <summary>
        /// 书签ID，书签表主键
        /// </summary>
		public long BookmarkInfoID{get;set;}
            
        /// <summary>
        /// 点评者ID
        /// </summary>
		public long CriticsUserID{get;set;}
            
        /// <summary>
        /// 点评时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 书签拥有者ID
        /// </summary>
		public long BookmarkUserID{get;set;}
            
	}
}