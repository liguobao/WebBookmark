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
	public class MessageInfo
	{
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long MessageInfoID{get;set;}
            
        /// <summary>
        /// 消息主题
        /// </summary>
		public string MessageTitle{get;set;}
            
        /// <summary>
        /// 消息内容
        /// </summary>
		public string MessageContent{get;set;}
            
        /// <summary>
        /// UID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 是否已读
        /// </summary>
		public int IsRead{get;set;}
            
        /// <summary>
        /// 消息类型
        /// </summary>
		public int MessageInfoType{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 消息相关URL
        /// </summary>
		public string MessageURL{get;set;}
            
	}
}