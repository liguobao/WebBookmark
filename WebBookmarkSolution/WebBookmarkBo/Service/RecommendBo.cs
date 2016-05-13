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

        /// <summary>
        /// 获取推荐用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<BizUserInfo> GetRecommendUserList(long userID)
        {
            var lstUser = new UserInfoDAL().GetSameHrefUserList(userID);
            if (lstUser != null)
                return lstUser.Select(model => new BizUserInfo(model)).ToList();
            return new List<BizUserInfo>();
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
        /// 随机获取书签数据
        /// </summary>
        /// <param name="starIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static List<BizBookmarkInfo> GetRandomBookmarkList(int starIndex = 0, int length = 0)
        {
            var lstBookmark = new BookmarkInfoDAL().GetRandomList(starIndex, length);
            if(lstBookmark!=null)
            {
                lstBookmark = lstBookmark.DistinctBy(bk=>bk.Href).DistinctBy(bk=>bk.UserInfoID);
                return lstBookmark.Select(model => new BizBookmarkInfo(model)).ToList();
            }else
            { 
                return new List<BizBookmarkInfo>();
            }
           
        }


        /// <summary>
        /// 获取推荐用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<BizUserInfo> GetRandomUserList()
        {
            Random rm = new Random(DateTime.Now.Millisecond);
            var length = rm.Next(0, 20);
            var lstUser = new UserInfoDAL().GetRandomList(0, length);
            if (lstUser != null)
                return lstUser.Select(model => new BizUserInfo(model)).ToList();
            return new List<BizUserInfo>();
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
