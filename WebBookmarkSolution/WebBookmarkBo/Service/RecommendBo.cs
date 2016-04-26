using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkBo.Model;
using WebBookmarkService.DAL;

namespace WebBookmarkBo.Service
{
    public class RecommendBo
    {
        /// <summary>
        /// 获取推荐用户ID（通过用户书签的host记录）
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<long> GetRecommendUserIDListDependHost(long userID)
        {
            var userBookmarkList = BookmarkInfoBo.LoadByUID(userID);
            if(userBookmarkList==null)
            {
                return new List<long>();
            }
            var hosts = userBookmarkList.Select(model => model.Host).Distinct().ToList();
            var lstBookmark = new BookmarkInfoDAL().GetListByHosts(hosts,userID);
            return lstBookmark.Select(model => model.UserInfoID).Distinct().ToList();//获取用户ID
        }


        public static List<BizBookmarkInfo> GetRecommendBookmarkList(long userID,int starIndex =0,int length=0)
        {
            var userBookmarkList = BookmarkInfoBo.LoadByUID(userID);
            if (userBookmarkList == null)
            {
                return new List<BizBookmarkInfo>();
            }
            var hosts = userBookmarkList.Select(model => model.Host).Distinct().ToList();
            hosts= Extend.GetRandomList(hosts);
            if(hosts.Count>10)
            {
                hosts = hosts.Take(10).ToList();
            }
            
            var lstBookmark = new BookmarkInfoDAL().GetListByHosts(hosts, userID,starIndex,length);
            return lstBookmark.Select(model => new BizBookmarkInfo(model)).ToList();
        }

        /// <summary>
        /// 获取相同域名的书签数据（排除书签所有者的数据）
        /// </summary>
        /// <param name="bookmarkID">书签ID</param>
        /// <param name="starIndex">分页Index</param>
        /// <param name="length">分页长度</param>
        /// <returns></returns>
        public static List<BizBookmarkInfo> LoadSameHostBookmarkList(long bookmarkID,int starIndex =0,int length=0)
        {
            var lstModel = new BookmarkInfoDAL().GetSameHostBookmarkByBookmarkID(bookmarkID,starIndex,length);
            if (lstModel != null)
                return lstModel.Select(model => new BizBookmarkInfo(model)).ToList();
            return new List<BizBookmarkInfo>();
        }



    }
}
