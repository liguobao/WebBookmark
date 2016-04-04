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
	public class BookmarkInfo
	{
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long BookmarkInfoID{get;set;}
            
        /// <summary>
        /// 网页收藏夹ID
        /// </summary>
		public long UserWebFolderID{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 网址
        /// </summary>
		public string Href{get;set;}
            
        /// <summary>
        /// 网页HTML
        /// </summary>
		public string HTML{get;set;}
            
        /// <summary>
        /// 域名
        /// </summary>
		public string Host{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 导入XML信息
        /// </summary>
		public string IElementJSON{get;set;}
            
        /// <summary>
        /// 
        /// </summary>
		public string BookmarkName{get;set;}
            
        /// <summary>
        /// 等级,0:对外公开，1：对关注者公开，2对群组公开，3：仅自己可见
        /// </summary>
		public int Grate{get;set;}
            
        /// <summary>
        /// 类似于主键
        /// </summary>
		public int HashCode{get;set;}
            
	}
}