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
	public partial class LikeLogDAL
	{
		#region 自动生成方法
		
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (LikeLog likeLog)
		{
				string sql ="INSERT INTO tblLikeLog (UserInfoID, InfoID, InfoType)  VALUES (@UserInfoID, @InfoID, @InfoType)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@UserInfoID", ToDBValue(likeLog.UserInfoID)),
						new MySqlParameter("@InfoID", ToDBValue(likeLog.InfoID)),
						new MySqlParameter("@InfoType", ToDBValue(likeLog.InfoType)),
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
        public int DeleteByLikeLogID(long likeLogID)
		{
            string sql = "DELETE from tblLikeLog WHERE LikeLogID = @LikeLogID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@LikeLogID", likeLogID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(LikeLog likeLog)
        {
            string sql =
                "UPDATE tblLikeLog " +
                "SET " +
			" UserInfoID = @UserInfoID" 
                +", InfoID = @InfoID" 
                +", InfoType = @InfoType" 
               
            +" WHERE LikeLogID = @LikeLogID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@LikeLogID", likeLog.LikeLogID)
					,new MySqlParameter("@UserInfoID", ToDBValue(likeLog.UserInfoID))
					,new MySqlParameter("@InfoID", ToDBValue(likeLog.InfoID))
					,new MySqlParameter("@InfoType", ToDBValue(likeLog.InfoType))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public LikeLog GetByLikeLogID(long likeLogID)
        {
            string sql = "SELECT * FROM tblLikeLog WHERE LikeLogID = @LikeLogID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@LikeLogID", likeLogID)))
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
		public LikeLog ToModel(MySqlDataReader dr)
		{
			LikeLog likeLog = new LikeLog();

			likeLog.LikeLogID = (long)ToModelValue(dr,"LikeLogID");
			likeLog.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			likeLog.InfoID = (long)ToModelValue(dr,"InfoID");
			likeLog.InfoType = (int)ToModelValue(dr,"InfoType");
			return likeLog;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblLikeLog";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<LikeLog> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by LikeLogID))-1 rownum FROM tblLikeLog) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<LikeLog> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblLikeLog where "+columnName+"="+columnContent;
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
		public IEnumerable<LikeLog> GetAll()
		{
			string sql = "SELECT * FROM tblLikeLog";
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
		protected IEnumerable<LikeLog> ToModels(MySqlDataReader reader)
		{
			var list = new List<LikeLog>();
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


        public IEnumerable<LikeLog> GetByInfoIDAndInfoType(long infoID, int infoType)
        {
            string sql = "SELECT * FROM tblLikeLog WHERE InfoID = @InfoID and InfoType=@InfoType";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@InfoID", infoID), new MySqlParameter("@InfoType", infoType)))
            {
                return ToModels(reader);
            }
        }

        public LikeLog GetByUserInfoIDAndInfoIDAndInfoType(long userID,long infoID,int type)
        {
            string sql = "SELECT * FROM tblLikeLog WHERE UserInfoID = @UserInfoID and InfoID=@InfoID and InfoType=@InfoType";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserInfoID", userID),
                new MySqlParameter("@InfoID", infoID), new MySqlParameter("@InfoType", type)))
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