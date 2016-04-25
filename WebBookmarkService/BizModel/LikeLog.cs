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
	public class BizLikeLog
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
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public LikeLog ToModel()
        {
            return new LikeLog()
            {
                LikeLogID =  LikeLogID,
                UserInfoID =  UserInfoID,
                InfoID =  InfoID,
                InfoType =  InfoType,
            };
        }
        
        
        public BizLikeLog (LikeLog dataInfo)
        {
             LikeLogID =  dataInfo.LikeLogID;
             UserInfoID =  dataInfo.UserInfoID;
             InfoID =  dataInfo.InfoID;
             InfoType =  dataInfo.InfoType;
        }
        
        public  BizLikeLog ()
        {
        
        } 
        
	}
}