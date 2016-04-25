using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkBo.Service;
using WebBookmarkService;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkBo.Model
{
    public partial class BizBookmarkInfo
    {
        #region 属性
        private static readonly BookmarkInfoDAL DAL = new BookmarkInfoDAL();

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

        public int HashCode { get; set; }


        /// <summary>
        /// 是否可在iframe中展示
        /// </summary>
        public int IsShowWithiframe { get; set; }

        public List<BizBookmarkTagInfo> BizBookmarkTagInfoList { get; set; }


        public List<BizLikeLog> LikeLogList { get; set; }

        /// <summary>
        /// 点赞数量
        /// </summary>
        public int LikeCount { get; set; }


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
                HashCode = HashCode,
                IsShowWithiframe = IsShowWithiframe,
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
            HashCode = dataInfo.HashCode;
            IsShowWithiframe = dataInfo.IsShowWithiframe;
        }



        public BizBookmarkInfo()
        {

        }

       
        public void Save()
        {
            if(BookmarkInfoID!=0)
            {
                DAL.Update(ToModel());
            }
            else
            {
                DAL.Add(ToModel());

                CreateDynamicInfo();
             
            }
        }

        public void LoadBookmarkTagInfo()
        {
            BizBookmarkTagInfoList = BizBookmarkTagInfo.LoadByBookmarkID(BookmarkInfoID);
        }

        public void LoadLikeLog()
        {
            LikeLogList = BizLikeLog.LoadByBookmarkID(BookmarkInfoID);
            LikeCount = LikeLogList.Count;
        }


        public string AddBookmarkTag(string tagname)
        {
            if (string.IsNullOrEmpty(tagname))
                return "标签不能为空呀。";
            var tagInfo = BizTagInfo.LoadByTagNameAndUserID(tagname,UserInfoID);
            if (tagInfo == null)
            {
                tagInfo = new BizTagInfo();
                tagInfo.TagName = tagname;
                tagInfo.UserInfoID = UserInfoID;
                tagInfo.CreateTime = DateTime.Now;
                tagInfo.Save();
            }
           

            var bookmarkTagInfo = BizBookmarkTagInfo.LoadByBookmarkIDAndTagInfoID(BookmarkInfoID,tagInfo.TagInfoID);
            if (bookmarkTagInfo != null)
                return "已经存在一样的标签了，换一个啦。";
            bookmarkTagInfo = new BizBookmarkTagInfo();
            bookmarkTagInfo.CreateTime = DateTime.Now;
            bookmarkTagInfo.BookmarkInfoID = BookmarkInfoID;
            bookmarkTagInfo.UserInfoID = UserInfoID;
            bookmarkTagInfo.TagInfoID = tagInfo.TagInfoID;
            bookmarkTagInfo.Save();

            return string.Empty;
        }

        public void AddBookmarkTag(long tagInfoID)
        {
            var tagInfo = BizTagInfo.LoadByTagInfoID(tagInfoID);
            if(tagInfo!=null)
            {
                //非当前用户标签，复制一份
                if(tagInfo.UserInfoID != UserInfoID)
                {
                    var newTagInfo = new BizTagInfo() 
                    {
                        TagName = tagInfo.TagName,
                        CreateTime = DateTime.Now,
                        UserInfoID = UserInfoID,
                    };
                    newTagInfo.Save();
                    tagInfo = newTagInfo;
                }
            }

            BizBookmarkTagInfo bookmarkTagInfo = new BizBookmarkTagInfo();
            bookmarkTagInfo.CreateTime = DateTime.Now;
            bookmarkTagInfo.BookmarkInfoID = BookmarkInfoID;
            bookmarkTagInfo.UserInfoID = UserInfoID;
            bookmarkTagInfo.TagInfoID = tagInfo.TagInfoID;
            bookmarkTagInfo.Save(); 
        }


        public void RemoveByBookmarkTagID(long bookmarkTagInfoID)
        {
            if(BizBookmarkTagInfoList!=null)
            {
                var bookmarkTagInfo = BizBookmarkTagInfoList.Find(model => model.BookmarkTagInfoID == bookmarkTagInfoID);
                if(bookmarkTagInfo!=null)
                {
                    BizBookmarkTagInfo.DeleteByBookmarkTagInfoID(bookmarkTagInfo.BookmarkTagInfoID);
                    BizBookmarkTagInfoList.Remove(bookmarkTagInfo);
                }
            }
        }

        public void RemoveByTagName(string tagName)
        {
            if (BizBookmarkTagInfoList != null)
            {
                var tagInfo = BizTagInfo.LoadByTagNameAndUserID(tagName,UserInfoID);
                if(tagInfo==null)
                    return;

                var bookmarkTagInfo = BizBookmarkTagInfoList.Find(model => model.TagInfoID == tagInfo.TagInfoID);
                if (bookmarkTagInfo != null)
                {
                    BizBookmarkTagInfo.DeleteByBookmarkTagInfoID(bookmarkTagInfo.BookmarkTagInfoID);
                    BizBookmarkTagInfoList.Remove(bookmarkTagInfo);
                }
            }
        }


       

        private void CreateDynamicInfo()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var model = DAL.GetByUserInfoIAndHashcode(UserInfoID, HashCode);
                    if (model != null)
                    {
                        BookmarkInfoID = model.BookmarkInfoID;
                        UserDynamicInfoBo.CreateDynamicInfoMessage(UserInfoID, DynamicInfoType.NewBookmark, this);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteException("CreateDynamicInfoMessage", ex, new { UserInfoID = UserInfoID, Bookmark = this });
                }

            });
        }


        


    }
}
