//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;
using System.Linq;

namespace WebBookmarkBo.Model
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class BizBookmarkTagInfo
	{

        private static BookmarkTagInfoDAL DAL = new BookmarkTagInfoDAL();
        /// <summary>
        /// 主键
        /// </summary>
        public long BookmarkTagInfoID { get; set; }

        /// <summary>
        /// 书签ID
        /// </summary>
        public long BookmarkInfoID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserInfoID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 标签页主键
        /// </summary>
        public long TagInfoID { get; set; }


        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public BookmarkTagInfo ToModel()
        {
            return new BookmarkTagInfo()
            {
                BookmarkTagInfoID = BookmarkTagInfoID,
                BookmarkInfoID = BookmarkInfoID,
                UserInfoID = UserInfoID,
                CreateTime = CreateTime,
                TagInfoID = TagInfoID,
            };
        }


        public BizBookmarkTagInfo(BookmarkTagInfo dataInfo)
        {
            BookmarkTagInfoID = dataInfo.BookmarkTagInfoID;
            BookmarkInfoID = dataInfo.BookmarkInfoID;
            UserInfoID = dataInfo.UserInfoID;
            CreateTime = dataInfo.CreateTime;
            TagInfoID = dataInfo.TagInfoID;
        }

        public BizBookmarkTagInfo()
        {

        } 

        public void Save()
        {
            if(BookmarkTagInfoID!=0)
            {
                DAL.Update(ToModel());
            }else
            {
                DAL.Add(ToModel());
            }
        }
        
        public static List<BizBookmarkTagInfo> LoadByBookmarkID(long bookmarkID)
        {
            var lstModel = DAL.GetByBookmarkID(bookmarkID);
            return lstModel == null ? new List<BizBookmarkTagInfo>() : lstModel.Select(model => new BizBookmarkTagInfo(model)).ToList();
           
        }

        public static bool DeleteByBookmarkTagInfoID(long bookmarkTagInfoID)
        {
            return DAL.DeleteByBookmarkTagInfoID(bookmarkTagInfoID) >=0;
        }
        
	}
}