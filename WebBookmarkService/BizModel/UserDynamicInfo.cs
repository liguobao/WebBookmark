//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using WebBookmarkService.Model;

namespace WebBookmarkService.BizModel
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class BizUserDynamicInfo
	{
        /// <summary>
        /// 
        /// </summary>
		public long UserDynamicInfoID{get;set;}
            
        /// <summary>
        /// 用户动态内容
        /// </summary>
		public string UserDynamicContent{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 用户动态类型
        /// </summary>
		public int UserDynamicType{get;set;}
            
        /// <summary>
        /// 动态相关URL
        /// </summary>
		public string UserDynamicURL{get;set;}
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public UserDynamicInfo ToModel()
        {
            return new UserDynamicInfo()
            {
                UserDynamicInfoID =  UserDynamicInfoID,
                UserDynamicContent =  UserDynamicContent,
                CreateTime =  CreateTime,
                UserInfoID =  UserInfoID,
                UserDynamicType =  UserDynamicType,
                UserDynamicURL =  UserDynamicURL,
            };
        }
        
        
        public BizUserDynamicInfo (UserDynamicInfo dataInfo)
        {
             UserDynamicInfoID =  dataInfo.UserDynamicInfoID;
             UserDynamicContent =  dataInfo.UserDynamicContent;
             CreateTime =  dataInfo.CreateTime;
             UserInfoID =  dataInfo.UserInfoID;
             UserDynamicType =  dataInfo.UserDynamicType;
             UserDynamicURL =  dataInfo.UserDynamicURL;
        }
        
        public  BizUserDynamicInfo ()
        {
        
        } 
        
	}
}