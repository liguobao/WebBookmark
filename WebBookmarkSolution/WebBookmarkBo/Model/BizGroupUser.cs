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
	public class BizGroupUser
	{
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long GroupUserID{get;set;}
            
        /// <summary>
        /// 用户群组ID
        /// </summary>
		public long GroupInfoID{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 是否通过(0：未通过，1已通过)
        /// </summary>
		public int IsPass{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public GroupUser ToModel()
        {
            return new GroupUser()
            {
                GroupUserID =  GroupUserID,
                GroupInfoID =  GroupInfoID,
                UserInfoID =  UserInfoID,
                IsPass =  IsPass,
                CreateTime =  CreateTime,
            };
        }
        
        
        public BizGroupUser (GroupUser dataInfo)
        {
             GroupUserID =  dataInfo.GroupUserID;
             GroupInfoID =  dataInfo.GroupInfoID;
             UserInfoID =  dataInfo.UserInfoID;
             IsPass =  dataInfo.IsPass;
             CreateTime =  dataInfo.CreateTime;
        }
        
        public  BizGroupUser ()
        {
        
        }


        public void Save()
        {
            if(GroupUserID!=0)
            {
                new GroupUserDAL().Update(ToModel());
            }else
            {
                new GroupUserDAL().Add(ToModel());
            }
        }


        public static List<BizGroupUser> LoadGroupUser(long userID)
        {
            var lstModel = new GroupUserDAL().GetListByUserID(userID);
            return lstModel.Select(mode=>new BizGroupUser(mode)).ToList();
        }

        
        public static List<BizGroupUser> LoadByGroupID(long groupID)
        {
            var lstModel = new GroupUserDAL().GetListByGroupInfoID(groupID);
            return lstModel.Select(mode => new BizGroupUser(mode)).ToList();
        }


        public static List<BizGroupUser> LoadByGroupIDList(List< long> groupIDs)
        {
            var lstModel = new GroupUserDAL().GetListByGroupInfoIDs(groupIDs);
            return lstModel.Select(mode => new BizGroupUser(mode)).ToList();
        }




        public static BizGroupUser LoadByGroupUserID(long infoID)
        {
            var model = new GroupUserDAL().GetByGroupUserID(infoID);
            if (model == null)
                return null;
            return new BizGroupUser(model);
        }


        public static BizGroupUser LoadByUserIDAndGroupID(long uid, long groupID)
        {
            var model = new GroupUserDAL().GetByUserIDAndGroupInfoID(uid,groupID);
            if (model == null)
                return null;
            return new BizGroupUser(model);
        }
	}

    public enum ApplyStatus
    {

        /// <summary>
        /// 等待审核
        /// </summary>
        Waiting = 0,


        /// <summary>
        /// 通过
        /// </summary>
        Pass = 1,

        /// <summary>
        /// 驳回
        /// </summary>
        Reject =2,

        /// <summary>
        /// 移除
        /// </summary>
        Remove = 3,

        /// <summary>
        /// 退出
        /// </summary>
        Quit = 4,

    }
}