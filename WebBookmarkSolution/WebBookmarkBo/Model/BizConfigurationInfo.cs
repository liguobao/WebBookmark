//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;
using System.Linq;

namespace WebBookmarkBo.Model
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class BizConfigurationInfo
	{

        private static readonly ConfigurationInfoDAL DAL = new ConfigurationInfoDAL();


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

        public static BizConfigurationInfo LoadByID(int confID)
        {
            var model = DAL.GetByConfigurationInfoID(confID);
            return model!=null ? new BizConfigurationInfo(model):null;
        }

        public static BizConfigurationInfo LoadByKey(string key)
        {
            var model = DAL.GetByConfigurationKey(key);
            return model != null ? new BizConfigurationInfo(model) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationNo"></param>
        /// <returns></returns>
        public static List<BizConfigurationInfo> LoadByConfigurationNo(int configurationNo)
        {
            var lstModel = DAL.GetByConfigurationNo(configurationNo);
            if (lstModel == null)
                return new List<BizConfigurationInfo>();
            return lstModel.Select(model=>new BizConfigurationInfo(model)).ToList();
        }
        
	}



}