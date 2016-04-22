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
	public partial class UserInfoDAL
	{
		#region 自动生成方法
		
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (UserInfo userInfo)
		{
				string sql ="INSERT INTO tblUserInfo (UserLoginName, UserPassword, UserName, UserEmail, UserPhone, UserQQ, CreateTime, UserImagURL, UserInfoComment, ActivateAccountToken, AccountStatus)  VALUES (@UserLoginName, @UserPassword, @UserName, @UserEmail, @UserPhone, @UserQQ, @CreateTime, @UserImagURL, @UserInfoComment, @ActivateAccountToken, @AccountStatus)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@UserLoginName", ToDBValue(userInfo.UserLoginName)),
						new MySqlParameter("@UserPassword", ToDBValue(userInfo.UserPassword)),
						new MySqlParameter("@UserName", ToDBValue(userInfo.UserName)),
						new MySqlParameter("@UserEmail", ToDBValue(userInfo.UserEmail)),
						new MySqlParameter("@UserPhone", ToDBValue(userInfo.UserPhone)),
						new MySqlParameter("@UserQQ", ToDBValue(userInfo.UserQQ)),
						new MySqlParameter("@CreateTime", ToDBValue(userInfo.CreateTime)),
						new MySqlParameter("@UserImagURL", ToDBValue(userInfo.UserImagURL)),
						new MySqlParameter("@UserInfoComment", ToDBValue(userInfo.UserInfoComment)),
						new MySqlParameter("@ActivateAccountToken", ToDBValue(userInfo.ActivateAccountToken)),
						new MySqlParameter("@AccountStatus", ToDBValue(userInfo.AccountStatus)),
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
        public int DeleteByUserInfoID(long userInfoID)
		{
            string sql = "DELETE from tblUserInfo WHERE UserInfoID = @UserInfoID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserInfoID", userInfoID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(UserInfo userInfo)
        {
            string sql =
                "UPDATE tblUserInfo " +
                "SET " +
			" UserLoginName = @UserLoginName" 
                +", UserPassword = @UserPassword" 
                +", UserName = @UserName" 
                +", UserEmail = @UserEmail" 
                +", UserPhone = @UserPhone" 
                +", UserQQ = @UserQQ" 
                +", CreateTime = @CreateTime" 
                +", UserImagURL = @UserImagURL" 
                +", UserInfoComment = @UserInfoComment" 
                +", ActivateAccountToken = @ActivateAccountToken" 
                +", AccountStatus = @AccountStatus" 
               
            +" WHERE UserInfoID = @UserInfoID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserInfoID", userInfo.UserInfoID)
					,new MySqlParameter("@UserLoginName", ToDBValue(userInfo.UserLoginName))
					,new MySqlParameter("@UserPassword", ToDBValue(userInfo.UserPassword))
					,new MySqlParameter("@UserName", ToDBValue(userInfo.UserName))
					,new MySqlParameter("@UserEmail", ToDBValue(userInfo.UserEmail))
					,new MySqlParameter("@UserPhone", ToDBValue(userInfo.UserPhone))
					,new MySqlParameter("@UserQQ", ToDBValue(userInfo.UserQQ))
					,new MySqlParameter("@CreateTime", ToDBValue(userInfo.CreateTime))
					,new MySqlParameter("@UserImagURL", ToDBValue(userInfo.UserImagURL))
					,new MySqlParameter("@UserInfoComment", ToDBValue(userInfo.UserInfoComment))
					,new MySqlParameter("@ActivateAccountToken", ToDBValue(userInfo.ActivateAccountToken))
					,new MySqlParameter("@AccountStatus", ToDBValue(userInfo.AccountStatus))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public UserInfo GetByUserInfoID(long userInfoID)
        {
            string sql = "SELECT * FROM tblUserInfo WHERE UserInfoID = @UserInfoID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserInfoID", userInfoID)))
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
		public UserInfo ToModel(MySqlDataReader dr)
		{
			UserInfo userInfo = new UserInfo();

			userInfo.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			userInfo.UserLoginName = (string)ToModelValue(dr,"UserLoginName");
			userInfo.UserPassword = (string)ToModelValue(dr,"UserPassword");
			userInfo.UserName = (string)ToModelValue(dr,"UserName");
			userInfo.UserEmail = (string)ToModelValue(dr,"UserEmail");
			userInfo.UserPhone = (string)ToModelValue(dr,"UserPhone");
			userInfo.UserQQ = (string)ToModelValue(dr,"UserQQ");
			userInfo.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			userInfo.UserImagURL = (string)ToModelValue(dr,"UserImagURL");
			userInfo.UserInfoComment = (string)ToModelValue(dr,"UserInfoComment");
			userInfo.ActivateAccountToken = (string)ToModelValue(dr,"ActivateAccountToken");
			userInfo.AccountStatus = (int)ToModelValue(dr,"AccountStatus");
			return userInfo;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblUserInfo";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<UserInfo> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by UserInfoID))-1 rownum FROM tblUserInfo) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<UserInfo> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblUserInfo where "+columnName+"="+columnContent;
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
		public IEnumerable<UserInfo> GetAll()
		{
			string sql = "SELECT * FROM tblUserInfo";
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
		protected IEnumerable<UserInfo> ToModels(MySqlDataReader reader)
		{
			var list = new List<UserInfo>();
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