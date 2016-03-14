using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkService.BLL
{
	[DataObject]
    public partial class HrefInfoBLL
    {
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回是否插入成功。
        /// </summary> 
		[DataObjectMethod(DataObjectMethodType.Insert)]
        public bool HrefInfoAdd(HrefInfo hrefInfo)
        {
            return new HrefInfoDAL().Add(hrefInfo);
        }
        #endregion
        
        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Delete)]
        public int DeleteByHrefInfoID(long hrefInfoID)
        {
            return new HrefInfoDAL().DeleteByHrefInfoID(hrefInfoID);
        }
        #endregion
		
		
        #region  根据字段名获取数据记录
        /// <summary>
        /// 根据字段名获取数据记录
        /// </summary>
	     public IEnumerable<HrefInfo> GetBycolumnName(string columnName,string columnContent)
		{
			return new HrefInfoDAL().GetBycolumnName(columnName,columnContent);
		}
        #endregion
        
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public int Update(HrefInfo hrefInfo)
        {
            return new HrefInfoDAL().Update(hrefInfo);
        }
        #endregion

        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Select)]
        public HrefInfo GetByHrefInfoID(long hrefInfoID)
        {
            return new HrefInfoDAL().GetByHrefInfoID(hrefInfoID);
        }
        #endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>
		public int GetTotalCount()
		{
			return new HrefInfoDAL().GetTotalCount();
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<HrefInfo> GetPagedData(int minrownum,int maxrownum)
		{
			return new HrefInfoDAL().GetPagedData(minrownum,maxrownum);
		}
		#endregion
        
        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<HrefInfo> GetAll()
		{
			return new HrefInfoDAL().GetAll();
		}
        #endregion
    }
}