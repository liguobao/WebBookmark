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
	public partial class BookmarkInfoDAL
	{
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (BookmarkInfo bookmarkInfo)
		{
				string sql ="INSERT INTO tblBookmarkInfo (UserWebFolderID, UserInfoID, Href, HTML, Host, CreateTime, IElementJSON, BookmarkName)  VALUES (@UserWebFolderID, @UserInfoID, @Href, @HTML, @Host, @CreateTime, @IElementJSON, @BookmarkName)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@UserWebFolderID", ToDBValue(bookmarkInfo.UserWebFolderID)),
						new MySqlParameter("@UserInfoID", ToDBValue(bookmarkInfo.UserInfoID)),
						new MySqlParameter("@Href", ToDBValue(bookmarkInfo.Href)),
						new MySqlParameter("@HTML", ToDBValue(bookmarkInfo.HTML)),
						new MySqlParameter("@Host", ToDBValue(bookmarkInfo.Host)),
						new MySqlParameter("@CreateTime", ToDBValue(bookmarkInfo.CreateTime)),
						new MySqlParameter("@IElementJSON", ToDBValue(bookmarkInfo.IElementJSON)),
						new MySqlParameter("@BookmarkName", ToDBValue(bookmarkInfo.BookmarkName)),
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
        

        public bool BatchInsert(List<BookmarkInfo> lstBookmarkInfo)
        {
            StringBuilder sbSQL = new StringBuilder();
            int index = 0;
            List<MySqlParameter> lstPara = new List<MySqlParameter>();
            foreach(var bookmarkInfo in lstBookmarkInfo)
            {
                sbSQL.AppendLine( 
                    "INSERT INTO tblBookmarkInfo (UserWebFolderID, UserInfoID, Href, HTML, Host, CreateTime, IElementJSON, BookmarkName) " +
                    "VALUES ("+
                    "@UserWebFolderID" + index +
                    ", @UserInfoID"+ index +
                    ", @Href" + index +
                    ", @HTML" + index +
                    ", @Host" + index +
                    ", @CreateTime" + index +
                    ", @IElementJSON" + index +
                    ", @BookmarkName" + index +
                    ");");


                MySqlParameter[] para = new MySqlParameter[]
                {
                        new MySqlParameter("@UserWebFolderID"+index, ToDBValue(bookmarkInfo.UserWebFolderID)),
                        new MySqlParameter("@UserInfoID"+index, ToDBValue(bookmarkInfo.UserInfoID)),
                        new MySqlParameter("@Href"+index, ToDBValue(bookmarkInfo.Href)),
                        new MySqlParameter("@HTML"+index, ToDBValue(bookmarkInfo.HTML)),
                        new MySqlParameter("@Host"+index, ToDBValue(bookmarkInfo.Host)),
                        new MySqlParameter("@CreateTime"+index, ToDBValue(bookmarkInfo.CreateTime)),
                        new MySqlParameter("@IElementJSON"+index, ToDBValue(bookmarkInfo.IElementJSON)),
                        new MySqlParameter("@BookmarkName"+index, ToDBValue(bookmarkInfo.BookmarkName)),
                };
                lstPara.AddRange(para);
                index = index + 1;
            }
            return (int)MyDBHelper.ExecuteScalar(sbSQL.ToString(), lstPara.ToArray()) >0;
            
        }


        #region  
        /// <summary>
        /// 根据 WebFolderID 删除数据
        /// </summary>
        public int DeleteByWebFolderID(long webfolderID)
        {
            string sql = "DELETE from tblBookmarkInfo WHERE UserWebFolderID = @UserWebFolderID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserWebFolderID", webfolderID)
			};
            return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion



        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
        public int DeleteByBookmarkInfoID(long bookmarkInfoID)
		{
            string sql = "DELETE from tblBookmarkInfo WHERE BookmarkInfoID = @BookmarkInfoID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@BookmarkInfoID", bookmarkInfoID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(BookmarkInfo bookmarkInfo)
        {
            string sql =
                "UPDATE tblBookmarkInfo " +
                "SET " +
			" UserWebFolderID = @UserWebFolderID" 
                +", UserInfoID = @UserInfoID" 
                +", Href = @Href" 
                +", HTML = @HTML" 
                +", Host = @Host" 
                +", CreateTime = @CreateTime" 
                +", IElementJSON = @IElementJSON" 
                +", BookmarkName = @BookmarkName" 
               
            +" WHERE BookmarkInfoID = @BookmarkInfoID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@BookmarkInfoID", bookmarkInfo.BookmarkInfoID)
					,new MySqlParameter("@UserWebFolderID", ToDBValue(bookmarkInfo.UserWebFolderID))
					,new MySqlParameter("@UserInfoID", ToDBValue(bookmarkInfo.UserInfoID))
					,new MySqlParameter("@Href", ToDBValue(bookmarkInfo.Href))
					,new MySqlParameter("@HTML", ToDBValue(bookmarkInfo.HTML))
					,new MySqlParameter("@Host", ToDBValue(bookmarkInfo.Host))
					,new MySqlParameter("@CreateTime", ToDBValue(bookmarkInfo.CreateTime))
					,new MySqlParameter("@IElementJSON", ToDBValue(bookmarkInfo.IElementJSON))
					,new MySqlParameter("@BookmarkName", ToDBValue(bookmarkInfo.BookmarkName))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public BookmarkInfo GetByBookmarkInfoID(long bookmarkInfoID)
        {
            string sql = "SELECT * FROM tblBookmarkInfo WHERE BookmarkInfoID = @BookmarkInfoID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@BookmarkInfoID", bookmarkInfoID)))
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
		public BookmarkInfo ToModel(MySqlDataReader dr)
		{
			BookmarkInfo bookmarkInfo = new BookmarkInfo();

			bookmarkInfo.BookmarkInfoID = (long)ToModelValue(dr,"BookmarkInfoID");
			bookmarkInfo.UserWebFolderID = (long)ToModelValue(dr,"UserWebFolderID");
			bookmarkInfo.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			bookmarkInfo.Href = (string)ToModelValue(dr,"Href");
			bookmarkInfo.HTML = (string)ToModelValue(dr,"HTML");
			bookmarkInfo.Host = (string)ToModelValue(dr,"Host");
			bookmarkInfo.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			bookmarkInfo.IElementJSON = (string)ToModelValue(dr,"IElementJSON");
			bookmarkInfo.BookmarkName = (string)ToModelValue(dr,"BookmarkName");
			return bookmarkInfo;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblBookmarkInfo";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<BookmarkInfo> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by BookmarkInfoID))-1 rownum FROM tblBookmarkInfo) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<BookmarkInfo> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblBookmarkInfo where "+columnName+"="+columnContent;
			using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
			{
				return ToModels(reader);			
			}
		}
        #endregion

        #region 通过Uid 获取书签数据
        ///<summary>
        ///根据字段名获取数据记录IEnumerable<>
        ///</summary>              
        public IEnumerable<BookmarkInfo> GetListByUID(long uid)
        {
            string sql = "SELECT * FROM tblBookmarkInfo where UserInfoID=" + uid;
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
            {
                return ToModels(reader);
            }
        }

        /// <summary>
        /// 通过书签夹ID获取书签数据
        /// </summary>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public IEnumerable<BookmarkInfo> GetListByFolderID(long folderID)
        {
            string sql = "SELECT * FROM tblBookmarkInfo where UserWebFolderID=" + folderID;
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
            {
                return ToModels(reader);
            }
        }

        #endregion

        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary> 
        public IEnumerable<BookmarkInfo> GetAll()
		{
			string sql = "SELECT * FROM tblBookmarkInfo";
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
		protected IEnumerable<BookmarkInfo> ToModels(MySqlDataReader reader)
		{
			var list = new List<BookmarkInfo>();
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