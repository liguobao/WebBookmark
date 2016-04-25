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
	public class LikeLog
	{
        /// <summary>
        /// 主键
        /// </summary>
		public long LikeLogID{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 外键ID
        /// </summary>
		public long InfoID{get;set;}
            
        /// <summary>
        /// 类型
        /// </summary>
		public int InfoType{get;set;}
            
	}
}