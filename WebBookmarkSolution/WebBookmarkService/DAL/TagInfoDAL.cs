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
	public partial class TagInfoDAL
	{
        #region 自动生成方法

        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add(TagInfo tagInfo)
        {
            string sql = "INSERT INTO tblTagInfo (TagName, UserInfoID, CreateTime)  VALUES (@TagName, @UserInfoID, @CreateTime)";
            MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@TagName", ToDBValue(tagInfo.TagName)),
						new MySqlParameter("@UserInfoID", ToDBValue(tagInfo.UserInfoID)),
						new MySqlParameter("@CreateTime", ToDBValue(tagInfo.CreateTime)),
					};

            int AddId = (int)MyDBHelper.ExecuteScalar(sql, para);
            if (AddId == 1)
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
        public int DeleteByTagInfoID(long tagInfoID)
        {
            string sql = "DELETE from tblTagInfo WHERE TagInfoID = @TagInfoID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@TagInfoID", tagInfoID)
			};

            return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion




        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(TagInfo tagInfo)
        {
            string sql =
                "UPDATE tblTagInfo " +
                "SET " +
            " TagName = @TagName"
                + ", UserInfoID = @UserInfoID"
                + ", CreateTime = @CreateTime"

            + " WHERE TagInfoID = @TagInfoID";


            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@TagInfoID", tagInfo.TagInfoID)
					,new MySqlParameter("@TagName", ToDBValue(tagInfo.TagName))
					,new MySqlParameter("@UserInfoID", ToDBValue(tagInfo.UserInfoID))
					,new MySqlParameter("@CreateTime", ToDBValue(tagInfo.CreateTime))
			};

            return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion

        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public TagInfo GetByTagInfoID(long tagInfoID)
        {
            string sql = "SELECT * FROM tblTagInfo WHERE TagInfoID = @TagInfoID";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@TagInfoID", tagInfoID)))
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
        public TagInfo ToModel(MySqlDataReader dr)
        {
            TagInfo tagInfo = new TagInfo();

            tagInfo.TagInfoID = (long)ToModelValue(dr, "TagInfoID");
            tagInfo.TagName = (string)ToModelValue(dr, "TagName");
            tagInfo.UserInfoID = (long)ToModelValue(dr, "UserInfoID");
            tagInfo.CreateTime = (DateTime)ToModelValue(dr, "CreateTime");
            return tagInfo;
        }
        #endregion

        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
        public int GetTotalCount()
        {
            string sql = "SELECT count(*) FROM tblTagInfo";
            return (int)MyDBHelper.ExecuteScalar(sql);
        }
        #endregion

        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
        public IEnumerable<TagInfo> GetPagedData(int minrownum, int maxrownum)
        {
            string sql = "SELECT * from(SELECT *,(row_number() over(order by TagInfoID))-1 rownum FROM tblTagInfo) t where rownum>=@minrownum and rownum<=@maxrownum";
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
        public IEnumerable<TagInfo> GetBycolumnName(string columnName, string columnContent)
        {
            string sql = "SELECT * FROM tblTagInfo where " + columnName + "=" + columnContent;
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
        public IEnumerable<TagInfo> GetAll()
        {
            string sql = "SELECT * FROM tblTagInfo";
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
        protected IEnumerable<TagInfo> ToModels(MySqlDataReader reader)
        {
            var list = new List<TagInfo>();
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
        /// 根据用户ID获取用户的所有标签
        /// </summary>
        /// <param name="userinfoID"></param>
        /// <returns></returns>
        public IEnumerable<TagInfo> GetListByUserID(long userinfoID)
        {
            string sql = "SELECT * FROM tblTagInfo WHERE UserInfoID = @UserInfoID";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@TagInfoID", userinfoID)))
            {
                return ToModels(reader);
            }
        }

        public IEnumerable<TagInfo> GetListByTagName(string tagName)
        {
            string sql = "SELECT * FROM tblTagInfo WHERE TagName = @TagName";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@TagName", tagName)))
            {
                return ToModels(reader);
            }
        }

        public TagInfo GetByTagNameAndUserInfoID(string tagName,long userInfoID)
        {
            string sql = "SELECT * FROM tblTagInfo WHERE TagName = @TagName and UserInfoID =@UserInfoID";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@TagName", tagName), new MySqlParameter("@UserInfoID", userInfoID)))
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