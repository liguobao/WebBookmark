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
	public partial class SystemLogDAL
	{
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (SystemLog systemLog)
		{
				string sql ="INSERT INTO tblSystemLog (LogTitle, LogContent, LogType, CreateTime, DynamicInfo)  VALUES (@LogTitle, @LogContent, @LogType, @CreateTime, @DynamicInfo)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@LogTitle", ToDBValue(systemLog.LogTitle)),
						new MySqlParameter("@LogContent", ToDBValue(systemLog.LogContent)),
						new MySqlParameter("@LogType", ToDBValue(systemLog.LogType)),
						new MySqlParameter("@CreateTime", ToDBValue(systemLog.CreateTime)),
						new MySqlParameter("@DynamicInfo", ToDBValue(systemLog.DynamicInfo)),
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
        public int DeleteBySystemLogID(long systemLogID)
		{
            string sql = "DELETE from tblSystemLog WHERE SystemLogID = @SystemLogID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@SystemLogID", systemLogID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(SystemLog systemLog)
        {
            string sql =
                "UPDATE tblSystemLog " +
                "SET " +
			" LogTitle = @LogTitle" 
                +", LogContent = @LogContent" 
                +", LogType = @LogType" 
                +", CreateTime = @CreateTime" 
                +", DynamicInfo = @DynamicInfo" 
               
            +" WHERE SystemLogID = @SystemLogID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@SystemLogID", systemLog.SystemLogID)
					,new MySqlParameter("@LogTitle", ToDBValue(systemLog.LogTitle))
					,new MySqlParameter("@LogContent", ToDBValue(systemLog.LogContent))
					,new MySqlParameter("@LogType", ToDBValue(systemLog.LogType))
					,new MySqlParameter("@CreateTime", ToDBValue(systemLog.CreateTime))
					,new MySqlParameter("@DynamicInfo", ToDBValue(systemLog.DynamicInfo))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public SystemLog GetBySystemLogID(long systemLogID)
        {
            string sql = "SELECT * FROM tblSystemLog WHERE SystemLogID = @SystemLogID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@SystemLogID", systemLogID)))
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
		public SystemLog ToModel(MySqlDataReader dr)
		{
			SystemLog systemLog = new SystemLog();

			systemLog.SystemLogID = (long)ToModelValue(dr,"SystemLogID");
			systemLog.LogTitle = (string)ToModelValue(dr,"LogTitle");
			systemLog.LogContent = (string)ToModelValue(dr,"LogContent");
			systemLog.LogType = (int)ToModelValue(dr,"LogType");
			systemLog.CreateTime = (DateTime?)ToModelValue(dr,"CreateTime");
			systemLog.DynamicInfo = (string)ToModelValue(dr,"DynamicInfo");
			return systemLog;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblSystemLog";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<SystemLog> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by SystemLogID))-1 rownum FROM tblSystemLog) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<SystemLog> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblSystemLog where "+columnName+"="+columnContent;
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
		public IEnumerable<SystemLog> GetAll()
		{
			string sql = "SELECT * FROM tblSystemLog";
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
		protected IEnumerable<SystemLog> ToModels(MySqlDataReader reader)
		{
			var list = new List<SystemLog>();
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