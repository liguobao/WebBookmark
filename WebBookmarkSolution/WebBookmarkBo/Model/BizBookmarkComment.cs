//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;
using System.Linq;

namespace WebBookmarkBo.Model
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class BizBookmarkComment
	{
        /// <summary>
        /// 主键
        /// </summary>
		public long BookmarkCommentID{get;set;}
            
        /// <summary>
        /// 标题
        /// </summary>
		public string CommentTitle{get;set;}
            
        /// <summary>
        /// 点评内容
        /// </summary>
		public string CommentContent{get;set;}
            
        /// <summary>
        /// 书签ID，书签表主键
        /// </summary>
		public long BookmarkInfoID{get;set;}
            
        /// <summary>
        /// 点评者ID
        /// </summary>
		public long CriticsUserID{get;set;}
            
        /// <summary>
        /// 点评时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 书签拥有者ID
        /// </summary>
		public long BookmarkUserID{get;set;}
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public BookmarkComment ToModel()
        {
            return new BookmarkComment()
            {
                BookmarkCommentID =  BookmarkCommentID,
                CommentTitle =  CommentTitle,
                CommentContent =  CommentContent,
                BookmarkInfoID =  BookmarkInfoID,
                CriticsUserID =  CriticsUserID,
                CreateTime =  CreateTime,
                BookmarkUserID =  BookmarkUserID,
            };
        }
        
        
        public BizBookmarkComment (BookmarkComment dataInfo)
        {
             BookmarkCommentID =  dataInfo.BookmarkCommentID;
             CommentTitle =  dataInfo.CommentTitle;
             CommentContent =  dataInfo.CommentContent;
             BookmarkInfoID =  dataInfo.BookmarkInfoID;
             CriticsUserID =  dataInfo.CriticsUserID;
             CreateTime =  dataInfo.CreateTime;
             BookmarkUserID =  dataInfo.BookmarkUserID;
        }
        
        public  BizBookmarkComment ()
        {
        
        }


        public void Save()
        {
            if(BookmarkCommentID!=0)
            {
                new BookmarkCommentDAL().Update(ToModel());
            }else
            {
                new BookmarkCommentDAL().Add(ToModel());
            }
        }


        public static List<BizBookmarkComment> LoadByBookmarkInfoID(long bookmarkInfoID)
        {
            if (bookmarkInfoID == 0)
                return new List<BizBookmarkComment>();
            var lstModel = new BookmarkCommentDAL().GetByBookmarkInfoID(bookmarkInfoID);
            if (lstModel == null)
                return new List<BizBookmarkComment>();
            return lstModel.Select(model=>new BizBookmarkComment(model)).ToList();
        }
        
	}
}