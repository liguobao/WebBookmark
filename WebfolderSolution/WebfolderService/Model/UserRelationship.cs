//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace WebfolderService.Model
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class UserRelationship
	{
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long UserRelationshipID{get;set;}
            
        /// <summary>
        /// 关注者ID
        /// </summary>
		public long FollowerID{get;set;}
            
        /// <summary>
        /// 被关注者ID
        /// </summary>
		public long BeFollwedUID{get;set;}
            
        /// <summary>
        /// 是否互相关注
        /// </summary>
		public ushort IsMutuallyFollwe{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
	}
}