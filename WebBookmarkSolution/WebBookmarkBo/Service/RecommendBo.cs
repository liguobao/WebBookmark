using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var rcommendUserIDList = new List<long>();
            var userBookmarkList = BookmarkInfoBo.LoadByUID(userID);
            var dicURLToHost= new Dictionary<string,string>();
            if(userBookmarkList!=null)
            {
                foreach(var bookmark in userBookmarkList)
                {
                    if (!dicURLToHost.ContainsKey(bookmark.Href))
                        dicURLToHost.Add(bookmark.Href,bookmark.Host);
                }
            }
            var hosts = dicURLToHost.Values.Distinct().ToList();
            var lstBookmark = new BookmarkInfoDAL().GetListByHosts(hosts,userID);
            return lstBookmark.Select(model => model.UserInfoID).Distinct().ToList();//获取用户ID
        }
    }
}
