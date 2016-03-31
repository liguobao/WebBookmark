using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkBo.Model
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
        /// 记录当前对象创建时候的Hashcode，以便取出来
        /// </summary>
        public int ObjectHascode { get; set; }


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
                ObjectHashcode = ObjectHascode,
            };
        }


        public BizGroupInfo(GroupInfo dataInfo)
        {
            GroupInfoID = dataInfo.GroupInfoID;
            GroupName = dataInfo.GroupName;
            GroupIntro = dataInfo.GroupIntro;
            CreateUesrID = dataInfo.CreateUesrID;
            CreateTime = dataInfo.CreateTime;
            ObjectHascode = dataInfo.ObjectHashcode;
        }

        public BizGroupInfo()
        {

        } 



        public void Save()
        {
            if (GroupInfoID != 0)
            {
                new GroupInfoDAL().Update(ToModel());
            }
            else
            {
                var DAL = new GroupInfoDAL();
                DAL.Add(ToModel());
                if(ObjectHascode!=0)
                {
                    var model = DAL.GetByObjectHashCode(ObjectHascode);
                    if (model == null)
                        return;
                    GroupInfoID = model.GroupInfoID;
                }
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


        public static  List<BizGroupInfo> LoadByGroupIDList(List<long> groupIDs)
        {
            var groupInfos = new GroupInfoDAL().GetByGroupIDList(groupIDs);
            if (groupInfos == null)
                return new List<BizGroupInfo>();
            return groupInfos.Select(model => new BizGroupInfo(model)).ToList();
        }
      

        

    }
}
