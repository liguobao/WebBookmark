using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebfolderBo.Model
{
        /// <summary>
    /// 
    /// </summary>
    public class BizUserRelationship
    {
        /// <summary>
        /// 主键，自增
        /// </summary>
        public long UserRelationshipID { get; set; }

        /// <summary>
        /// 关注者ID
        /// </summary>
        public long FollowerID { get; set; }

        /// <summary>
        /// 被关注者ID
        /// </summary>
        public long BeFollwedUID { get; set; }

        /// <summary>
        /// 是否互相关注
        /// </summary>
        public sbyte IsMutuallyFollwe { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public UserRelationship ToModel()
        {
            return new UserRelationship()
            {
                UserRelationshipID = UserRelationshipID,
                FollowerID = FollowerID,
                BeFollwedUID = BeFollwedUID,
                IsMutuallyFollwe = IsMutuallyFollwe,
                CreateTime = CreateTime,
            };
        }


        public BizUserRelationship(UserRelationship dataInfo)
        {
            UserRelationshipID = dataInfo.UserRelationshipID;
            FollowerID = dataInfo.FollowerID;
            BeFollwedUID = dataInfo.BeFollwedUID;
            IsMutuallyFollwe = dataInfo.IsMutuallyFollwe;
            CreateTime = dataInfo.CreateTime;
        }

        public BizUserRelationship()
        {

        }

        public void Save()
        {
            if(UserRelationshipID!=0)
            {
                new UserRelationshipDAL().Update(ToModel());

            }else
            {
                new UserRelationshipDAL().Add(ToModel());
            }
        }
        


    }
}
