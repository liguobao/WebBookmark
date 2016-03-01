//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace WebfolderService.Model
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class WebpageComment
	{
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long WebpageCommentID
		{
			 get;
             set;
		}
            
        /// <summary>
        /// 评论者ID
        /// </summary>
		public long CommentUserID
		{
			 get;
             set;
		}
            
        /// <summary>
        /// URL ID
        /// </summary>
		public long URLInfoID
		{
			 get;
             set;
		}
            
        /// <summary>
        /// 网页所有者ID
        /// </summary>
		public long WebpageUserInfoID
		{
			 get;
             set;
		}
            
        /// <summary>
        /// 标题
        /// </summary>
		public string CommentTitle
		{
			 get;
             set;
		}
            
        /// <summary>
        /// 评论内容
        /// </summary>
		public string CommentContent
		{
			 get;
             set;
		}
            
        /// <summary>
        ///  评论类型
        /// </summary>
		public int CommentType
		{
			 get;
             set;
		}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime
		{
			 get;
             set;
		}
            
	}
}