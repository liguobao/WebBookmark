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
	public class BizMessageInfo
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
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public MessageInfo ToModel()
        {
            return new MessageInfo()
            {
                MessageInfoID =  MessageInfoID,
                MessageTitle =  MessageTitle,
                MessageContent =  MessageContent,
                UserInfoID =  UserInfoID,
                IsRead =  IsRead,
                MessageInfoType =  MessageInfoType,
                CreateTime =  CreateTime,
                MessageURL =  MessageURL,
            };
        }
        
        
        public BizMessageInfo (MessageInfo dataInfo)
        {
             MessageInfoID =  dataInfo.MessageInfoID;
             MessageTitle =  dataInfo.MessageTitle;
             MessageContent =  dataInfo.MessageContent;
             UserInfoID =  dataInfo.UserInfoID;
             IsRead =  dataInfo.IsRead;
             MessageInfoType =  dataInfo.MessageInfoType;
             CreateTime =  dataInfo.CreateTime;
             MessageURL =  dataInfo.MessageURL;
        }
        
        public  BizMessageInfo ()
        {
        
        } 
        
	}
}