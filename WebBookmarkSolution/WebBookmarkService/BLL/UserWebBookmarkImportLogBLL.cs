using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkService.BLL
{
	[DataObject]
    public partial class UserWebBookmarkImportLogBLL
    {
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回是否插入成功。
        /// </summary> 
		[DataObjectMethod(DataObjectMethodType.Insert)]
        public bool UserWebBookmarkImportLogAdd(UserWebBookmarkImportLog userWebBookmarkImportLog)
        {
            return new UserWebBookmarkImportLogDAL().Add(userWebBookmarkImportLog);
        }
        #endregion
        
        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Delete)]
        public int DeleteByUserWebBookmarkImportLogID(long userWebBookmarkImportLogID)
        {
            return new UserWebBookmarkImportLogDAL().DeleteByUserWebBookmarkImportLogID(userWebBookmarkImportLogID);
        }
        #endregion
		
		
        #region  根据字段名获取数据记录
        /// <summary>
        /// 根据字段名获取数据记录
        /// </summary>
	     public IEnumerable<UserWebBookmarkImportLog> GetBycolumnName(string columnName,string columnContent)
		{
			return new UserWebBookmarkImportLogDAL().GetBycolumnName(columnName,columnContent);
		}
        #endregion
        
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public int Update(UserWebBookmarkImportLog userWebBookmarkImportLog)
        {
            return new UserWebBookmarkImportLogDAL().Update(userWebBookmarkImportLog);
        }
        #endregion

        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Select)]
        public UserWebBookmarkImportLog GetByUserWebBookmarkImportLogID(long userWebBookmarkImportLogID)
        {
            return new UserWebBookmarkImportLogDAL().GetByUserWebBookmarkImportLogID(userWebBookmarkImportLogID);
        }
        #endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>
		public int GetTotalCount()
		{
			return new UserWebBookmarkImportLogDAL().GetTotalCount();
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<UserWebBookmarkImportLog> GetPagedData(int minrownum,int maxrownum)
		{
			return new UserWebBookmarkImportLogDAL().GetPagedData(minrownum,maxrownum);
		}
		#endregion
        
        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<UserWebBookmarkImportLog> GetAll()
		{
			return new UserWebBookmarkImportLogDAL().GetAll();
		}
        #endregion
    }
}