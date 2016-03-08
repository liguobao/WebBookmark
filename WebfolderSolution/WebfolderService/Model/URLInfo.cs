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
	public class URLInfo
	{
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long URLInfoID{get;set;}
            
        /// <summary>
        /// 网页收藏夹ID
        /// </summary>
		public long UserWebFolderID{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 网址
        /// </summary>
		public string URL{get;set;}
            
        /// <summary>
        /// 网页HTML
        /// </summary>
		public string HTML{get;set;}
            
        /// <summary>
        /// 域名
        /// </summary>
		public string DomainName{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
	}
}