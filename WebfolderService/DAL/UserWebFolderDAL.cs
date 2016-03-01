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
	public partial class UserWebFolderDAL
	{
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (UserWebFolder userWebFolder)
		{
				string sql ="INSERT INTO tblUserWebFolder (WebFolderName, UserInfoID, CreateTime, Visible)  VALUES (@WebFolderName, @UserInfoID, @CreateTime, @Visible)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@WebFolderName", ToDBValue(userWebFolder.WebFolderName)),
						new MySqlParameter("@UserInfoID", ToDBValue(userWebFolder.UserInfoID)),
						new MySqlParameter("@CreateTime", ToDBValue(userWebFolder.CreateTime)),
						new MySqlParameter("@Visible", ToDBValue(userWebFolder.Visible)),
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
        public int DeleteByUserWebFolderID(long userWebFolderID)
		{
            string sql = "DELETE from tblUserWebFolder WHERE UserWebFolderID = @UserWebFolderID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserWebFolderID", userWebFolderID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(UserWebFolder userWebFolder)
        {
            string sql =
                "UPDATE tblUserWebFolder " +
                "SET " +
			" WebFolderName = @WebFolderName" 
                +", UserInfoID = @UserInfoID" 
                +", CreateTime = @CreateTime" 
                +", Visible = @Visible" 
               
            +" WHERE UserWebFolderID = @UserWebFolderID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserWebFolderID", userWebFolder.UserWebFolderID)
					,new MySqlParameter("@WebFolderName", ToDBValue(userWebFolder.WebFolderName))
					,new MySqlParameter("@UserInfoID", ToDBValue(userWebFolder.UserInfoID))
					,new MySqlParameter("@CreateTime", ToDBValue(userWebFolder.CreateTime))
					,new MySqlParameter("@Visible", ToDBValue(userWebFolder.Visible))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public UserWebFolder GetByUserWebFolderID(long userWebFolderID)
        {
            string sql = "SELECT * FROM tblUserWebFolder WHERE UserWebFolderID = @UserWebFolderID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserWebFolderID", userWebFolderID)))
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
		public UserWebFolder ToModel(MySqlDataReader dr)
		{
			UserWebFolder userWebFolder = new UserWebFolder();

			userWebFolder.UserWebFolderID = (long)ToModelValue(dr,"UserWebFolderID");
			userWebFolder.WebFolderName = (string)ToModelValue(dr,"WebFolderName");
			userWebFolder.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			userWebFolder.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			userWebFolder.Visible = (ushort)ToModelValue(dr,"Visible");
			return userWebFolder;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblUserWebFolder";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<UserWebFolder> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by UserWebFolderID))-1 rownum FROM tblUserWebFolder) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<UserWebFolder> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblUserWebFolder where "+columnName+"="+columnContent;
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
		public IEnumerable<UserWebFolder> GetAll()
		{
			string sql = "SELECT * FROM tblUserWebFolder";
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
		protected IEnumerable<UserWebFolder> ToModels(MySqlDataReader reader)
		{
			var list = new List<UserWebFolder>();
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