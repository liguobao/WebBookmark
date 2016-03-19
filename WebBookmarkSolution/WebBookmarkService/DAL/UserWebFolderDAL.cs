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
	public partial class UserWebFolderDAL
	{
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (UserWebFolder userWebFolder)
		{
				string sql = "INSERT INTO tblUserWebFolder (UserWebFolderID, WebFolderName, UserInfoID, CreateTime, Visible, ParentWebfolderID, IntroContent,IElementJSON,IElementHashcode)  VALUES (@UserWebFolderID, @WebFolderName, @UserInfoID, @CreateTime, @Visible, @ParentWebfolderID, @IntroContent,@IElementJSON,@IElementHashcode)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@UserWebFolderID", ToDBValue(userWebFolder.UserWebFolderID)),
						new MySqlParameter("@WebFolderName", ToDBValue(userWebFolder.WebFolderName)),
						new MySqlParameter("@UserInfoID", ToDBValue(userWebFolder.UserInfoID)),
						new MySqlParameter("@CreateTime", ToDBValue(userWebFolder.CreateTime)),
						new MySqlParameter("@Visible", ToDBValue(userWebFolder.Visible)),
						new MySqlParameter("@ParentWebfolderID", ToDBValue(userWebFolder.ParentWebfolderID)),
						new MySqlParameter("@IntroContent", ToDBValue(userWebFolder.IntroContent)),
                        new MySqlParameter("@IElementJSON", ToDBValue(userWebFolder.IElementJSON)),
                         new MySqlParameter("@IElementHashcode", ToDBValue(userWebFolder.IElementHashcode)),
                        

                    };
				int AddId = (int)MyDBHelper.ExecuteNonQuery(sql, para);
				if(AddId==1)
				{
					return true;
				}else
				{
					return false;					
				}			
		}
        #endregion




        #region 批量插入Model
        /// <summary>
        /// 批量插入Model
        /// </summary>        
        public bool BatchAdd(List<UserWebFolder> lstUserWebFolder)
        {
          
            StringBuilder sbSQL = new StringBuilder();
            int index = 0;

            List<MySqlParameter> lstPara = new List<MySqlParameter>();
            foreach (var userWebFolder in lstUserWebFolder)
            {
                sbSQL.AppendLine(string.Format(@"INSERT INTO tblUserWebFolder(UserWebFolderID, WebFolderName, UserInfoID, CreateTime, Visible, ParentWebfolderID, IntroContent,IElementJSON,IElementHashcode)  VALUES(
                   @UserWebFolderID{0}, @WebFolderName{1}, @UserInfoID{2}, @CreateTime{3}, @Visible{4}, @ParentWebfolderID{5}, @IntroContent{6},@IElementJSON{7},@IElementHashcode{8});",
                    index,index,index,index,index,index,index,index,index));

                MySqlParameter[] paramater = new MySqlParameter[]
               {
                        new MySqlParameter(string.Format("@UserWebFolderID{0}",index), ToDBValue(userWebFolder.UserWebFolderID)),
                        new MySqlParameter(string.Format("@WebFolderName{0}",index), ToDBValue(userWebFolder.WebFolderName)),
                        new MySqlParameter(string.Format("@UserInfoID{0}",index), ToDBValue(userWebFolder.UserInfoID)),
                        new MySqlParameter(string.Format("@CreateTime{0}",index), ToDBValue(userWebFolder.CreateTime)),
                        new MySqlParameter(string.Format("@Visible{0}",index), ToDBValue(userWebFolder.Visible)),
                        new MySqlParameter(string.Format("@ParentWebfolderID{0}",index), ToDBValue(userWebFolder.ParentWebfolderID)),
                        new MySqlParameter(string.Format("@IntroContent{0}",index), ToDBValue(userWebFolder.IntroContent)),
                        new MySqlParameter(string.Format("@IElementJSON{0}",index), ToDBValue(userWebFolder.IElementJSON)),
                         new MySqlParameter(string.Format("@IElementHashcode{0}",index), ToDBValue(userWebFolder.IElementHashcode)),
                        
               };
               lstPara.AddRange(paramater);
                index = index + 1;
            }

            int AddId = (int)MyDBHelper.ExecuteNonQuery(sbSQL.ToString(), lstPara.ToArray());
            return (AddId > 0);
            
        }
        #endregion


        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="lstUserWebFolder"></param>
        /// <returns></returns>
        public bool BatchUpdata(List<UserWebFolder> lstUserWebFolder)
        {
            try
            {
                StringBuilder sbSQL = new StringBuilder();
                int index = 0;

                List<MySqlParameter> lstPara = new List<MySqlParameter>();
                foreach (var userWebFolder in lstUserWebFolder)
                {
                    sbSQL.AppendLine(
                   "UPDATE tblUserWebFolder " +
                   "SET "
                   + " WebFolderName = @WebFolderName" + index
                   + ", UserInfoID = @UserInfoID" + index
                   + ", CreateTime = @CreateTime" + index
                   + ", Visible = @Visible" + index
                   + ", ParentWebfolderID = @ParentWebfolderID" + index
                   + ", IntroContent = @IntroContent" + index
                   + ", IElementJSON = @IElementJSON" + index
                   + ", IElementHashcode = @IElementHashcode" + index
                   + " WHERE UserWebFolderID = @UserWebFolderID" + index + ";");

                    MySqlParameter[] paramater = new MySqlParameter[]
                   {
                        new MySqlParameter(string.Format("@UserWebFolderID{0}",index), ToDBValue(userWebFolder.UserWebFolderID)),
                        new MySqlParameter(string.Format("@WebFolderName{0}",index), ToDBValue(userWebFolder.WebFolderName)),
                        new MySqlParameter(string.Format("@UserInfoID{0}",index), ToDBValue(userWebFolder.UserInfoID)),
                        new MySqlParameter(string.Format("@CreateTime{0}",index), ToDBValue(userWebFolder.CreateTime)),
                        new MySqlParameter(string.Format("@Visible{0}",index), ToDBValue(userWebFolder.Visible)),
                        new MySqlParameter(string.Format("@ParentWebfolderID{0}",index), ToDBValue(userWebFolder.ParentWebfolderID)),
                        new MySqlParameter(string.Format("@IntroContent{0}",index), ToDBValue(userWebFolder.IntroContent)),
                        new MySqlParameter(string.Format("@IElementJSON{0}",index), ToDBValue(userWebFolder.IElementJSON)),
                         new MySqlParameter(string.Format("@IElementHashcode{0}",index), ToDBValue(userWebFolder.IElementHashcode)),

                   };
                    lstPara.AddRange(paramater);
                    index = index + 1;
                }

                return MyDBHelper.ExecuteNonQuery(sbSQL.ToString(), lstPara.ToArray()) > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        ///<summary>
        ///根据字段名获取数据记录IEnumerable<>
        ///</summary>              
        public IEnumerable<UserWebFolder> GetByUID(long uid)
        {
            string sql = "SELECT * FROM tblUserWebFolder where UserInfoID=" + uid;
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
            {
                return ToModels(reader);
            }
        }


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
                + ", UserInfoID = @UserInfoID"
                + ", CreateTime = @CreateTime"
                + ", Visible = @Visible"
                + ", ParentWebfolderID = @ParentWebfolderID"
                + ", IntroContent = @IntroContent"
                + ", IElementJSON = @IElementJSON"
                 + ", IElementHashcode = @IElementHashcode"

            + " WHERE UserWebFolderID = @UserWebFolderID";


            MySqlParameter[] para = new MySqlParameter[]
            {
                new MySqlParameter("@UserWebFolderID", userWebFolder.UserWebFolderID)
                    ,new MySqlParameter("@WebFolderName", ToDBValue(userWebFolder.WebFolderName))
                    ,new MySqlParameter("@UserInfoID", ToDBValue(userWebFolder.UserInfoID))
                    ,new MySqlParameter("@CreateTime", ToDBValue(userWebFolder.CreateTime))
                    ,new MySqlParameter("@Visible", ToDBValue(userWebFolder.Visible))
                    ,new MySqlParameter("@ParentWebfolderID", ToDBValue(userWebFolder.ParentWebfolderID))
                    ,new MySqlParameter("@IntroContent", ToDBValue(userWebFolder.IntroContent))
                    ,new MySqlParameter("@IElementJSON", ToDBValue(userWebFolder.IElementJSON))
                     ,new MySqlParameter("@IElementHashcode", ToDBValue(userWebFolder.IElementHashcode))
                    
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
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserWebFolderID", userWebFolderID)))
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

            userWebFolder.UserWebFolderID = (long)ToModelValue(dr, "UserWebFolderID");
            userWebFolder.WebFolderName = (string)ToModelValue(dr, "WebFolderName");
            userWebFolder.UserInfoID = (long)ToModelValue(dr, "UserInfoID");
            userWebFolder.CreateTime = (DateTime)ToModelValue(dr, "CreateTime");
            userWebFolder.Visible = (sbyte)ToModelValue(dr, "Visible");
            userWebFolder.ParentWebfolderID = (long?)ToModelValue(dr, "ParentWebfolderID");
            userWebFolder.IntroContent = (string)ToModelValue(dr, "IntroContent");
            userWebFolder.IElementJSON = (string)ToModelValue(dr, "IElementJSON");
            userWebFolder.IElementHashcode = (int)ToModelValue(dr, "IElementHashcode");
            
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
        public IEnumerable<UserWebFolder> GetPagedData(int minrownum, int maxrownum)
        {
            string sql = "SELECT * from(SELECT *,(row_number() over(order by UserWebFolderID))-1 rownum FROM tblUserWebFolder) t where rownum>=@minrownum and rownum<=@maxrownum";
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
        public IEnumerable<UserWebFolder> GetBycolumnName(string columnName, string columnContent)
        {
            string sql = "SELECT * FROM tblUserWebFolder where " + columnName + "=" + columnContent;
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
        public IEnumerable<UserWebFolder> GetAll()
        {
            string sql = "SELECT * FROM tblUserWebFolder";
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
        protected IEnumerable<UserWebFolder> ToModels(MySqlDataReader reader)
        {
            var list = new List<UserWebFolder>();
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

    }
}