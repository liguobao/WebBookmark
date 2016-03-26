using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkBo.Model
{
    public class BizBookmarkInfo
    {
        #region 属性

        /// <summary>
        /// 主键，自增
        /// </summary>
        public long BookmarkInfoID { get; set; }

        /// <summary>
        /// 网页收藏夹ID
        /// </summary>
        public long UserWebFolderID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserInfoID { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 网页HTML
        /// </summary>
        public string HTML { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 导入XML信息
        /// </summary>
        public string IElementJSON { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BookmarkName { get; set; }


        #endregion

        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public BookmarkInfo ToModel()
        {
            return new BookmarkInfo()
            {
                BookmarkInfoID = BookmarkInfoID,
                UserWebFolderID = UserWebFolderID,
                UserInfoID = UserInfoID,
                Href = Href,
                HTML = HTML,
                Host = Host,
                CreateTime = CreateTime,
                IElementJSON = IElementJSON,
                BookmarkName = BookmarkName,
            };
        }




        public BizBookmarkInfo(BookmarkInfo dataInfo)
        {
            BookmarkInfoID = dataInfo.BookmarkInfoID;
            UserWebFolderID = dataInfo.UserWebFolderID;
            UserInfoID = dataInfo.UserInfoID;
            Href = dataInfo.Href;
            HTML = dataInfo.HTML;
            Host = dataInfo.Host;
            CreateTime = dataInfo.CreateTime;
            IElementJSON = dataInfo.IElementJSON;
            BookmarkName = dataInfo.BookmarkName;
        }



        public BizBookmarkInfo()
        {

        }

        public static BizBookmarkInfo LoadByID(long infoID)
        {
            var dataInfo = new BookmarkInfoDAL().GetByBookmarkInfoID(infoID);
            if(dataInfo!=null)
                return new BizBookmarkInfo(dataInfo);
            return new BizBookmarkInfo();
        }


        /// <summary>
        /// 通过FolderID 加载书签数据
        /// </summary>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public static List<BizBookmarkInfo> LoadByFolderID(long folderID)
        {
            var lstModel = new BookmarkInfoDAL().GetListByFolderID(folderID);
            if (lstModel == null)
                return new List<BizBookmarkInfo>();
            return lstModel.Select(model=>new BizBookmarkInfo(model)).ToList();
            
        }


        public static List<BizBookmarkInfo> LoadByUID(long uid)
        {
            var lstBiz = new List<BizBookmarkInfo>();
            var lstModel = new BookmarkInfoDAL().GetListByUID(uid);
            lstBiz = lstModel.Select(model => new BizBookmarkInfo(model)).ToList();
            return lstBiz;
        }


        public void Save()
        {
            if(BookmarkInfoID!=0)
            {
                new BookmarkInfoDAL().Update(ToModel());
            }
            else
            {
                new BookmarkInfoDAL().Add(ToModel()); ;
            }
        }
    }
}
