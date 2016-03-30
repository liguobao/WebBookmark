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
	public partial class GroupInfoDAL
	{

        #region 自动生成方法

        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add(GroupInfo groupInfo)
        {
            string sql = "INSERT INTO tblGroupInfo (GroupName, GroupIntro, CreateUesrID, CreateTime, ObjectHashcode)  VALUES (@GroupName, @GroupIntro, @CreateUesrID, @CreateTime, @ObjectHashcode)";
            MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@GroupName", ToDBValue(groupInfo.GroupName)),
						new MySqlParameter("@GroupIntro", ToDBValue(groupInfo.GroupIntro)),
						new MySqlParameter("@CreateUesrID", ToDBValue(groupInfo.CreateUesrID)),
						new MySqlParameter("@CreateTime", ToDBValue(groupInfo.CreateTime)),
						new MySqlParameter("@ObjectHashcode", ToDBValue(groupInfo.ObjectHashcode)),
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
        public int DeleteByGroupInfoID(long groupInfoID)
        {
            string sql = "DELETE from tblGroupInfo WHERE GroupInfoID = @GroupInfoID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@GroupInfoID", groupInfoID)
			};

            return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion




        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(GroupInfo groupInfo)
        {
            string sql =
                "UPDATE tblGroupInfo " +
                "SET " +
            " GroupName = @GroupName"
                + ", GroupIntro = @GroupIntro"
                + ", CreateUesrID = @CreateUesrID"
                + ", CreateTime = @CreateTime"
                + ", ObjectHashcode = @ObjectHashcode"

            + " WHERE GroupInfoID = @GroupInfoID";


            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@GroupInfoID", groupInfo.GroupInfoID)
					,new MySqlParameter("@GroupName", ToDBValue(groupInfo.GroupName))
					,new MySqlParameter("@GroupIntro", ToDBValue(groupInfo.GroupIntro))
					,new MySqlParameter("@CreateUesrID", ToDBValue(groupInfo.CreateUesrID))
					,new MySqlParameter("@CreateTime", ToDBValue(groupInfo.CreateTime))
					,new MySqlParameter("@ObjectHashcode", ToDBValue(groupInfo.ObjectHashcode))
			};

            return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion

        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public GroupInfo GetByGroupInfoID(long groupInfoID)
        {
            string sql = "SELECT * FROM tblGroupInfo WHERE GroupInfoID = @GroupInfoID";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@GroupInfoID", groupInfoID)))
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
        public GroupInfo ToModel(MySqlDataReader dr)
        {
            GroupInfo groupInfo = new GroupInfo();

            groupInfo.GroupInfoID = (long)ToModelValue(dr, "GroupInfoID");
            groupInfo.GroupName = (string)ToModelValue(dr, "GroupName");
            groupInfo.GroupIntro = (string)ToModelValue(dr, "GroupIntro");
            groupInfo.CreateUesrID = (long)ToModelValue(dr, "CreateUesrID");
            groupInfo.CreateTime = (DateTime)ToModelValue(dr, "CreateTime");
            groupInfo.ObjectHashcode = (int)ToModelValue(dr, "ObjectHashcode");
            return groupInfo;
        }
        #endregion

        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
        public int GetTotalCount()
        {
            string sql = "SELECT count(*) FROM tblGroupInfo";
            return (int)MyDBHelper.ExecuteScalar(sql);
        }
        #endregion

        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
        public IEnumerable<GroupInfo> GetPagedData(int minrownum, int maxrownum)
        {
            string sql = "SELECT * from(SELECT *,(row_number() over(order by GroupInfoID))-1 rownum FROM tblGroupInfo) t where rownum>=@minrownum and rownum<=@maxrownum";
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
        public IEnumerable<GroupInfo> GetBycolumnName(string columnName, string columnContent)
        {
            string sql = "SELECT * FROM tblGroupInfo where " + columnName + "=" + columnContent;
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
        public IEnumerable<GroupInfo> GetAll()
        {
            string sql = "SELECT * FROM tblGroupInfo";
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
        protected IEnumerable<GroupInfo> ToModels(MySqlDataReader reader)
        {
            var list = new List<GroupInfo>();
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
        /// 根据创建者获取用户组
        /// </summary>
        /// <param name="createUesrID"></param>
        /// <returns></returns>
        public IEnumerable<GroupInfo> GetByCreateUesrID(long createUesrID)
        {
            string sql = "SELECT * FROM tblGroupInfo WHERE CreateUesrID = @CreateUesrID";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@CreateUesrID", createUesrID)))
            {
                return ToModels(reader);
            }
        }


        
        /// <summary>
        /// 获取批量的群组信息
        /// </summary>
        /// <param name="createUesrID"></param>
        /// <returns></returns>
        public IEnumerable<GroupInfo> GetByGroupIDList(List<long> lstGroupID)
        {
            string sql =string.Format( "SELECT * FROM tblGroupInfo WHERE GroupInfoID IN ({0})",string.Join(",",lstGroupID));
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
            {
                return ToModels(reader);
            }
        }

        /// <summary>
        /// 用创建对象的hashcode获取对象
        /// </summary>
        /// <param name="ojHashcode"></param>
        /// <returns></returns>
        public GroupInfo GetByObjectHashCode(long ojHashcode)
        {
            string sql = "SELECT * FROM tblGroupInfo WHERE ObjectHashcode = @ObjectHashcode";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@ObjectHashcode", ojHashcode)))
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