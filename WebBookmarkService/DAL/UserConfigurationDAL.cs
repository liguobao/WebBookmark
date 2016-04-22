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
	public partial class UserConfigurationDAL
	{
		#region 自动生成方法
		
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (UserConfiguration userConfiguration)
		{
				string sql ="INSERT INTO tblUserConfiguration (UserInfoID, UserConfigurationKey, UserConfigurationValue, Description, UserConfigurationNo)  VALUES (@UserInfoID, @UserConfigurationKey, @UserConfigurationValue, @Description, @UserConfigurationNo)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@UserInfoID", ToDBValue(userConfiguration.UserInfoID)),
						new MySqlParameter("@UserConfigurationKey", ToDBValue(userConfiguration.UserConfigurationKey)),
						new MySqlParameter("@UserConfigurationValue", ToDBValue(userConfiguration.UserConfigurationValue)),
						new MySqlParameter("@Description", ToDBValue(userConfiguration.Description)),
						new MySqlParameter("@UserConfigurationNo", ToDBValue(userConfiguration.UserConfigurationNo)),
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
        public int DeleteByUserConfigurationID(long userConfigurationID)
		{
            string sql = "DELETE from tblUserConfiguration WHERE UserConfigurationID = @UserConfigurationID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserConfigurationID", userConfigurationID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(UserConfiguration userConfiguration)
        {
            string sql =
                "UPDATE tblUserConfiguration " +
                "SET " +
			" UserInfoID = @UserInfoID" 
                +", UserConfigurationKey = @UserConfigurationKey" 
                +", UserConfigurationValue = @UserConfigurationValue" 
                +", Description = @Description" 
                +", UserConfigurationNo = @UserConfigurationNo" 
               
            +" WHERE UserConfigurationID = @UserConfigurationID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserConfigurationID", userConfiguration.UserConfigurationID)
					,new MySqlParameter("@UserInfoID", ToDBValue(userConfiguration.UserInfoID))
					,new MySqlParameter("@UserConfigurationKey", ToDBValue(userConfiguration.UserConfigurationKey))
					,new MySqlParameter("@UserConfigurationValue", ToDBValue(userConfiguration.UserConfigurationValue))
					,new MySqlParameter("@Description", ToDBValue(userConfiguration.Description))
					,new MySqlParameter("@UserConfigurationNo", ToDBValue(userConfiguration.UserConfigurationNo))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public UserConfiguration GetByUserConfigurationID(long userConfigurationID)
        {
            string sql = "SELECT * FROM tblUserConfiguration WHERE UserConfigurationID = @UserConfigurationID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserConfigurationID", userConfigurationID)))
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
		public UserConfiguration ToModel(MySqlDataReader dr)
		{
			UserConfiguration userConfiguration = new UserConfiguration();

			userConfiguration.UserConfigurationID = (long)ToModelValue(dr,"UserConfigurationID");
			userConfiguration.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			userConfiguration.UserConfigurationKey = (string)ToModelValue(dr,"UserConfigurationKey");
			userConfiguration.UserConfigurationValue = (string)ToModelValue(dr,"UserConfigurationValue");
			userConfiguration.Description = (string)ToModelValue(dr,"Description");
			userConfiguration.UserConfigurationNo = (int)ToModelValue(dr,"UserConfigurationNo");
			return userConfiguration;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblUserConfiguration";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<UserConfiguration> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by UserConfigurationID))-1 rownum FROM tblUserConfiguration) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<UserConfiguration> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblUserConfiguration where "+columnName+"="+columnContent;
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
		public IEnumerable<UserConfiguration> GetAll()
		{
			string sql = "SELECT * FROM tblUserConfiguration";
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
		protected IEnumerable<UserConfiguration> ToModels(MySqlDataReader reader)
		{
			var list = new List<UserConfiguration>();
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