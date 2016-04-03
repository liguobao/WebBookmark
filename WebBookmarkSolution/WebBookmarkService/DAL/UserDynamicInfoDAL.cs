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
	public partial class UserDynamicInfoDAL
	{
        #region 自动生成方法

        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add(UserDynamicInfo userDynamicInfo)
        {
            string sql = "INSERT INTO tblUserDynamicInfo (UserDynamicInfoID, UserDynamicContent, CreateTime, UserInfoID, UserDynamicType, UserDynamicURL)  VALUES (@UserDynamicInfoID, @UserDynamicContent, @CreateTime, @UserInfoID, @UserDynamicType, @UserDynamicURL)";
            MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@UserDynamicInfoID", ToDBValue(userDynamicInfo.UserDynamicInfoID)),
						new MySqlParameter("@UserDynamicContent", ToDBValue(userDynamicInfo.UserDynamicContent)),
						new MySqlParameter("@CreateTime", ToDBValue(userDynamicInfo.CreateTime)),
						new MySqlParameter("@UserInfoID", ToDBValue(userDynamicInfo.UserInfoID)),
						new MySqlParameter("@UserDynamicType", ToDBValue(userDynamicInfo.UserDynamicType)),
						new MySqlParameter("@UserDynamicURL", ToDBValue(userDynamicInfo.UserDynamicURL)),
					};
            int AddId = (int)MyDBHelper.ExecuteNonQuery(sql, para);
            if (AddId > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
        public int DeleteByUserDynamicInfoID(long userDynamicInfoID)
        {
            string sql = "DELETE from tblUserDynamicInfo WHERE UserDynamicInfoID = @UserDynamicInfoID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserDynamicInfoID", userDynamicInfoID)
			};

            return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion




        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(UserDynamicInfo userDynamicInfo)
        {
            string sql =
                "UPDATE tblUserDynamicInfo " +
                "SET " +
            " UserDynamicContent = @UserDynamicContent"
                + ", CreateTime = @CreateTime"
                + ", UserInfoID = @UserInfoID"
                + ", UserDynamicType = @UserDynamicType"
                + ", UserDynamicURL = @UserDynamicURL"

            + " WHERE UserDynamicInfoID = @UserDynamicInfoID";


            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserDynamicInfoID", userDynamicInfo.UserDynamicInfoID)
					,new MySqlParameter("@UserDynamicContent", ToDBValue(userDynamicInfo.UserDynamicContent))
					,new MySqlParameter("@CreateTime", ToDBValue(userDynamicInfo.CreateTime))
					,new MySqlParameter("@UserInfoID", ToDBValue(userDynamicInfo.UserInfoID))
					,new MySqlParameter("@UserDynamicType", ToDBValue(userDynamicInfo.UserDynamicType))
					,new MySqlParameter("@UserDynamicURL", ToDBValue(userDynamicInfo.UserDynamicURL))
			};

            return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion

        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public UserDynamicInfo GetByUserDynamicInfoID(long userDynamicInfoID)
        {
            string sql = "SELECT * FROM tblUserDynamicInfo WHERE UserDynamicInfoID = @UserDynamicInfoID";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserDynamicInfoID", userDynamicInfoID)))
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
        public UserDynamicInfo ToModel(MySqlDataReader dr)
        {
            UserDynamicInfo userDynamicInfo = new UserDynamicInfo();

            userDynamicInfo.UserDynamicInfoID = (long)ToModelValue(dr, "UserDynamicInfoID");
            userDynamicInfo.UserDynamicContent = (string)ToModelValue(dr, "UserDynamicContent");
            userDynamicInfo.CreateTime = (DateTime)ToModelValue(dr, "CreateTime");
            userDynamicInfo.UserInfoID = (long)ToModelValue(dr, "UserInfoID");
            userDynamicInfo.UserDynamicType = (int)ToModelValue(dr, "UserDynamicType");
            userDynamicInfo.UserDynamicURL = (string)ToModelValue(dr, "UserDynamicURL");
            return userDynamicInfo;
        }
        #endregion

        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
        public int GetTotalCount()
        {
            string sql = "SELECT count(*) FROM tblUserDynamicInfo";
            return (int)MyDBHelper.ExecuteScalar(sql);
        }
        #endregion

        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
        public IEnumerable<UserDynamicInfo> GetPagedData(int minrownum, int maxrownum)
        {
            string sql = "SELECT * from(SELECT *,(row_number() over(order by UserDynamicInfoID))-1 rownum FROM tblUserDynamicInfo) t where rownum>=@minrownum and rownum<=@maxrownum";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql,
                new MySqlParameter("@minrownum", minrownum),
                new MySqlParameter("@maxrownum", maxrownum)))
            {
                return ToModels(reader);
            }
        }
        #endregion


        #region 根据字段名获取数据记录IEnumerable<>
        ///<summary>
        ///根据字段名获取数据记录IEnumerable<>
        ///</summary>              
        public IEnumerable<UserDynamicInfo> GetBycolumnName(string columnName, string columnContent)
        {
            string sql = "SELECT * FROM tblUserDynamicInfo where " + columnName + "=" + columnContent;
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
            {
                return ToModels(reader);
            }
        }
        #endregion



        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary> 
        public IEnumerable<UserDynamicInfo> GetAll()
        {
            string sql = "SELECT * FROM tblUserDynamicInfo";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
            {
                return ToModels(reader);
            }
        }
        #endregion

        #region 把MySqlDataReader转换成IEnumerable<>
        ///<summary>
        /// 把MySqlDataReader转换成IEnumerable<>
        ///</summary> 
        protected IEnumerable<UserDynamicInfo> ToModels(MySqlDataReader reader)
        {
            var list = new List<UserDynamicInfo>();
            while (reader.Read())
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
            if (value == null)
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
        protected object ToModelValue(MySqlDataReader reader, string columnName)
        {
            if (reader.IsDBNull(reader.GetOrdinal(columnName)))
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
        /// 获取用户动态信息
        /// </summary>
        /// <param name="userIDs">用户ID列表</param>
        /// <returns></returns>
        public IEnumerable< UserDynamicInfo> GetListByUserIDs(List<long> userIDs)
        {
            if (userIDs == null && userIDs.Count == 0)
                return new List<UserDynamicInfo>();
            string sql = string.Format("SELECT * FROM tblUserDynamicInfo WHERE UserInfoID in ({0})", string.Join(",", userIDs));
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
            {
                return ToModels(reader);
            }
        }


	}
}