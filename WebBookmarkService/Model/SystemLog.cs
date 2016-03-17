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
	public class SystemLog
	{
        /// <summary>
        /// 
        /// </summary>
		public long SystemLogID{get;set;}
            
        /// <summary>
        /// 日志标题
        /// </summary>
		public string LogTitle{get;set;}
            
        /// <summary>
        /// 日志内容
        /// </summary>
		public string LogContent{get;set;}
            
        /// <summary>
        /// 日志类型：常规/警告/异常
        /// </summary>
		public int? LogType{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime? CreateTime{get;set;}
            
	}
}