using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkService.BLL
{
	[DataObject]
    public partial class ConfigurationInfoBLL
    {
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回是否插入成功。
        /// </summary> 
		[DataObjectMethod(DataObjectMethodType.Insert)]
        public bool ConfigurationInfoAdd(ConfigurationInfo configurationInfo)
        {
            return new ConfigurationInfoDAL().Add(configurationInfo);
        }
        #endregion
        
        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Delete)]
        public int DeleteByConfigurationInfoID(int configurationInfoID)
        {
            return new ConfigurationInfoDAL().DeleteByConfigurationInfoID(configurationInfoID);
        }
        #endregion
		
		
        #region  根据字段名获取数据记录
        /// <summary>
        /// 根据字段名获取数据记录
        /// </summary>
	     public IEnumerable<ConfigurationInfo> GetBycolumnName(string columnName,string columnContent)
		{
			return new ConfigurationInfoDAL().GetBycolumnName(columnName,columnContent);
		}
        #endregion
        
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public int Update(ConfigurationInfo configurationInfo)
        {
            return new ConfigurationInfoDAL().Update(configurationInfo);
        }
        #endregion

        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Select)]
        public ConfigurationInfo GetByConfigurationInfoID(int configurationInfoID)
        {
            return new ConfigurationInfoDAL().GetByConfigurationInfoID(configurationInfoID);
        }
        #endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>
		public int GetTotalCount()
		{
			return new ConfigurationInfoDAL().GetTotalCount();
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<ConfigurationInfo> GetPagedData(int minrownum,int maxrownum)
		{
			return new ConfigurationInfoDAL().GetPagedData(minrownum,maxrownum);
		}
		#endregion
        
        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<ConfigurationInfo> GetAll()
		{
			return new ConfigurationInfoDAL().GetAll();
		}
        #endregion
    }
}