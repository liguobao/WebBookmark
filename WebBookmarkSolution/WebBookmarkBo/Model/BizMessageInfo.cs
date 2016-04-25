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
	public class BizMessageInfo
	{

        private static readonly MessageInfoDAL DAL = new MessageInfoDAL();

        #region 属性

        /// <summary>
        /// 主键，自增
        /// </summary>
		public long MessageInfoID{get;set;}
            
        /// <summary>
        /// 消息主题
        /// </summary>
		public string MessageTitle{get;set;}
            
        /// <summary>
        /// 消息内容
        /// </summary>
		public string MessageContent{get;set;}
            
        /// <summary>
        /// UID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 是否已读
        /// </summary>
		public int IsRead{get;set;}
            
        /// <summary>
        /// 消息类型
        /// </summary>
		public int MessageInfoType{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 消息相关URL
        /// </summary>
		public string MessageURL{get;set;}


        #endregion

        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public MessageInfo ToModel()
        {
            return new MessageInfo()
            {
                MessageInfoID =  MessageInfoID,
                MessageTitle =  MessageTitle,
                MessageContent =  MessageContent,
                UserInfoID =  UserInfoID,
                IsRead =  IsRead,
                MessageInfoType =  MessageInfoType,
                CreateTime =  CreateTime,
                MessageURL =  MessageURL,
            };
        }
        
        
        public BizMessageInfo (MessageInfo dataInfo)
        {
             MessageInfoID =  dataInfo.MessageInfoID;
             MessageTitle =  dataInfo.MessageTitle;
             MessageContent =  dataInfo.MessageContent;
             UserInfoID =  dataInfo.UserInfoID;
             IsRead =  dataInfo.IsRead;
             MessageInfoType =  dataInfo.MessageInfoType;
             CreateTime =  dataInfo.CreateTime;
             MessageURL =  dataInfo.MessageURL;
        }
        
        public  BizMessageInfo ()
        {
        
        }

        /// <summary>
        /// 落地到库
        /// </summary>
        public void Save()
        {
            if(MessageInfoID==0)
            {
                DAL.Add(ToModel());
            }
            else
            {
                DAL.Update(ToModel());
            }
        }

        /// <summary>
        /// 设置为已读
        /// </summary>
        public void SetToHasRead()
        {
            IsRead = (int)MessageReadStatus.Read;
            DAL.Update(ToModel());
        }


        /// <summary>
        /// 加载用户的所有信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<BizMessageInfo> LoadByUserID(long userID)
        {
            var lstModel = DAL.GetListByUserInfoID(userID);
            if (lstModel == null)
                return new List<BizMessageInfo>();
            return lstModel.Select(model => new BizMessageInfo(model)).ToList();
        }

        /// <summary>
        /// 加载未读消息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<BizMessageInfo> LoadNotReadListByUserID(long userID)
        {
            var lstModel = DAL.GetNotReadListByUserInfoID(userID);
            if (lstModel == null)
                return new List<BizMessageInfo>();
            return lstModel.Select(model => new BizMessageInfo(model)).ToList();
        }


        /// <summary>
        ///加载已读消息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<BizMessageInfo> LoadHasReadListByUserID(long userID)
        {
            var lstModel = DAL.GetHasReadListByUserInfoID(userID);
            if (lstModel == null)
                return new List<BizMessageInfo>();
            return lstModel.Select(model => new BizMessageInfo(model)).ToList();
        }

        public static int GetNotReadMessageCount(long userID)
        {
            var lstModel = DAL.GetNotReadListByUserInfoID(userID);
            if (lstModel == null)
                return 0;
            return lstModel.Count();
          
        }

        public static BizMessageInfo LoadByMessageID(long messageID)
        {
            var model = DAL.GetByMessageInfoID(messageID);
            if (model == null)
                return null;
            return new BizMessageInfo(model);
        }


	}


    public enum MessageReadStatus
    {
        /// <summary>
        /// 未读
        /// </summary>
        NotRead = 0,

        /// <summary>
        /// 已读
        /// </summary>
        Read = 1,
    }


    public enum MessageTypeEnum 
    {
        /// <summary>
        /// 欢迎加入
        /// </summary>
        WelcomeToWebBookmark = 0,

        /// <summary>
        /// 关注用户
        /// </summary>
        FollowUser = 1,

        /// <summary>
        /// 新的关注者
        /// </summary>
        NewBeFollow = 2,

        /// <summary>
        /// 新的留言
        /// </summary>
        NewBookmarkComment = 3,

        /// <summary>
        /// 加入群组成功
        /// </summary>
        JoinGroupSuccess = 4,

        /// <summary>
        /// 退出群组成功
        /// </summary>
        QuitGroupSuccess =5,


        /// <summary>
        /// 被移除
        /// </summary>
        RemoveGroup = 6,

        /// <summary>
        /// 导入书签成功
        /// </summary>
        ImportBookmarkSuccess = 7,

        /// <summary>
        /// 导入书签失败
        /// </summary>
        ImportBookmarkFail = 8,

        /// <summary>
        /// 申请加入群组
        /// </summary>
        ApplyJoinGroup = 9,

        /// <summary>
        /// 加入群组被驳回
        /// </summary>
        RejecrApplyJoinGroup = 10,

        /// <summary>
        /// 新增点赞
        /// </summary>
        NewLikeBookmarkLog = 11,

    }
}