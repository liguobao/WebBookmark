using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkService.BLL
{
	[DataObject]
    public partial class GroupUserBLL
    {
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回是否插入成功。
        /// </summary> 
		[DataObjectMethod(DataObjectMethodType.Insert)]
        public bool GroupUserAdd(GroupUser groupUser)
        {
            return new GroupUserDAL().Add(groupUser);
        }
        #endregion
        
        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Delete)]
        public int DeleteByGroupUserID(long groupUserID)
        {
            return new GroupUserDAL().DeleteByGroupUserID(groupUserID);
        }
        #endregion
		
		
        #region  根据字段名获取数据记录
        /// <summary>
        /// 根据字段名获取数据记录
        /// </summary>
	     public IEnumerable<GroupUser> GetBycolumnName(string columnName,string columnContent)
		{
			return new GroupUserDAL().GetBycolumnName(columnName,columnContent);
		}
        #endregion
        
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public int Update(GroupUser groupUser)
        {
            return new GroupUserDAL().Update(groupUser);
        }
        #endregion

        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Select)]
        public GroupUser GetByGroupUserID(long groupUserID)
        {
            return new GroupUserDAL().GetByGroupUserID(groupUserID);
        }
        #endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>
		public int GetTotalCount()
		{
			return new GroupUserDAL().GetTotalCount();
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<GroupUser> GetPagedData(int minrownum,int maxrownum)
		{
			return new GroupUserDAL().GetPagedData(minrownum,maxrownum);
		}
		#endregion
        
        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<GroupUser> GetAll()
		{
			return new GroupUserDAL().GetAll();
		}
        #endregion
    }
}