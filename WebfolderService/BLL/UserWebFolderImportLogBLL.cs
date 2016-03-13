using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using WebfolderService.DAL;
using WebfolderService.Model;

namespace WebfolderService.BLL
{
	[DataObject]
    public partial class UserWebFolderImportLogBLL
    {
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回是否插入成功。
        /// </summary> 
		[DataObjectMethod(DataObjectMethodType.Insert)]
        public bool UserWebFolderImportLogAdd(UserWebFolderImportLog userWebFolderImportLog)
        {
            return new UserWebFolderImportLogDAL().Add(userWebFolderImportLog);
        }
        #endregion
        
        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Delete)]
        public int DeleteByUserWebFolderImportLogID(long userWebFolderImportLogID)
        {
            return new UserWebFolderImportLogDAL().DeleteByUserWebFolderImportLogID(userWebFolderImportLogID);
        }
        #endregion
		
		
        #region  根据字段名获取数据记录
        /// <summary>
        /// 根据字段名获取数据记录
        /// </summary>
	     public IEnumerable<UserWebFolderImportLog> GetBycolumnName(string columnName,string columnContent)
		{
			return new UserWebFolderImportLogDAL().GetBycolumnName(columnName,columnContent);
		}
        #endregion
        
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public int Update(UserWebFolderImportLog userWebFolderImportLog)
        {
            return new UserWebFolderImportLogDAL().Update(userWebFolderImportLog);
        }
        #endregion

        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Select)]
        public UserWebFolderImportLog GetByUserWebFolderImportLogID(long userWebFolderImportLogID)
        {
            return new UserWebFolderImportLogDAL().GetByUserWebFolderImportLogID(userWebFolderImportLogID);
        }
        #endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>
		public int GetTotalCount()
		{
			return new UserWebFolderImportLogDAL().GetTotalCount();
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<UserWebFolderImportLog> GetPagedData(int minrownum,int maxrownum)
		{
			return new UserWebFolderImportLogDAL().GetPagedData(minrownum,maxrownum);
		}
		#endregion
        
        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<UserWebFolderImportLog> GetAll()
		{
			return new UserWebFolderImportLogDAL().GetAll();
		}
        #endregion
    }
}