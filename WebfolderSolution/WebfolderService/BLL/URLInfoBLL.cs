using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using WebfolderService.DAL;
using WebfolderService.Model;

namespace WebfolderService.BLL
{
	[DataObject]
    public partial class URLInfoBLL
    {
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回是否插入成功。
        /// </summary> 
		[DataObjectMethod(DataObjectMethodType.Insert)]
        public bool URLInfoAdd(URLInfo uRLInfo)
        {
            return new URLInfoDAL().Add(uRLInfo);
        }
        #endregion
        
        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Delete)]
        public int DeleteByURLInfoID(long uRLInfoID)
        {
            return new URLInfoDAL().DeleteByURLInfoID(uRLInfoID);
        }
        #endregion
		
		
        #region  根据字段名获取数据记录
        /// <summary>
        /// 根据字段名获取数据记录
        /// </summary>
	     public IEnumerable<URLInfo> GetBycolumnName(string columnName,string columnContent)
		{
			return new URLInfoDAL().GetBycolumnName(columnName,columnContent);
		}
        #endregion
        
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public int Update(URLInfo uRLInfo)
        {
            return new URLInfoDAL().Update(uRLInfo);
        }
        #endregion

        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Select)]
        public URLInfo GetByURLInfoID(long uRLInfoID)
        {
            return new URLInfoDAL().GetByURLInfoID(uRLInfoID);
        }
        #endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>
		public int GetTotalCount()
		{
			return new URLInfoDAL().GetTotalCount();
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<URLInfo> GetPagedData(int minrownum,int maxrownum)
		{
			return new URLInfoDAL().GetPagedData(minrownum,maxrownum);
		}
		#endregion
        
        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<URLInfo> GetAll()
		{
			return new URLInfoDAL().GetAll();
		}
        #endregion
    }
}