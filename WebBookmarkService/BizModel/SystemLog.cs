//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace WebBookmarkService.BizModel
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class BizSystemLog
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
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public SystemLog ToModel()
        {
            return new SystemLog()
            {
                SystemLogID =  SystemLogID,
                LogTitle =  LogTitle,
                LogContent =  LogContent,
                LogType =  LogType,
                CreateTime =  CreateTime,
            }
        }
        
        
        public BizSystemLog (SystemLog dataInfo)
        {
             SystemLogID =  dataInfo.SystemLogID;
             LogTitle =  dataInfo.LogTitle;
             LogContent =  dataInfo.LogContent;
             LogType =  dataInfo.LogType;
             CreateTime =  dataInfo.CreateTime;
        }
        
        public  BizSystemLog ()
        {
        
        } 
        
	}
}