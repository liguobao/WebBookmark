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
	public class UserLabelInfolog
	{
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long UserLabelInfoID{get;set;}
            
        /// <summary>
        /// 标签名称
        /// </summary>
		public string LabelName{get;set;}
            
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