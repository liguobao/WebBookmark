using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkBo.Model
{
    public class BizUserWebFolder
    {
        #region 属性


        /// <summary>
        /// 主键，自增
        /// </summary>
        public long UserWebFolderID { get; set; }

        /// <summary>
        /// 收藏夹名称
        /// </summary>
        public string WebFolderName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserInfoID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 对外是否可见
        /// </summary>
        public sbyte Visible { get; set; }

        /// <summary>
        /// 父收藏夹ID
        /// </summary>
        public long ParentWebfolderID { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Comment { get; set; }
        #endregion


        public UserWebFolder ToModel()
        {

            return new UserWebFolder()
            {
                UserInfoID= UserInfoID,
                UserWebFolderID=UserWebFolderID,
                Comment = Comment,
                CreateTime=CreateTime,
                ParentWebfolderID = ParentWebfolderID,
                Visible = Visible,
                WebFolderName = WebFolderName,
            };

        } 

        public BizUserWebFolder (UserWebFolder dataInfo)
        {
            UserInfoID = dataInfo.UserInfoID;
            UserWebFolderID = dataInfo.UserWebFolderID;
            Comment = dataInfo.Comment;
            CreateTime = dataInfo.CreateTime;
            ParentWebfolderID = dataInfo.ParentWebfolderID;
            Visible = dataInfo.Visible;
            WebFolderName = dataInfo.WebFolderName;
        }

        public BizUserWebFolder()
        {

        }

        public List<BizUserWebFolder> LoadAllByUID(long uid)
        {
            List<BizUserWebFolder> lst = new List<BizUserWebFolder>();

            return lst;
        }
    }
}
