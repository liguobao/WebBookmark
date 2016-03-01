using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using WebfolderService.DAL;
using WebfolderService.Model;

namespace WebfolderService.BLL
{
	[DataObject]
    public partial class WebpageCommentBLL
    {
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回是否插入成功。
        /// </summary> 
		[DataObjectMethod(DataObjectMethodType.Insert)]
        public bool WebpageCommentAdd(WebpageComment webpageComment)
        {
            return new WebpageCommentDAL().Add(webpageComment);
        }
        #endregion
        
        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Delete)]
        public int DeleteByWebpageCommentID(long webpageCommentID)
        {
            return new WebpageCommentDAL().DeleteByWebpageCommentID(webpageCommentID);
        }
        #endregion
		
		
        #region  根据字段名获取数据记录
        /// <summary>
        /// 根据字段名获取数据记录
        /// </summary>
	     public IEnumerable<WebpageComment> GetBycolumnName(string columnName,string columnContent)
		{
			return new WebpageCommentDAL().GetBycolumnName(columnName,columnContent);
		}
        #endregion
        
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Update)]
		public int Update(WebpageComment webpageComment)
        {
            return new WebpageCommentDAL().Update(webpageComment);
        }
        #endregion

        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
		[DataObjectMethod(DataObjectMethodType.Select)]
        public WebpageComment GetByWebpageCommentID(long webpageCommentID)
        {
            return new WebpageCommentDAL().GetByWebpageCommentID(webpageCommentID);
        }
        #endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>
		public int GetTotalCount()
		{
			return new WebpageCommentDAL().GetTotalCount();
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<WebpageComment> GetPagedData(int minrownum,int maxrownum)
		{
			return new WebpageCommentDAL().GetPagedData(minrownum,maxrownum);
		}
		#endregion
        
        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary>  
		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<WebpageComment> GetAll()
		{
			return new WebpageCommentDAL().GetAll();
		}
        #endregion
    }
}