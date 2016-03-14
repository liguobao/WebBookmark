using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkService.BLL
{
	[DataObject]
    public partial class GroupBLL
    {
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回是否插入成功。
        /// </summary> 
		[DataObjectMethod(DataObjectMethodType.Insert)]
        public bool GroupAdd(Group group)
        {
            return new GroupDAL().Add(group);
        }
        #endregion
        
        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Delete)]
        public int DeleteByGroupID(long groupID)
        {
            return new GroupDAL().DeleteByGroupID(groupID);
        }
        #endregion
		
		
        #region  根据字段名获取数据记录
        /// <summary>
        /// 根据字段名获取数据记录
        /// </summary>
	     public IEnumerable<Group> GetBycolumnName(string columnName,string columnContent)
		{
			return new GroupDAL().GetBycolumnName(columnName,columnContent);
		}
        #endregion
        
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public int Update(Group group)
        {
            return new GroupDAL().Update(group);
        }
        #endregion

        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Select)]
        public Group GetByGroupID(long groupID)
        {
            return new GroupDAL().GetByGroupID(groupID);
        }
        #endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>
		public int GetTotalCount()
		{
			return new GroupDAL().GetTotalCount();
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<Group> GetPagedData(int minrownum,int maxrownum)
		{
			return new GroupDAL().GetPagedData(minrownum,maxrownum);
		}
		#endregion
        
        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<Group> GetAll()
		{
			return new GroupDAL().GetAll();
		}
        #endregion
    }
}