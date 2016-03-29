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
	public class BizGroupUser
	{
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long GroupUserID{get;set;}
            
        /// <summary>
        /// 用户群组ID
        /// </summary>
		public long GroupInfoID{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 是否通过
        /// </summary>
		public int IsPass{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public GroupUser ToModel()
        {
            return new GroupUser()
            {
                GroupUserID =  GroupUserID,
                GroupInfoID =  GroupInfoID,
                UserInfoID =  UserInfoID,
                IsPass =  IsPass,
                CreateTime =  CreateTime,
            };
        }
        
        
        public BizGroupUser (GroupUser dataInfo)
        {
             GroupUserID =  dataInfo.GroupUserID;
             GroupInfoID =  dataInfo.GroupInfoID;
             UserInfoID =  dataInfo.UserInfoID;
             IsPass =  dataInfo.IsPass;
             CreateTime =  dataInfo.CreateTime;
        }
        
        public  BizGroupUser ()
        {
        
        } 
        
	}
}