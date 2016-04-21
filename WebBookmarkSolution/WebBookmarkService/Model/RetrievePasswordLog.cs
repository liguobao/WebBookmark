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
	public class RetrievePasswordLog
	{
        /// <summary>
        /// 
        /// </summary>
		public long RetrievePasswordLogID{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 当前状态
        /// </summary>
		public int LogStatus{get;set;}
            
        /// <summary>
        /// token
        /// </summary>
		public string Token{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 最后更新时间
        /// </summary>
		public DateTime LastTime{get;set;}
            
	}
}