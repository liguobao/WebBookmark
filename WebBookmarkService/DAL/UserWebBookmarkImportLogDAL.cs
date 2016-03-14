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
	public partial class UserWebBookmarkImportLogDAL
	{
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (UserWebBookmarkImportLog userWebBookmarkImportLog)
		{
				string sql ="INSERT INTO tblUserWebBookmarkImportLog (UserInfoID, Path, FileName, CreateTime)  VALUES (@UserInfoID, @Path, @FileName, @CreateTime)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@UserInfoID", ToDBValue(userWebBookmarkImportLog.UserInfoID)),
						new MySqlParameter("@Path", ToDBValue(userWebBookmarkImportLog.Path)),
						new MySqlParameter("@FileName", ToDBValue(userWebBookmarkImportLog.FileName)),
						new MySqlParameter("@CreateTime", ToDBValue(userWebBookmarkImportLog.CreateTime)),
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
        public int DeleteByUserWebBookmarkImportLogID(long userWebBookmarkImportLogID)
		{
            string sql = "DELETE from tblUserWebBookmarkImportLog WHERE UserWebBookmarkImportLogID = @UserWebBookmarkImportLogID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserWebBookmarkImportLogID", userWebBookmarkImportLogID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(UserWebBookmarkImportLog userWebBookmarkImportLog)
        {
            string sql =
                "UPDATE tblUserWebBookmarkImportLog " +
                "SET " +
			" UserInfoID = @UserInfoID" 
                +", Path = @Path" 
                +", FileName = @FileName" 
                +", CreateTime = @CreateTime" 
               
            +" WHERE UserWebBookmarkImportLogID = @UserWebBookmarkImportLogID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserWebBookmarkImportLogID", userWebBookmarkImportLog.UserWebBookmarkImportLogID)
					,new MySqlParameter("@UserInfoID", ToDBValue(userWebBookmarkImportLog.UserInfoID))
					,new MySqlParameter("@Path", ToDBValue(userWebBookmarkImportLog.Path))
					,new MySqlParameter("@FileName", ToDBValue(userWebBookmarkImportLog.FileName))
					,new MySqlParameter("@CreateTime", ToDBValue(userWebBookmarkImportLog.CreateTime))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public UserWebBookmarkImportLog GetByUserWebBookmarkImportLogID(long userWebBookmarkImportLogID)
        {
            string sql = "SELECT * FROM tblUserWebBookmarkImportLog WHERE UserWebBookmarkImportLogID = @UserWebBookmarkImportLogID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserWebBookmarkImportLogID", userWebBookmarkImportLogID)))
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
		public UserWebBookmarkImportLog ToModel(MySqlDataReader dr)
		{
			UserWebBookmarkImportLog userWebBookmarkImportLog = new UserWebBookmarkImportLog();

			userWebBookmarkImportLog.UserWebBookmarkImportLogID = (long)ToModelValue(dr,"UserWebBookmarkImportLogID");
			userWebBookmarkImportLog.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			userWebBookmarkImportLog.Path = (string)ToModelValue(dr,"Path");
			userWebBookmarkImportLog.FileName = (string)ToModelValue(dr,"FileName");
			userWebBookmarkImportLog.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			return userWebBookmarkImportLog;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblUserWebBookmarkImportLog";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<UserWebBookmarkImportLog> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by UserWebBookmarkImportLogID))-1 rownum FROM tblUserWebBookmarkImportLog) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<UserWebBookmarkImportLog> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblUserWebBookmarkImportLog where "+columnName+"="+columnContent;
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
		public IEnumerable<UserWebBookmarkImportLog> GetAll()
		{
			string sql = "SELECT * FROM tblUserWebBookmarkImportLog";
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
		protected IEnumerable<UserWebBookmarkImportLog> ToModels(MySqlDataReader reader)
		{
			var list = new List<UserWebBookmarkImportLog>();
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