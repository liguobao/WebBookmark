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
	public partial class ConfigurationInfoDAL
	{
		#region 自动生成方法
		
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (ConfigurationInfo configurationInfo)
		{
				string sql ="INSERT INTO tblConfigurationInfo (ConfigurationKey, ConfigurationValue, Description, ConfigurationNo, HashCode)  VALUES (@ConfigurationKey, @ConfigurationValue, @Description, @ConfigurationNo, @HashCode)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@ConfigurationKey", ToDBValue(configurationInfo.ConfigurationKey)),
						new MySqlParameter("@ConfigurationValue", ToDBValue(configurationInfo.ConfigurationValue)),
						new MySqlParameter("@Description", ToDBValue(configurationInfo.Description)),
						new MySqlParameter("@ConfigurationNo", ToDBValue(configurationInfo.ConfigurationNo)),
						new MySqlParameter("@HashCode", ToDBValue(configurationInfo.HashCode)),
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
        public int DeleteByConfigurationInfoID(int configurationInfoID)
		{
            string sql = "DELETE from tblConfigurationInfo WHERE ConfigurationInfoID = @ConfigurationInfoID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@ConfigurationInfoID", configurationInfoID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(ConfigurationInfo configurationInfo)
        {
            string sql =
                "UPDATE tblConfigurationInfo " +
                "SET " +
			" ConfigurationKey = @ConfigurationKey" 
                +", ConfigurationValue = @ConfigurationValue" 
                +", Description = @Description" 
                +", ConfigurationNo = @ConfigurationNo" 
                +", HashCode = @HashCode" 
               
            +" WHERE ConfigurationInfoID = @ConfigurationInfoID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@ConfigurationInfoID", configurationInfo.ConfigurationInfoID)
					,new MySqlParameter("@ConfigurationKey", ToDBValue(configurationInfo.ConfigurationKey))
					,new MySqlParameter("@ConfigurationValue", ToDBValue(configurationInfo.ConfigurationValue))
					,new MySqlParameter("@Description", ToDBValue(configurationInfo.Description))
					,new MySqlParameter("@ConfigurationNo", ToDBValue(configurationInfo.ConfigurationNo))
					,new MySqlParameter("@HashCode", ToDBValue(configurationInfo.HashCode))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public ConfigurationInfo GetByConfigurationInfoID(int configurationInfoID)
        {
            string sql = "SELECT * FROM tblConfigurationInfo WHERE ConfigurationInfoID = @ConfigurationInfoID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@ConfigurationInfoID", configurationInfoID)))
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
		public ConfigurationInfo ToModel(MySqlDataReader dr)
		{
			ConfigurationInfo configurationInfo = new ConfigurationInfo();

			configurationInfo.ConfigurationInfoID = (int)ToModelValue(dr,"ConfigurationInfoID");
			configurationInfo.ConfigurationKey = (string)ToModelValue(dr,"ConfigurationKey");
			configurationInfo.ConfigurationValue = (string)ToModelValue(dr,"ConfigurationValue");
			configurationInfo.Description = (string)ToModelValue(dr,"Description");
			configurationInfo.ConfigurationNo = (int)ToModelValue(dr,"ConfigurationNo");
			configurationInfo.HashCode = (int)ToModelValue(dr,"HashCode");
			return configurationInfo;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblConfigurationInfo";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<ConfigurationInfo> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by ConfigurationInfoID))-1 rownum FROM tblConfigurationInfo) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<ConfigurationInfo> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblConfigurationInfo where "+columnName+"="+columnContent;
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
		public IEnumerable<ConfigurationInfo> GetAll()
		{
			string sql = "SELECT * FROM tblConfigurationInfo";
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
		protected IEnumerable<ConfigurationInfo> ToModels(MySqlDataReader reader)
		{
			var list = new List<ConfigurationInfo>();
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