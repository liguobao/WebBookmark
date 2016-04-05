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
	public class BookmarkTagInfo
	{
        /// <summary>
        /// 主键
        /// </summary>
		public long BookmarkTagInfoID{get;set;}
            
        /// <summary>
        /// 书签ID
        /// </summary>
		public long BookmarkInfoID{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 标签页主键
        /// </summary>
		public long TagInfoID{get;set;}
            
	}
}