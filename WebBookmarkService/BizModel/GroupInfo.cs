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
	public class BizGroupInfo
	{
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long GroupInfoID{get;set;}
            
        /// <summary>
        /// 用户群组名称
        /// </summary>
		public string GroupName{get;set;}
            
        /// <summary>
        /// 用户群组介绍
        /// </summary>
		public string GroupIntro{get;set;}
            
        /// <summary>
        /// UID
        /// </summary>
		public long CreateUesrID{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public GroupInfo ToModel()
        {
            return new GroupInfo()
            {
                GroupInfoID =  GroupInfoID,
                GroupName =  GroupName,
                GroupIntro =  GroupIntro,
                CreateUesrID =  CreateUesrID,
                CreateTime =  CreateTime,
            };
        }
        
        
        public BizGroupInfo (GroupInfo dataInfo)
        {
             GroupInfoID =  dataInfo.GroupInfoID;
             GroupName =  dataInfo.GroupName;
             GroupIntro =  dataInfo.GroupIntro;
             CreateUesrID =  dataInfo.CreateUesrID;
             CreateTime =  dataInfo.CreateTime;
        }
        
        public  BizGroupInfo ()
        {
        
        } 
        
	}
}