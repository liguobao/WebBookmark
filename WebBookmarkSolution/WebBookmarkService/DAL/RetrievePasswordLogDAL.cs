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
	public partial class RetrievePasswordLogDAL
	{
		#region 自动生成方法
		
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (RetrievePasswordLog retrievePasswordLog)
		{
				string sql ="INSERT INTO tblRetrievePasswordLog (RetrievePasswordLogID, UserInfoID, LogStatus, Token, CreateTime, LastTime)  VALUES (@RetrievePasswordLogID, @UserInfoID, @LogStatus, @Token, @CreateTime, @LastTime)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@RetrievePasswordLogID", ToDBValue(retrievePasswordLog.RetrievePasswordLogID)),
						new MySqlParameter("@UserInfoID", ToDBValue(retrievePasswordLog.UserInfoID)),
						new MySqlParameter("@LogStatus", ToDBValue(retrievePasswordLog.LogStatus)),
						new MySqlParameter("@Token", ToDBValue(retrievePasswordLog.Token)),
						new MySqlParameter("@CreateTime", ToDBValue(retrievePasswordLog.CreateTime)),
						new MySqlParameter("@LastTime", ToDBValue(retrievePasswordLog.LastTime)),
					};
				int AddId = (int)MyDBHelper.ExecuteNonQuery(sql, para);
				if(AddId > 1)
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
        public int DeleteByRetrievePasswordLogID(long retrievePasswordLogID)
		{
            string sql = "DELETE from tblRetrievePasswordLog WHERE RetrievePasswordLogID = @RetrievePasswordLogID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@RetrievePasswordLogID", retrievePasswordLogID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(RetrievePasswordLog retrievePasswordLog)
        {
            string sql =
                "UPDATE tblRetrievePasswordLog " +
                "SET " +
			" UserInfoID = @UserInfoID" 
                +", LogStatus = @LogStatus" 
                +", Token = @Token" 
                +", CreateTime = @CreateTime" 
                +", LastTime = @LastTime" 
               
            +" WHERE RetrievePasswordLogID = @RetrievePasswordLogID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@RetrievePasswordLogID", retrievePasswordLog.RetrievePasswordLogID)
					,new MySqlParameter("@UserInfoID", ToDBValue(retrievePasswordLog.UserInfoID))
					,new MySqlParameter("@LogStatus", ToDBValue(retrievePasswordLog.LogStatus))
					,new MySqlParameter("@Token", ToDBValue(retrievePasswordLog.Token))
					,new MySqlParameter("@CreateTime", ToDBValue(retrievePasswordLog.CreateTime))
					,new MySqlParameter("@LastTime", ToDBValue(retrievePasswordLog.LastTime))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public RetrievePasswordLog GetByRetrievePasswordLogID(long retrievePasswordLogID)
        {
            string sql = "SELECT * FROM tblRetrievePasswordLog WHERE RetrievePasswordLogID = @RetrievePasswordLogID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@RetrievePasswordLogID", retrievePasswordLogID)))
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
		public RetrievePasswordLog ToModel(MySqlDataReader dr)
		{
			RetrievePasswordLog retrievePasswordLog = new RetrievePasswordLog();

			retrievePasswordLog.RetrievePasswordLogID = (long)ToModelValue(dr,"RetrievePasswordLogID");
			retrievePasswordLog.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			retrievePasswordLog.LogStatus = (int)ToModelValue(dr,"LogStatus");
			retrievePasswordLog.Token = (string)ToModelValue(dr,"Token");
			retrievePasswordLog.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			retrievePasswordLog.LastTime = (DateTime)ToModelValue(dr,"LastTime");
			return retrievePasswordLog;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblRetrievePasswordLog";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<RetrievePasswordLog> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by RetrievePasswordLogID))-1 rownum FROM tblRetrievePasswordLog) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<RetrievePasswordLog> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblRetrievePasswordLog where "+columnName+"="+columnContent;
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
		public IEnumerable<RetrievePasswordLog> GetAll()
		{
			string sql = "SELECT * FROM tblRetrievePasswordLog";
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
		protected IEnumerable<RetrievePasswordLog> ToModels(MySqlDataReader reader)
		{
			var list = new List<RetrievePasswordLog>();
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




        public RetrievePasswordLog GetByToken(string token)
        {
            string sql = "SELECT * FROM tblRetrievePasswordLog WHERE Token = @Token";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@Token", token)))
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
	}
}