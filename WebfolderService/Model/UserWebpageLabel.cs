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
	public class UserWebpageLabel
	{
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long UserWebpageLabelID{get;set;}
            
        /// <summary>
        /// URL信息ID
        /// </summary>
		public long URLInfoID{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
	}
}