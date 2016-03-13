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
	public partial class GroupUserDAL
	{
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (GroupUser groupUser)
		{
				string sql ="INSERT INTO tblGroupUser (GroupID, UserInfoID, IsPass, CreateTime)  VALUES (@GroupID, @UserInfoID, @IsPass, @CreateTime)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@GroupID", ToDBValue(groupUser.GroupID)),
						new MySqlParameter("@UserInfoID", ToDBValue(groupUser.UserInfoID)),
						new MySqlParameter("@IsPass", ToDBValue(groupUser.IsPass)),
						new MySqlParameter("@CreateTime", ToDBValue(groupUser.CreateTime)),
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
        public int DeleteByGroupUserID(long groupUserID)
		{
            string sql = "DELETE from tblGroupUser WHERE GroupUserID = @GroupUserID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@GroupUserID", groupUserID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(GroupUser groupUser)
        {
            string sql =
                "UPDATE tblGroupUser " +
                "SET " +
			" GroupID = @GroupID" 
                +", UserInfoID = @UserInfoID" 
                +", IsPass = @IsPass" 
                +", CreateTime = @CreateTime" 
               
            +" WHERE GroupUserID = @GroupUserID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@GroupUserID", groupUser.GroupUserID)
					,new MySqlParameter("@GroupID", ToDBValue(groupUser.GroupID))
					,new MySqlParameter("@UserInfoID", ToDBValue(groupUser.UserInfoID))
					,new MySqlParameter("@IsPass", ToDBValue(groupUser.IsPass))
					,new MySqlParameter("@CreateTime", ToDBValue(groupUser.CreateTime))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public GroupUser GetByGroupUserID(long groupUserID)
        {
            string sql = "SELECT * FROM tblGroupUser WHERE GroupUserID = @GroupUserID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@GroupUserID", groupUserID)))
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
		public GroupUser ToModel(MySqlDataReader dr)
		{
			GroupUser groupUser = new GroupUser();

			groupUser.GroupUserID = (long)ToModelValue(dr,"GroupUserID");
			groupUser.GroupID = (long)ToModelValue(dr,"GroupID");
			groupUser.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			groupUser.IsPass = (sbyte)ToModelValue(dr,"IsPass");
			groupUser.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			return groupUser;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblGroupUser";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<GroupUser> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by GroupUserID))-1 rownum FROM tblGroupUser) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<GroupUser> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblGroupUser where "+columnName+"="+columnContent;
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
		public IEnumerable<GroupUser> GetAll()
		{
			string sql = "SELECT * FROM tblGroupUser";
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
		protected IEnumerable<GroupUser> ToModels(MySqlDataReader reader)
		{
			var list = new List<GroupUser>();
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