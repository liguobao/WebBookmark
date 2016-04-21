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
	public class BizConfigurationInfo
	{
        /// <summary>
        /// 
        /// </summary>
		public int ConfigurationInfoID{get;set;}
            
        /// <summary>
        /// key,可通过key找到数据
        /// </summary>
		public string ConfigurationKey{get;set;}
            
        /// <summary>
        /// 内容
        /// </summary>
		public string ConfigurationValue{get;set;}
            
        /// <summary>
        /// 描述
        /// </summary>
		public string Description{get;set;}
            
        /// <summary>
        /// 序号
        /// </summary>
		public int ConfigurationNo{get;set;}
            
        /// <summary>
        /// 主键/hashcode
        /// </summary>
		public int HashCode{get;set;}
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public ConfigurationInfo ToModel()
        {
            return new ConfigurationInfo()
            {
                ConfigurationInfoID =  ConfigurationInfoID,
                ConfigurationKey =  ConfigurationKey,
                ConfigurationValue =  ConfigurationValue,
                Description =  Description,
                ConfigurationNo =  ConfigurationNo,
                HashCode =  HashCode,
            };
        }
        
        
        public BizConfigurationInfo (ConfigurationInfo dataInfo)
        {
             ConfigurationInfoID =  dataInfo.ConfigurationInfoID;
             ConfigurationKey =  dataInfo.ConfigurationKey;
             ConfigurationValue =  dataInfo.ConfigurationValue;
             Description =  dataInfo.Description;
             ConfigurationNo =  dataInfo.ConfigurationNo;
             HashCode =  dataInfo.HashCode;
        }
        
        public  BizConfigurationInfo ()
        {
        
        } 
        
	}
}