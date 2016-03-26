using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkService.DAL;
using WebfolderBo.Model;

namespace WebfolderBo.Service
{
    public class UserRelationshipBo
    {

        private static UserRelationshipDAL DAL = new UserRelationshipDAL();

        /// <summary>
        /// 添加关注
        /// </summary>
        /// <param name="befollowID"></param>
        /// <param name="followID"></param>
        /// <returns></returns>
        public static bool CheckFollowStatus(long beFollwedUID, long followerID)
        {
            return DAL.CheckFollowStatus(followerID, beFollwedUID);
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="beFollwedUID"></param>
        /// <param name="followerID"></param>
        /// <returns></returns>
        public static bool UnFollowUser(long beFollwedUID, long followerID)
        {
            return DAL.DeleteFollowRelationship(followerID, beFollwedUID);
        }

        /// <summary>
        /// 获取用户所关注的人
        /// </summary>
        /// <param name="followUserID"></param>
        /// <returns>返回Dictionary,key为所关注的人，value为BizUserRelationship</returns>
        public static Dictionary<long,BizUserRelationship> GetByFollowUserID(long followUserID)
        {
            var lst = DAL.GetByFollowUserID(followUserID);
            if (lst == null || lst.Count() == 0)
                return new Dictionary<long,BizUserRelationship>();

            return lst.Select(model => new BizUserRelationship(model)).ToDictionary(model=>model.BeFollwedUID,model=>model);

        }

        /// <summary>
        /// 获取关注了此用户的人
        /// </summary>
        /// <param name="beFollwedUID"></param>
        /// <returns></returns>
        public static Dictionary<long, BizUserRelationship> GetByBeFollwedUID(long beFollwedUID)
        {
            var lst = DAL.GetByBeFollowUserID(beFollwedUID);
            if (lst == null || lst.Count() == 0)
                return new Dictionary<long, BizUserRelationship>();

            return lst.Select(model => new BizUserRelationship(model))
                .ToDictionary(model => model.FollowerID, model => model);

        }

    }
}
