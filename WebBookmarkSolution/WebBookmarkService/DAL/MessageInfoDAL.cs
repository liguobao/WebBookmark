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
	public partial class MessageInfoDAL
	{
		#region 自动生成方法
		
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (MessageInfo messageInfo)
		{
				string sql ="INSERT INTO tblMessageInfo (MessageTitle, MessageContent, UserInfoID, IsRead, MessageInfoType, CreateTime, MessageURL)  VALUES (@MessageTitle, @MessageContent, @UserInfoID, @IsRead, @MessageInfoType, @CreateTime, @MessageURL)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@MessageTitle", ToDBValue(messageInfo.MessageTitle)),
						new MySqlParameter("@MessageContent", ToDBValue(messageInfo.MessageContent)),
						new MySqlParameter("@UserInfoID", ToDBValue(messageInfo.UserInfoID)),
						new MySqlParameter("@IsRead", ToDBValue(messageInfo.IsRead)),
						new MySqlParameter("@MessageInfoType", ToDBValue(messageInfo.MessageInfoType)),
						new MySqlParameter("@CreateTime", ToDBValue(messageInfo.CreateTime)),
						new MySqlParameter("@MessageURL", ToDBValue(messageInfo.MessageURL)),
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
        public int DeleteByMessageInfoID(long messageInfoID)
		{
            string sql = "DELETE from tblMessageInfo WHERE MessageInfoID = @MessageInfoID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@MessageInfoID", messageInfoID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(MessageInfo messageInfo)
        {
            string sql =
                "UPDATE tblMessageInfo " +
                "SET " +
			" MessageTitle = @MessageTitle" 
                +", MessageContent = @MessageContent" 
                +", UserInfoID = @UserInfoID" 
                +", IsRead = @IsRead" 
                +", MessageInfoType = @MessageInfoType" 
                +", CreateTime = @CreateTime" 
                +", MessageURL = @MessageURL" 
               
            +" WHERE MessageInfoID = @MessageInfoID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@MessageInfoID", messageInfo.MessageInfoID)
					,new MySqlParameter("@MessageTitle", ToDBValue(messageInfo.MessageTitle))
					,new MySqlParameter("@MessageContent", ToDBValue(messageInfo.MessageContent))
					,new MySqlParameter("@UserInfoID", ToDBValue(messageInfo.UserInfoID))
					,new MySqlParameter("@IsRead", ToDBValue(messageInfo.IsRead))
					,new MySqlParameter("@MessageInfoType", ToDBValue(messageInfo.MessageInfoType))
					,new MySqlParameter("@CreateTime", ToDBValue(messageInfo.CreateTime))
					,new MySqlParameter("@MessageURL", ToDBValue(messageInfo.MessageURL))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public MessageInfo GetByMessageInfoID(long messageInfoID)
        {
            string sql = "SELECT * FROM tblMessageInfo WHERE MessageInfoID = @MessageInfoID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@MessageInfoID", messageInfoID)))
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
		public MessageInfo ToModel(MySqlDataReader dr)
		{
			MessageInfo messageInfo = new MessageInfo();

			messageInfo.MessageInfoID = (long)ToModelValue(dr,"MessageInfoID");
			messageInfo.MessageTitle = (string)ToModelValue(dr,"MessageTitle");
			messageInfo.MessageContent = (string)ToModelValue(dr,"MessageContent");
			messageInfo.UserInfoID = (long)ToModelValue(dr,"UserInfoID");
			messageInfo.IsRead = (int)ToModelValue(dr,"IsRead");
			messageInfo.MessageInfoType = (int)ToModelValue(dr,"MessageInfoType");
			messageInfo.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			messageInfo.MessageURL = (string)ToModelValue(dr,"MessageURL");
			return messageInfo;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblMessageInfo";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<MessageInfo> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by MessageInfoID))-1 rownum FROM tblMessageInfo) t where rownum>=@minrownum and rownum<=@maxrownum";
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
		public IEnumerable<MessageInfo> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblMessageInfo where "+columnName+"="+columnContent;
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
		public IEnumerable<MessageInfo> GetAll()
		{
			string sql = "SELECT * FROM tblMessageInfo";
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
		protected IEnumerable<MessageInfo> ToModels(MySqlDataReader reader)
		{
			var list = new List<MessageInfo>();
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


        /// <summary>
        /// 根据UserInfoID获取信息
        /// </summary>
        /// <param name="userInfoID"></param>
        /// <returns></returns>
        public IEnumerable<MessageInfo> GetListByUserInfoID(long userInfoID)
        {
            string sql = "SELECT * FROM tblMessageInfo WHERE UserInfoID = @UserInfoID order by CreateTime desc";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserInfoID", userInfoID)))
            {
                return ToModels(reader);
            }
        }


        /// <summary>
        /// 根据未读消息
        /// </summary>
        /// <param name="userInfoID"></param>
        /// <returns></returns>
        public IEnumerable<MessageInfo> GetNotReadListByUserInfoID(long userInfoID)
        {
            string sql = "SELECT * FROM tblMessageInfo WHERE UserInfoID = @UserInfoID and IsRead =0 order by CreateTime desc";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserInfoID", userInfoID)))
            {
                return ToModels(reader);
            }
        }

     

        public IEnumerable<MessageInfo> GetHasReadListByUserInfoID(long userInfoID)
        {
            string sql = "SELECT * FROM tblMessageInfo WHERE UserInfoID = @UserInfoID and IsRead =1 order by CreateTime desc";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserInfoID", userInfoID)))
            {
                return ToModels(reader);
            }
        }

	}
}