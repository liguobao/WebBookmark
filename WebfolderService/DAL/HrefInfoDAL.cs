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
	public partial class HrefInfoDAL
	{
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (HrefInfo hrefInfo)
		{
				string sql ="INSERT INTO tblHrefInfo (UserWebFolderID, UserInfoID, Href, HTML, Host, CreateTime, ImportXML)  VALUES (@UserWebFolderID, @UserInfoID, @Href, @HTML, @Host, @CreateTime, @ImportXML)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@UserWebFolderID", ToDBValue(hrefInfo.UserWebFolderID)),
						new MySqlParameter("@UserInfoID", ToDBValue(hrefInfo.UserInfoID)),
						new MySqlParameter("@Href", ToDBValue(hrefInfo.Href)),
						new MySqlParameter("@HTML", ToDBValue(hrefInfo.HTML)),
						new MySqlParameter("@Host", ToDBValue(hrefInfo.Host)),
						new MySqlParameter("@CreateTime", ToDBValue(hrefInfo.CreateTime)),
						new MySqlParameter("@ImportXML", ToDBValue(hrefInfo.ImportXML)),
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
        public int DeleteByHrefInfoID(long hrefInfoID)
		{
            string sql = "DELETE from tblHrefInfo WHERE HrefInfoID = @HrefInfoID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@HrefInfoID", hrefInfoID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(HrefInfo hrefInfo)
        {
            string sql =
                "UPDATE tblHrefInfo " +
                "SET " +
			" UserWebFolderID = @UserWebFolderID" 
                +", UserInfoID = @UserInfoID" 
                +", Href = @Href" 
                +", HTML = @HTML" 
                +", Host = @Host" 
                +", CreateTime = @CreateTime" 
                +", ImportXML = @ImportXML" 
               
            +" WHERE HrefInfoID = @HrefInfoID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@HrefInfoID", hrefInfo.HrefInfoID)
					,new MySqlParameter("@UserWebFolderID", ToDBValue(hrefInfo.UserWebFolderID))
					,new MySqlParameter("@UserInfoID", ToDBValue(hrefInfo.UserInfoID))
					,new MySqlParameter("@Href", ToDBValue(hrefInfo.Href))
					,new MySqlParameter("@HTML", ToDBValue(hrefInfo.HTML))
					,new MySqlParameter("@Host", ToDBValue(hrefInfo.Host))
					,new MySqlParameter("@CreateTime", ToDBValue(hrefInfo.CreateTime))
					,new MySqlParameter("@ImportXML", ToDBValue(hrefInfo.ImportXML))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public HrefInfo GetByHrefInfoID(long hrefInfoID)
        {
            string sql = "SELECT * FROM tblHrefInfo WHERE HrefInfoID = @HrefInfoID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@HrefInfoID", hrefInfoID)))
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
		public HrefInfo ToModel(MySqlDataReader dr)
		{
			HrefInfo hrefInfo = new HrefInfo();

			hrefInfo.HrefInfoID = (long)ToModelValue(dr,"HrefInfoID");
			hrefInfo.UserWebFolderID = (long)ToModelValue(dr,"UserWebFolderID");
			hrefInfo.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			hrefInfo.Href = (string)ToModelValue(dr,"Href");
			hrefInfo.HTML = (string)ToModelValue(dr,"HTML");
			hrefInfo.Host = (string)ToModelValue(dr,"Host");
			hrefInfo.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			hrefInfo.ImportXML = (string)ToModelValue(dr,"ImportXML");
			return hrefInfo;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblHrefInfo";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<HrefInfo> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by HrefInfoID))-1 rownum FROM tblHrefInfo) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<HrefInfo> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblHrefInfo where "+columnName+"="+columnContent;
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
		public IEnumerable<HrefInfo> GetAll()
		{
			string sql = "SELECT * FROM tblHrefInfo";
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
		protected IEnumerable<HrefInfo> ToModels(MySqlDataReader reader)
		{
			var list = new List<HrefInfo>();
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