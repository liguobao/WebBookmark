//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace WebBookmarkService.Model
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class UserConfiguration
	{
        /// <summary>
        /// 
        /// </summary>
		public long UserConfigurationID{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// key
        /// </summary>
		public string UserConfigurationKey{get;set;}
            
        /// <summary>
        /// value
        /// </summary>
		public string UserConfigurationValue{get;set;}
            
        /// <summary>
        /// 描叙
        /// </summary>
		public string Description{get;set;}
            
        /// <summary>
        /// 序号
        /// </summary>
		public int UserConfigurationNo{get;set;}
            
	}
}