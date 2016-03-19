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
	public class UserWebFolder
	{
        /// <summary>
        /// 
        /// </summary>
		public long UserWebFolderID{get;set;}
            
        /// <summary>
        /// 收藏夹名称
        /// </summary>
		public string WebFolderName{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 对外是否可见
        /// </summary>
		public sbyte Visible{get;set;}
            
        /// <summary>
        /// 父收藏夹ID
        /// </summary>
		public long? ParentWebfolderID{get;set;}
            
        /// <summary>
        /// 收藏夹描述
        /// </summary>
		public string IntroContent{get;set;}
            
        /// <summary>
        /// IElement 序列化数据
        /// </summary>
		public string IElementJSON{get;set;}


        public int IElementHashcode { get; set; }

    }
}