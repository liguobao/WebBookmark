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
	public partial class UserWebpageLabelDAL
	{
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (UserWebpageLabel userWebpageLabel)
		{
				string sql ="INSERT INTO tblUserWebpageLabel (URLInfoID, UserInfoID, CreateTime)  VALUES (@URLInfoID, @UserInfoID, @CreateTime)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@URLInfoID", ToDBValue(userWebpageLabel.URLInfoID)),
						new MySqlParameter("@UserInfoID", ToDBValue(userWebpageLabel.UserInfoID)),
						new MySqlParameter("@CreateTime", ToDBValue(userWebpageLabel.CreateTime)),
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
        public int DeleteByUserWebpageLabelID(long userWebpageLabelID)
		{
            string sql = "DELETE from tblUserWebpageLabel WHERE UserWebpageLabelID = @UserWebpageLabelID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserWebpageLabelID", userWebpageLabelID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(UserWebpageLabel userWebpageLabel)
        {
            string sql =
                "UPDATE tblUserWebpageLabel " +
                "SET " +
			" URLInfoID = @URLInfoID" 
                +", UserInfoID = @UserInfoID" 
                +", CreateTime = @CreateTime" 
               
            +" WHERE UserWebpageLabelID = @UserWebpageLabelID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserWebpageLabelID", userWebpageLabel.UserWebpageLabelID)
					,new MySqlParameter("@URLInfoID", ToDBValue(userWebpageLabel.URLInfoID))
					,new MySqlParameter("@UserInfoID", ToDBValue(userWebpageLabel.UserInfoID))
					,new MySqlParameter("@CreateTime", ToDBValue(userWebpageLabel.CreateTime))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public UserWebpageLabel GetByUserWebpageLabelID(long userWebpageLabelID)
        {
            string sql = "SELECT * FROM tblUserWebpageLabel WHERE UserWebpageLabelID = @UserWebpageLabelID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserWebpageLabelID", userWebpageLabelID)))
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
		public UserWebpageLabel ToModel(MySqlDataReader dr)
		{
			UserWebpageLabel userWebpageLabel = new UserWebpageLabel();

			userWebpageLabel.UserWebpageLabelID = (long)ToModelValue(dr,"UserWebpageLabelID");
			userWebpageLabel.URLInfoID = (long)ToModelValue(dr,"URLInfoID");
			userWebpageLabel.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			userWebpageLabel.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			return userWebpageLabel;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblUserWebpageLabel";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<UserWebpageLabel> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by UserWebpageLabelID))-1 rownum FROM tblUserWebpageLabel) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<UserWebpageLabel> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblUserWebpageLabel where "+columnName+"="+columnContent;
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
		public IEnumerable<UserWebpageLabel> GetAll()
		{
			string sql = "SELECT * FROM tblUserWebpageLabel";
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
		protected IEnumerable<UserWebpageLabel> ToModels(MySqlDataReader reader)
		{
			var list = new List<UserWebpageLabel>();
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