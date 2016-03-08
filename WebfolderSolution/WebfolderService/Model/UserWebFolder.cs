//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace WebfolderService.Model
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class UserWebFolder
	{
        /// <summary>
        /// 主键，自增
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
		public ushort Visible{get;set;}
            
	}
}