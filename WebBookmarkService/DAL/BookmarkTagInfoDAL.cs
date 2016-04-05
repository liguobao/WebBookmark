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
	public partial class BookmarkTagInfoDAL
	{
		#region 自动生成方法
		
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (BookmarkTagInfo bookmarkTagInfo)
		{
				string sql ="INSERT INTO tblBookmarkTagInfo (BookmarkInfoID, UserInfoID, CreateTime, TagInfoID)  VALUES (@BookmarkInfoID, @UserInfoID, @CreateTime, @TagInfoID)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@BookmarkInfoID", ToDBValue(bookmarkTagInfo.BookmarkInfoID)),
						new MySqlParameter("@UserInfoID", ToDBValue(bookmarkTagInfo.UserInfoID)),
						new MySqlParameter("@CreateTime", ToDBValue(bookmarkTagInfo.CreateTime)),
						new MySqlParameter("@TagInfoID", ToDBValue(bookmarkTagInfo.TagInfoID)),
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
        public int DeleteByBookmarkTagInfoID(long bookmarkTagInfoID)
		{
            string sql = "DELETE from tblBookmarkTagInfo WHERE BookmarkTagInfoID = @BookmarkTagInfoID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@BookmarkTagInfoID", bookmarkTagInfoID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(BookmarkTagInfo bookmarkTagInfo)
        {
            string sql =
                "UPDATE tblBookmarkTagInfo " +
                "SET " +
			" BookmarkInfoID = @BookmarkInfoID" 
                +", UserInfoID = @UserInfoID" 
                +", CreateTime = @CreateTime" 
                +", TagInfoID = @TagInfoID" 
               
            +" WHERE BookmarkTagInfoID = @BookmarkTagInfoID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@BookmarkTagInfoID", bookmarkTagInfo.BookmarkTagInfoID)
					,new MySqlParameter("@BookmarkInfoID", ToDBValue(bookmarkTagInfo.BookmarkInfoID))
					,new MySqlParameter("@UserInfoID", ToDBValue(bookmarkTagInfo.UserInfoID))
					,new MySqlParameter("@CreateTime", ToDBValue(bookmarkTagInfo.CreateTime))
					,new MySqlParameter("@TagInfoID", ToDBValue(bookmarkTagInfo.TagInfoID))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public BookmarkTagInfo GetByBookmarkTagInfoID(long bookmarkTagInfoID)
        {
            string sql = "SELECT * FROM tblBookmarkTagInfo WHERE BookmarkTagInfoID = @BookmarkTagInfoID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@BookmarkTagInfoID", bookmarkTagInfoID)))
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
		public BookmarkTagInfo ToModel(MySqlDataReader dr)
		{
			BookmarkTagInfo bookmarkTagInfo = new BookmarkTagInfo();

			bookmarkTagInfo.BookmarkTagInfoID = (long)ToModelValue(dr,"BookmarkTagInfoID");
			bookmarkTagInfo.BookmarkInfoID = (long)ToModelValue(dr,"BookmarkInfoID");
			bookmarkTagInfo.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			bookmarkTagInfo.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			bookmarkTagInfo.TagInfoID = (long)ToModelValue(dr,"TagInfoID");
			return bookmarkTagInfo;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblBookmarkTagInfo";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<BookmarkTagInfo> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by BookmarkTagInfoID))-1 rownum FROM tblBookmarkTagInfo) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<BookmarkTagInfo> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblBookmarkTagInfo where "+columnName+"="+columnContent;
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
		public IEnumerable<BookmarkTagInfo> GetAll()
		{
			string sql = "SELECT * FROM tblBookmarkTagInfo";
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
		protected IEnumerable<BookmarkTagInfo> ToModels(MySqlDataReader reader)
		{
			var list = new List<BookmarkTagInfo>();
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