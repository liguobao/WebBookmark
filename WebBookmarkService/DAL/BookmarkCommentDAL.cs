//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using WebBookmarkService.Model;

namespace WebBookmarkService.DAL
{
	public partial class BookmarkCommentDAL
	{
		#region 自动生成方法
		
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (BookmarkComment bookmarkComment)
		{
				string sql ="INSERT INTO tblBookmarkComment (CommentTitle, CommentContent, BookmarkInfoID, CriticsUserID, CreateTime, BookmarkUserID)  VALUES (@CommentTitle, @CommentContent, @BookmarkInfoID, @CriticsUserID, @CreateTime, @BookmarkUserID)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@CommentTitle", ToDBValue(bookmarkComment.CommentTitle)),
						new MySqlParameter("@CommentContent", ToDBValue(bookmarkComment.CommentContent)),
						new MySqlParameter("@BookmarkInfoID", ToDBValue(bookmarkComment.BookmarkInfoID)),
						new MySqlParameter("@CriticsUserID", ToDBValue(bookmarkComment.CriticsUserID)),
						new MySqlParameter("@CreateTime", ToDBValue(bookmarkComment.CreateTime)),
						new MySqlParameter("@BookmarkUserID", ToDBValue(bookmarkComment.BookmarkUserID)),
					};
					
				int AddId = (int)MyDBHelper.ExecuteScalar(sql, para);
				if(AddId==1)
				{
					return true;
				}else
				{
					return false;					
				}
		}
         #endregion
         

        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
        public int DeleteByBookmarkCommentID(long bookmarkCommentID)
		{
            string sql = "DELETE from tblBookmarkComment WHERE BookmarkCommentID = @BookmarkCommentID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@BookmarkCommentID", bookmarkCommentID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(BookmarkComment bookmarkComment)
        {
            string sql =
                "UPDATE tblBookmarkComment " +
                "SET " +
			" CommentTitle = @CommentTitle" 
                +", CommentContent = @CommentContent" 
                +", BookmarkInfoID = @BookmarkInfoID" 
                +", CriticsUserID = @CriticsUserID" 
                +", CreateTime = @CreateTime" 
                +", BookmarkUserID = @BookmarkUserID" 
               
            +" WHERE BookmarkCommentID = @BookmarkCommentID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@BookmarkCommentID", bookmarkComment.BookmarkCommentID)
					,new MySqlParameter("@CommentTitle", ToDBValue(bookmarkComment.CommentTitle))
					,new MySqlParameter("@CommentContent", ToDBValue(bookmarkComment.CommentContent))
					,new MySqlParameter("@BookmarkInfoID", ToDBValue(bookmarkComment.BookmarkInfoID))
					,new MySqlParameter("@CriticsUserID", ToDBValue(bookmarkComment.CriticsUserID))
					,new MySqlParameter("@CreateTime", ToDBValue(bookmarkComment.CreateTime))
					,new MySqlParameter("@BookmarkUserID", ToDBValue(bookmarkComment.BookmarkUserID))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public BookmarkComment GetByBookmarkCommentID(long bookmarkCommentID)
        {
            string sql = "SELECT * FROM tblBookmarkComment WHERE BookmarkCommentID = @BookmarkCommentID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@BookmarkCommentID", bookmarkCommentID)))
			{
				if (reader.Read())
				{
					return ToModel(reader);
				}
				else
				{
					return null;
				}
       		}
        }
		#endregion
        
        #region 把DataRow转换成Model
        /// <summary>
        /// 把DataRow转换成Model
        /// </summary>
		public BookmarkComment ToModel(MySqlDataReader dr)
		{
			BookmarkComment bookmarkComment = new BookmarkComment();

			bookmarkComment.BookmarkCommentID = (long)ToModelValue(dr,"BookmarkCommentID");
			bookmarkComment.CommentTitle = (string)ToModelValue(dr,"CommentTitle");
			bookmarkComment.CommentContent = (string)ToModelValue(dr,"CommentContent");
			bookmarkComment.BookmarkInfoID = (long)ToModelValue(dr,"BookmarkInfoID");
			bookmarkComment.CriticsUserID = (long)ToModelValue(dr,"CriticsUserID");
			bookmarkComment.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			bookmarkComment.BookmarkUserID = (long)ToModelValue(dr,"BookmarkUserID");
			return bookmarkComment;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblBookmarkComment";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<BookmarkComment> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by BookmarkCommentID))-1 rownum FROM tblBookmarkComment) t where rownum>=@minrownum and rownum<=@maxrownum";
			using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql,
				new MySqlParameter("@minrownum",minrownum),
				new MySqlParameter("@maxrownum",maxrownum)))
			{
				return ToModels(reader);					
			}
		}
		#endregion
        
        
        #region 根据字段名获取数据记录IEnumerable<>
        ///<summary>
        ///根据字段名获取数据记录IEnumerable<>
        ///</summary>              
		public IEnumerable<BookmarkComment> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblBookmarkComment where "+columnName+"="+columnContent;
			using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
			{
				return ToModels(reader);			
			}
		}
		#endregion
        
        
        
        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary> 
		public IEnumerable<BookmarkComment> GetAll()
		{
			string sql = "SELECT * FROM tblBookmarkComment";
			using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
			{
				return ToModels(reader);			
			}
		}
        #endregion
		
        #region 把MySqlDataReader转换成IEnumerable<>
        ///<summary>
        /// 把MySqlDataReader转换成IEnumerable<>
        ///</summary> 
		protected IEnumerable<BookmarkComment> ToModels(MySqlDataReader reader)
		{
			var list = new List<BookmarkComment>();
			while(reader.Read())
			{
				list.Add(ToModel(reader));
			}	
			return list;
		}		
		#endregion
        
        #region 判断数据是否为空
        ///<summary>
        /// 判断数据是否为空
        ///</summary>
		protected object ToDBValue(object value)
		{
			if(value==null)
			{
				return DBNull.Value;
			}
			else
			{
				return value;
			}
		}
		#endregion
        
        #region 判断数据表中是否包含该字段
        ///<summary>
        /// 判断数据表中是否包含该字段
        ///</summary>
		protected object ToModelValue(MySqlDataReader reader,string columnName)
		{
			if(reader.IsDBNull(reader.GetOrdinal(columnName)))
			{
				return null;
			}
			else
			{
				return reader[columnName];
			}
		}
        #endregion
	
	    #endregion
	}
}