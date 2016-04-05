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
	public class BizTagInfo
	{
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long TagInfoID{get;set;}
            
        /// <summary>
        /// URL信息ID
        /// </summary>
		public string TagName{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public TagInfo ToModel()
        {
            return new TagInfo()
            {
                TagInfoID =  TagInfoID,
                TagName =  TagName,
                UserInfoID =  UserInfoID,
                CreateTime =  CreateTime,
            };
        }
        
        
        public BizTagInfo (TagInfo dataInfo)
        {
             TagInfoID =  dataInfo.TagInfoID;
             TagName =  dataInfo.TagName;
             UserInfoID =  dataInfo.UserInfoID;
             CreateTime =  dataInfo.CreateTime;
        }
        
        public  BizTagInfo ()
        {
        
        } 
        
	}
}