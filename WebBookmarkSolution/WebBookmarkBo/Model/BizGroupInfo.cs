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
    public class BizGroupInfo
    {
        /// <summary>
        /// 主键，自增
        /// </summary>
        public long GroupInfoID { get; set; }

        /// <summary>
        /// 用户群组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 用户群组介绍
        /// </summary>
        public string GroupIntro { get; set; }

        /// <summary>
        /// UID
        /// </summary>
        public long CreateUesrID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public GroupInfo ToModel()
        {
            return new GroupInfo()
            {
                GroupInfoID = GroupInfoID,
                GroupName = GroupName,
                GroupIntro = GroupIntro,
                CreateUesrID = CreateUesrID,
                CreateTime = CreateTime,
            };
        }


        public BizGroupInfo(GroupInfo dataInfo)
        {
            GroupInfoID = dataInfo.GroupInfoID;
            GroupName = dataInfo.GroupName;
            GroupIntro = dataInfo.GroupIntro;
            CreateUesrID = dataInfo.CreateUesrID;
            CreateTime = dataInfo.CreateTime;
        }

        public BizGroupInfo()
        {

        }

        public GroupInfo ToModle()
        {
            return new GroupInfo()
            {
                GroupName = GroupName,
                GroupInfoID = GroupInfoID,
                GroupIntro = GroupIntro,
                CreateTime = CreateTime,
                CreateUesrID = CreateUesrID,
            };
        }



        public void Save()
        {
            if (GroupInfoID != 0)
            {
                new GroupInfoDAL().Update(ToModle());
            }
            else
            {
                new GroupInfoDAL().Add(ToModle());
            }
        }

        /// <summary>
        /// 根据用户群组ID获取群组信息
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static BizGroupInfo LoadByGroupID(long groupID)
        {
            var groupInfo = new GroupInfoDAL().GetByGroupInfoID(groupID);
            if (groupInfo == null)
                return null;
            return new BizGroupInfo(groupInfo);
        }

        /// <summary>
        /// 根据创建者ID获取群组信息
        /// </summary>
        /// <param name="createUesrID"></param>
        /// <returns></returns>
        public static List<BizGroupInfo> LoadByCreateUserID(long createUesrID)
        {
            var groupInfos = new GroupInfoDAL().GetByCreateUesrID(createUesrID);
            if (groupInfos == null)
                return new List<BizGroupInfo>();
            return groupInfos.Select(model => new BizGroupInfo(model)).ToList();
        }

    }
}
