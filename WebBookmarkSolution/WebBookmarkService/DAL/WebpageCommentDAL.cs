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
	public partial class WebpageCommentDAL
	{
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (WebpageComment webpageComment)
		{
				string sql ="INSERT INTO tblWebpageComment (CommentUserID, URLInfoID, WebpageUserInfoID, CommentTitle, CommentContent, CommentType, CreateTime)  VALUES (@CommentUserID, @URLInfoID, @WebpageUserInfoID, @CommentTitle, @CommentContent, @CommentType, @CreateTime)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@CommentUserID", ToDBValue(webpageComment.CommentUserID)),
						new MySqlParameter("@URLInfoID", ToDBValue(webpageComment.URLInfoID)),
						new MySqlParameter("@WebpageUserInfoID", ToDBValue(webpageComment.WebpageUserInfoID)),
						new MySqlParameter("@CommentTitle", ToDBValue(webpageComment.CommentTitle)),
						new MySqlParameter("@CommentContent", ToDBValue(webpageComment.CommentContent)),
						new MySqlParameter("@CommentType", ToDBValue(webpageComment.CommentType)),
						new MySqlParameter("@CreateTime", ToDBValue(webpageComment.CreateTime)),
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
        public int DeleteByWebpageCommentID(long webpageCommentID)
		{
            string sql = "DELETE from tblWebpageComment WHERE WebpageCommentID = @WebpageCommentID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@WebpageCommentID", webpageCommentID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(WebpageComment webpageComment)
        {
            string sql =
                "UPDATE tblWebpageComment " +
                "SET " +
			" CommentUserID = @CommentUserID" 
                +", URLInfoID = @URLInfoID" 
                +", WebpageUserInfoID = @WebpageUserInfoID" 
                +", CommentTitle = @CommentTitle" 
                +", CommentContent = @CommentContent" 
                +", CommentType = @CommentType" 
                +", CreateTime = @CreateTime" 
               
            +" WHERE WebpageCommentID = @WebpageCommentID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@WebpageCommentID", webpageComment.WebpageCommentID)
					,new MySqlParameter("@CommentUserID", ToDBValue(webpageComment.CommentUserID))
					,new MySqlParameter("@URLInfoID", ToDBValue(webpageComment.URLInfoID))
					,new MySqlParameter("@WebpageUserInfoID", ToDBValue(webpageComment.WebpageUserInfoID))
					,new MySqlParameter("@CommentTitle", ToDBValue(webpageComment.CommentTitle))
					,new MySqlParameter("@CommentContent", ToDBValue(webpageComment.CommentContent))
					,new MySqlParameter("@CommentType", ToDBValue(webpageComment.CommentType))
					,new MySqlParameter("@CreateTime", ToDBValue(webpageComment.CreateTime))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public WebpageComment GetByWebpageCommentID(long webpageCommentID)
        {
            string sql = "SELECT * FROM tblWebpageComment WHERE WebpageCommentID = @WebpageCommentID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@WebpageCommentID", webpageCommentID)))
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
		public WebpageComment ToModel(MySqlDataReader dr)
		{
			WebpageComment webpageComment = new WebpageComment();

			webpageComment.WebpageCommentID = (long)ToModelValue(dr,"WebpageCommentID");
			webpageComment.CommentUserID = (long)ToModelValue(dr,"CommentUserID");
			webpageComment.URLInfoID = (long)ToModelValue(dr,"URLInfoID");
			webpageComment.WebpageUserInfoID = (long)ToModelValue(dr,"WebpageUserInfoID");
			webpageComment.CommentTitle = (string)ToModelValue(dr,"CommentTitle");
			webpageComment.CommentContent = (string)ToModelValue(dr,"CommentContent");
			webpageComment.CommentType = (int)ToModelValue(dr,"CommentType");
			webpageComment.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			return webpageComment;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblWebpageComment";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<WebpageComment> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by WebpageCommentID))-1 rownum FROM tblWebpageComment) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<WebpageComment> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblWebpageComment where "+columnName+"="+columnContent;
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
		public IEnumerable<WebpageComment> GetAll()
		{
			string sql = "SELECT * FROM tblWebpageComment";
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
		protected IEnumerable<WebpageComment> ToModels(MySqlDataReader reader)
		{
			var list = new List<WebpageComment>();
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
	}
}