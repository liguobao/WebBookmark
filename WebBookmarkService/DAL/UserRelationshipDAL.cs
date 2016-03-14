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
	public partial class UserRelationshipDAL
	{
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (UserRelationship userRelationship)
		{
				string sql ="INSERT INTO tblUserRelationship (FollowerID, BeFollwedUID, IsMutuallyFollwe, CreateTime)  VALUES (@FollowerID, @BeFollwedUID, @IsMutuallyFollwe, @CreateTime)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@FollowerID", ToDBValue(userRelationship.FollowerID)),
						new MySqlParameter("@BeFollwedUID", ToDBValue(userRelationship.BeFollwedUID)),
						new MySqlParameter("@IsMutuallyFollwe", ToDBValue(userRelationship.IsMutuallyFollwe)),
						new MySqlParameter("@CreateTime", ToDBValue(userRelationship.CreateTime)),
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
        public int DeleteByUserRelationshipID(long userRelationshipID)
		{
            string sql = "DELETE from tblUserRelationship WHERE UserRelationshipID = @UserRelationshipID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserRelationshipID", userRelationshipID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(UserRelationship userRelationship)
        {
            string sql =
                "UPDATE tblUserRelationship " +
                "SET " +
			" FollowerID = @FollowerID" 
                +", BeFollwedUID = @BeFollwedUID" 
                +", IsMutuallyFollwe = @IsMutuallyFollwe" 
                +", CreateTime = @CreateTime" 
               
            +" WHERE UserRelationshipID = @UserRelationshipID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserRelationshipID", userRelationship.UserRelationshipID)
					,new MySqlParameter("@FollowerID", ToDBValue(userRelationship.FollowerID))
					,new MySqlParameter("@BeFollwedUID", ToDBValue(userRelationship.BeFollwedUID))
					,new MySqlParameter("@IsMutuallyFollwe", ToDBValue(userRelationship.IsMutuallyFollwe))
					,new MySqlParameter("@CreateTime", ToDBValue(userRelationship.CreateTime))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public UserRelationship GetByUserRelationshipID(long userRelationshipID)
        {
            string sql = "SELECT * FROM tblUserRelationship WHERE UserRelationshipID = @UserRelationshipID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserRelationshipID", userRelationshipID)))
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
		public UserRelationship ToModel(MySqlDataReader dr)
		{
			UserRelationship userRelationship = new UserRelationship();

			userRelationship.UserRelationshipID = (long)ToModelValue(dr,"UserRelationshipID");
			userRelationship.FollowerID = (long)ToModelValue(dr,"FollowerID");
			userRelationship.BeFollwedUID = (long)ToModelValue(dr,"BeFollwedUID");
			userRelationship.IsMutuallyFollwe = (sbyte)ToModelValue(dr,"IsMutuallyFollwe");
			userRelationship.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			return userRelationship;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblUserRelationship";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<UserRelationship> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by UserRelationshipID))-1 rownum FROM tblUserRelationship) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<UserRelationship> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblUserRelationship where "+columnName+"="+columnContent;
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
		public IEnumerable<UserRelationship> GetAll()
		{
			string sql = "SELECT * FROM tblUserRelationship";
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
		protected IEnumerable<UserRelationship> ToModels(MySqlDataReader reader)
		{
			var list = new List<UserRelationship>();
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