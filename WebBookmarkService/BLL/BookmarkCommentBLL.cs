using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkService.BLL
{
	[DataObject]
    public partial class BookmarkCommentBLL
    {
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回是否插入成功。
        /// </summary> 
		[DataObjectMethod(DataObjectMethodType.Insert)]
        public bool BookmarkCommentAdd(BookmarkComment bookmarkComment)
        {
            return new BookmarkCommentDAL().Add(bookmarkComment);
        }
        #endregion
        
        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Delete)]
        public int DeleteByBookmarkCommentID(long bookmarkCommentID)
        {
            return new BookmarkCommentDAL().DeleteByBookmarkCommentID(bookmarkCommentID);
        }
        #endregion
		
		
        #region  根据字段名获取数据记录
        /// <summary>
        /// 根据字段名获取数据记录
        /// </summary>
	     public IEnumerable<BookmarkComment> GetBycolumnName(string columnName,string columnContent)
		{
			return new BookmarkCommentDAL().GetBycolumnName(columnName,columnContent);
		}
        #endregion
        
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public int Update(BookmarkComment bookmarkComment)
        {
            return new BookmarkCommentDAL().Update(bookmarkComment);
        }
        #endregion

        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Select)]
        public BookmarkComment GetByBookmarkCommentID(long bookmarkCommentID)
        {
            return new BookmarkCommentDAL().GetByBookmarkCommentID(bookmarkCommentID);
        }
        #endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>
		public int GetTotalCount()
		{
			return new BookmarkCommentDAL().GetTotalCount();
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<BookmarkComment> GetPagedData(int minrownum,int maxrownum)
		{
			return new BookmarkCommentDAL().GetPagedData(minrownum,maxrownum);
		}
		#endregion
        
        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<BookmarkComment> GetAll()
		{
			return new BookmarkCommentDAL().GetAll();
		}
        #endregion
    }
}