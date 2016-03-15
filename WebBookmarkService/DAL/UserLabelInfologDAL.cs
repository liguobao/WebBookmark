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
	public partial class UserLabelInfologDAL
	{
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (UserLabelInfolog userLabelInfolog)
		{
				string sql ="INSERT INTO tblUserLabelInfolog (LabelName, UserInfoID, CreateTime)  VALUES (@LabelName, @UserInfoID, @CreateTime)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@LabelName", ToDBValue(userLabelInfolog.LabelName)),
						new MySqlParameter("@UserInfoID", ToDBValue(userLabelInfolog.UserInfoID)),
						new MySqlParameter("@CreateTime", ToDBValue(userLabelInfolog.CreateTime)),
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
        public int DeleteByUserLabelInfoID(long userLabelInfoID)
		{
            string sql = "DELETE from tblUserLabelInfolog WHERE UserLabelInfoID = @UserLabelInfoID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserLabelInfoID", userLabelInfoID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(UserLabelInfolog userLabelInfolog)
        {
            string sql =
                "UPDATE tblUserLabelInfolog " +
                "SET " +
			" LabelName = @LabelName" 
                +", UserInfoID = @UserInfoID" 
                +", CreateTime = @CreateTime" 
               
            +" WHERE UserLabelInfoID = @UserLabelInfoID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserLabelInfoID", userLabelInfolog.UserLabelInfoID)
					,new MySqlParameter("@LabelName", ToDBValue(userLabelInfolog.LabelName))
					,new MySqlParameter("@UserInfoID", ToDBValue(userLabelInfolog.UserInfoID))
					,new MySqlParameter("@CreateTime", ToDBValue(userLabelInfolog.CreateTime))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public UserLabelInfolog GetByUserLabelInfoID(long userLabelInfoID)
        {
            string sql = "SELECT * FROM tblUserLabelInfolog WHERE UserLabelInfoID = @UserLabelInfoID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserLabelInfoID", userLabelInfoID)))
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
		public UserLabelInfolog ToModel(MySqlDataReader dr)
		{
			UserLabelInfolog userLabelInfolog = new UserLabelInfolog();

			userLabelInfolog.UserLabelInfoID = (long)ToModelValue(dr,"UserLabelInfoID");
			userLabelInfolog.LabelName = (string)ToModelValue(dr,"LabelName");
			userLabelInfolog.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			userLabelInfolog.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			return userLabelInfolog;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblUserLabelInfolog";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<UserLabelInfolog> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by UserLabelInfoID))-1 rownum FROM tblUserLabelInfolog) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<UserLabelInfolog> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblUserLabelInfolog where "+columnName+"="+columnContent;
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
		public IEnumerable<UserLabelInfolog> GetAll()
		{
			string sql = "SELECT * FROM tblUserLabelInfolog";
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
		protected IEnumerable<UserLabelInfolog> ToModels(MySqlDataReader reader)
		{
			var list = new List<UserLabelInfolog>();
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