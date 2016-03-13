//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using WebfolderService.Model;

namespace WebfolderService.DAL
{
	public partial class UserWebFolderImportLogDAL
	{
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (UserWebFolderImportLog userWebFolderImportLog)
		{
				string sql ="INSERT INTO tblUserWebFolderImportLog (UserInfoID, Path, FileName, CreateTime)  VALUES (@UserInfoID, @Path, @FileName, @CreateTime)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@UserInfoID", ToDBValue(userWebFolderImportLog.UserInfoID)),
						new MySqlParameter("@Path", ToDBValue(userWebFolderImportLog.Path)),
						new MySqlParameter("@FileName", ToDBValue(userWebFolderImportLog.FileName)),
						new MySqlParameter("@CreateTime", ToDBValue(userWebFolderImportLog.CreateTime)),
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
        public int DeleteByUserWebFolderImportLogID(long userWebFolderImportLogID)
		{
            string sql = "DELETE from tblUserWebFolderImportLog WHERE UserWebFolderImportLogID = @UserWebFolderImportLogID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserWebFolderImportLogID", userWebFolderImportLogID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(UserWebFolderImportLog userWebFolderImportLog)
        {
            string sql =
                "UPDATE tblUserWebFolderImportLog " +
                "SET " +
			" UserInfoID = @UserInfoID" 
                +", Path = @Path" 
                +", FileName = @FileName" 
                +", CreateTime = @CreateTime" 
               
            +" WHERE UserWebFolderImportLogID = @UserWebFolderImportLogID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserWebFolderImportLogID", userWebFolderImportLog.UserWebFolderImportLogID)
					,new MySqlParameter("@UserInfoID", ToDBValue(userWebFolderImportLog.UserInfoID))
					,new MySqlParameter("@Path", ToDBValue(userWebFolderImportLog.Path))
					,new MySqlParameter("@FileName", ToDBValue(userWebFolderImportLog.FileName))
					,new MySqlParameter("@CreateTime", ToDBValue(userWebFolderImportLog.CreateTime))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public UserWebFolderImportLog GetByUserWebFolderImportLogID(long userWebFolderImportLogID)
        {
            string sql = "SELECT * FROM tblUserWebFolderImportLog WHERE UserWebFolderImportLogID = @UserWebFolderImportLogID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserWebFolderImportLogID", userWebFolderImportLogID)))
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
		public UserWebFolderImportLog ToModel(MySqlDataReader dr)
		{
			UserWebFolderImportLog userWebFolderImportLog = new UserWebFolderImportLog();

			userWebFolderImportLog.UserWebFolderImportLogID = (long)ToModelValue(dr,"UserWebFolderImportLogID");
			userWebFolderImportLog.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			userWebFolderImportLog.Path = (string)ToModelValue(dr,"Path");
			userWebFolderImportLog.FileName = (string)ToModelValue(dr,"FileName");
			userWebFolderImportLog.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			return userWebFolderImportLog;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblUserWebFolderImportLog";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<UserWebFolderImportLog> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by UserWebFolderImportLogID))-1 rownum FROM tblUserWebFolderImportLog) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<UserWebFolderImportLog> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblUserWebFolderImportLog where "+columnName+"="+columnContent;
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
		public IEnumerable<UserWebFolderImportLog> GetAll()
		{
			string sql = "SELECT * FROM tblUserWebFolderImportLog";
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
		protected IEnumerable<UserWebFolderImportLog> ToModels(MySqlDataReader reader)
		{
			var list = new List<UserWebFolderImportLog>();
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