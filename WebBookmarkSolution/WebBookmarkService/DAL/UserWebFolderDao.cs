using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using WebBookmarkService.Model;
using WebBookmarkService;

namespace WebfolderService.DAL
{
    public partial class UserWebFolderDAL
    {

        #region 批量插入Model
        /// <summary>
        /// 批量插入Model
        /// </summary>        
        public bool BatchInsert(List<UserWebFolder> lstUserWebFolder)
        {

            StringBuilder sbSQL = new StringBuilder();
            int index = 0;

            List<MySqlParameter> lstPara = new List<MySqlParameter>();
            foreach (var userWebFolder in lstUserWebFolder)
            {
                sbSQL.AppendLine(string.Format(@"INSERT INTO tblUserWebFolder(UserWebFolderID, WebFolderName, UserInfoID, CreateTime, Visible, ParentWebfolderID, IntroContent,IElementJSON,IElementHashcode)  VALUES(
                   @UserWebFolderID{0}, @WebFolderName{1}, @UserInfoID{2}, @CreateTime{3}, @Visible{4}, @ParentWebfolderID{5}, @IntroContent{6},@IElementJSON{7},@IElementHashcode{8});",
                    index, index, index, index, index, index, index, index, index));

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

    }
}
