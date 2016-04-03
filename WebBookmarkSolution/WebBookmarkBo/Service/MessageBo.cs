using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkBo.Model;
using WebBookmarkService;
using WebBookmarkService.BizModel;

namespace WebBookmarkBo.Service
{
    public class MessageBo
    {
        public static void CreateMessage(long userID,MessageTypeEnum type,object info)
        {
            Task.Factory.StartNew(() => 
            {
                try
                {
                    switch (type)
                    {
                        case MessageTypeEnum.WelcomeToWebBookmark:
                            WelcomeToWebBookmark(userID, info);
                            return;
                        case MessageTypeEnum.ImportBookmarkSuccess:
                            ImportBookmarkSuccess(userID);
                            return;
                        case MessageTypeEnum.ImportBookmarkFail:
                            ImportBookmarkFail(userID);
                            return;
                        case MessageTypeEnum.NewBookmarkComment:
                            NewBookmarkComment(userID,info);
                            return;
                        case MessageTypeEnum.NewBeFollow:
                            NewBeFollow(userID,info);
                            return;
                        case MessageTypeEnum.JoinGroupSuccess:
                            JoinGroupSuccess(info);
                            return;
                        case MessageTypeEnum.QuitGroupSuccess:
                            QuitGroup(info);
                            return;
                        case MessageTypeEnum.ApplyJoinGroup:
                            ApplyJoinGroup(info);
                            return;
                        case MessageTypeEnum.RemoveGroup:
                            RemoveGroupUser(info);
                            return;
                        case MessageTypeEnum.RejecrApplyJoinGroup:
                            RejectGroupUser(info);
                            return;
                    }
                }catch(Exception ex)
                {
                    LogHelper.WriteException("CreateMessage Exception", ex, new { UserID = userID, MessageTypeEnum = type, ObjectInfo = info });
                }
            });
           
        }

        private static void ImportBookmarkSuccess(long userID)
        {
            BizMessageInfo messageInfo = new BizMessageInfo();
            messageInfo.CreateTime = DateTime.Now;
            messageInfo.IsRead = (int)MessageReadStatus.NotRead;
            messageInfo.MessageTitle = "成功导入书签数据";
            messageInfo.MessageContent = string.Format("你已经成功导入书签数据到WebBookmark里面,赶紧去看看你的书签吧。");
            messageInfo.MessageURL = "~/WebBookmarkTable";
            messageInfo.UserInfoID = userID;
            messageInfo.MessageInfoType = (int)MessageTypeEnum.ImportBookmarkSuccess;
            messageInfo.Save();
        }


        private static void ImportBookmarkFail(long userID)
        {
            BizMessageInfo messageInfo = new BizMessageInfo();
            messageInfo.CreateTime = DateTime.Now;
            messageInfo.IsRead = (int)MessageReadStatus.NotRead;
            messageInfo.MessageTitle = "导入书签数据失败";
            messageInfo.MessageContent = @"导入书签数据失败。可能是因为书签文件不是合法的数据文件，请使用现代浏览器(chrome、firebox、ie10之类)的书签文件。
                                         反正打死不认是系统问题。点击下面重新导入试试吧。^-^";
            messageInfo.MessageURL = "~/WebBookmarkTable/ImportWebBookmark";
            messageInfo.UserInfoID = userID;
            messageInfo.MessageInfoType = (int)MessageTypeEnum.ImportBookmarkFail;
            messageInfo.Save();
        }

        private static void NewBookmarkComment(long userID, object info)
        {
            var bookmarkComment = info as BizBookmarkComment;
            var bookmarkInfo = BizBookmarkInfo.LoadByID(bookmarkComment.BookmarkInfoID);
            var criticsUser = BizUserInfo.LoadByUserInfoID(bookmarkComment.CriticsUserID);
            BizMessageInfo messageInfo = new BizMessageInfo();
            messageInfo.CreateTime = DateTime.Now;
            messageInfo.IsRead = (int)MessageReadStatus.NotRead;
            messageInfo.MessageTitle =string.Format("书签：【{0}】评论",bookmarkInfo.BookmarkName);
            messageInfo.MessageContent = string.Format("书签：【{0}】 新增来自【{1}】的评论，点击下面的链接去查看评论啦。^-^",
                bookmarkInfo.BookmarkName,criticsUser.UserName);
            messageInfo.MessageURL = "~/ShowBookmarkInfo?bookmarkID="+ bookmarkInfo.BookmarkInfoID;
            messageInfo.UserInfoID = userID;
            messageInfo.MessageInfoType = (int)MessageTypeEnum.NewBookmarkComment;
            messageInfo.Save();
        }


        private static void WelcomeToWebBookmark(long userID, object info)
        {
            var userInfo = info as BizUserInfo;
            BizMessageInfo messageInfo = new BizMessageInfo();
            messageInfo.CreateTime = DateTime.Now;
            messageInfo.IsRead = (int)MessageReadStatus.NotRead;
            messageInfo.MessageTitle = "欢迎加入WebBookmark";
            messageInfo.MessageContent = string.Format("欢迎加入WebBookmark,你是我们这里的第{0}位用户。先去完善一下你的个人信息吧。", userInfo.UserInfoID);
            messageInfo.MessageURL = "~/UserInfo";
            messageInfo.UserInfoID = userID;
            messageInfo.MessageInfoType = (int)MessageTypeEnum.WelcomeToWebBookmark;
            messageInfo.Save();
        }


        private static void NewBeFollow(long userID, object info)
        {
            var userRelationship = info as BizUserRelationship;

            var befollowUser = BizUserInfo.LoadByUserInfoID(userRelationship.BeFollwedUID);
            var follwer = BizUserInfo.LoadByUserInfoID(userRelationship.FollowerID);
          
            BizMessageInfo beFollowMessageInfo = new BizMessageInfo();
            beFollowMessageInfo.CreateTime = DateTime.Now;
            beFollowMessageInfo.IsRead = (int)MessageReadStatus.NotRead;
            beFollowMessageInfo.MessageTitle =string.Format( "新的关注者【{0}】",follwer.UserName);
            beFollowMessageInfo.MessageContent = "你有了新的关注者 【{0}】,点击下面的链接可以去看他的个人信息哦。";
            beFollowMessageInfo.MessageURL = "~/UserInfo/ShowUserDetail?uid=" + follwer.UserInfoID;
            beFollowMessageInfo.UserInfoID = befollowUser.UserInfoID;
            beFollowMessageInfo.MessageInfoType = (int)MessageTypeEnum.NewBeFollow;
            beFollowMessageInfo.Save();

            BizMessageInfo followerMessageInfo = new BizMessageInfo();
            followerMessageInfo.CreateTime = DateTime.Now;
            followerMessageInfo.IsRead = (int)MessageReadStatus.NotRead;
            followerMessageInfo.MessageTitle =string.Format( "关注【{0}】成功！",befollowUser.UserName);
            followerMessageInfo.MessageContent =string.Format( "你成功关注了【{0}】，点击下面的链接可以去看他公开的个人信息哦。",befollowUser.UserName);
            followerMessageInfo.MessageURL = "~/UserInfo/ShowUserDetail?uid=" + befollowUser.UserInfoID; ;
            followerMessageInfo.UserInfoID = follwer.UserInfoID;
            followerMessageInfo.MessageInfoType = (int)MessageTypeEnum.FollowUser;
            followerMessageInfo.Save();
        }



        private static void ApplyJoinGroup(object info)
        {
            var groupUser = info as BizGroupUser;
            if(groupUser!=null)
            {
                var groupInfo = BizGroupInfo.LoadByGroupID(groupUser.GroupInfoID);
                var applyUserInfo = BizUserInfo.LoadByUserInfoID(groupUser.UserInfoID);

                BizMessageInfo messageInfo = new BizMessageInfo();
                messageInfo.CreateTime = DateTime.Now;
                messageInfo.IsRead = (int)MessageReadStatus.NotRead;
                messageInfo.MessageTitle = string.Format("用户【{0}】正在申请加入群组【{1}】。", applyUserInfo.UserName,groupInfo.GroupName);
                messageInfo.MessageContent = string.Format("用户【{0}】正在申请加入群组【{1}】，可以点击下面的链接去通过申请或者直接驳回。", applyUserInfo.UserName, groupInfo.GroupName);
                messageInfo.MessageURL = "~/GroupInfo/ShowGroupDetail?groupID=" + groupInfo.GroupInfoID;
                messageInfo.UserInfoID = groupInfo.CreateUesrID;
                messageInfo.MessageInfoType = (int)MessageTypeEnum.ApplyJoinGroup;
                messageInfo.Save();
            }
          
        }

        private static void JoinGroupSuccess(object info)
        {
            var groupUser = info as BizGroupUser;
            if (groupUser != null)
            {
                var groupInfo = BizGroupInfo.LoadByGroupID(groupUser.GroupInfoID);
                var applyUserInfo = BizUserInfo.LoadByUserInfoID(groupUser.UserInfoID);

                BizMessageInfo messageInfo = new BizMessageInfo();
                messageInfo.CreateTime = DateTime.Now;
                messageInfo.IsRead = (int)MessageReadStatus.NotRead;
                messageInfo.MessageTitle = string.Format("成功加入群组【{0}】", groupInfo.GroupName);
                messageInfo.MessageContent = string.Format("群组大人已经通过了你的申请，现在你已经是群组【{0}】的一员了。撒花~~~~。你可以点击下面的链接去查看你的申请记录哦。",
                    groupInfo.GroupName);
                messageInfo.MessageURL = "~/ShowMyAllGroup";
                messageInfo.UserInfoID = applyUserInfo.UserInfoID;
                messageInfo.MessageInfoType = (int)MessageTypeEnum.JoinGroupSuccess;
                messageInfo.Save();
            }

        }


        private static void QuitGroup(object info)
        {
            var groupUser = info as BizGroupUser;
            if (groupUser != null)
            {
                var groupInfo = BizGroupInfo.LoadByGroupID(groupUser.GroupInfoID);
                var applyUserInfo = BizUserInfo.LoadByUserInfoID(groupUser.UserInfoID);

                BizMessageInfo messageInfo = new BizMessageInfo();
                messageInfo.CreateTime = DateTime.Now;
                messageInfo.IsRead = (int)MessageReadStatus.NotRead;
                messageInfo.MessageTitle = string.Format("已退出群组【{0}】", groupInfo.GroupName);
                messageInfo.MessageContent = string.Format("你已经成功退出群组【{0}】。点击下面的链接可以查看你的申请记录哦。",
                    groupInfo.GroupName);
                messageInfo.MessageURL = "~/ShowMyAllGroup";
                messageInfo.UserInfoID = applyUserInfo.UserInfoID;
                messageInfo.MessageInfoType = (int)MessageTypeEnum.QuitGroupSuccess;
                messageInfo.Save();


                BizMessageInfo quitGroupMessage = new BizMessageInfo();
                quitGroupMessage.CreateTime = DateTime.Now;
                quitGroupMessage.IsRead = (int)MessageReadStatus.NotRead;
                quitGroupMessage.MessageTitle = string.Format("用户【{0}】已退出群组【{1}】", applyUserInfo.UserName,groupInfo.GroupName);
                quitGroupMessage.MessageContent = string.Format("用户【{0}】已退出群组【{1}】。点击下面的链接去查看该群组相关成员。",
                    applyUserInfo.UserName, groupInfo.GroupName);
                quitGroupMessage.MessageURL = "~/GroupInfo/ShowGroupDetail?groupID=" + groupInfo.GroupInfoID;
                quitGroupMessage.UserInfoID = groupInfo.CreateUesrID;
                quitGroupMessage.MessageInfoType = (int)MessageTypeEnum.QuitGroupSuccess;
                quitGroupMessage.Save();
            }

        }

        private static void RemoveGroupUser(object info)
        {
            var groupUser = info as BizGroupUser;
            if (groupUser != null)
            {
                var groupInfo = BizGroupInfo.LoadByGroupID(groupUser.GroupInfoID);
                var applyUserInfo = BizUserInfo.LoadByUserInfoID(groupUser.UserInfoID);

                BizMessageInfo messageInfo = new BizMessageInfo();
                messageInfo.CreateTime = DateTime.Now;
                messageInfo.IsRead = (int)MessageReadStatus.NotRead;
                messageInfo.MessageTitle = string.Format("已被移出群组【{0}】", groupInfo.GroupName);
                messageInfo.MessageContent = string.Format("你已经被群组管理员从群组【{0}】移除。点击下面的链接可以查看你的申请记录。",
                    groupInfo.GroupName);
                messageInfo.MessageURL = "~/ShowMyAllGroup";
                messageInfo.UserInfoID = applyUserInfo.UserInfoID;
                messageInfo.MessageInfoType = (int)MessageTypeEnum.RemoveGroup;
                messageInfo.Save();

            }

        }


        private static void RejectGroupUser(object info)
        {
            var groupUser = info as BizGroupUser;
            if (groupUser != null)
            {
                var groupInfo = BizGroupInfo.LoadByGroupID(groupUser.GroupInfoID);
                var applyUserInfo = BizUserInfo.LoadByUserInfoID(groupUser.UserInfoID);

                BizMessageInfo messageInfo = new BizMessageInfo();
                messageInfo.CreateTime = DateTime.Now;
                messageInfo.IsRead = (int)MessageReadStatus.NotRead;
                messageInfo.MessageTitle = string.Format("加入群组【{0}】申请被驳回", groupInfo.GroupName);
                messageInfo.MessageContent = string.Format("很遗憾告诉您，你的加入群组【{0}】申请被驳回了。点击下面的链接可以查看你的申请记录。",
                    groupInfo.GroupName);
                messageInfo.MessageURL = "~/ShowMyAllGroup";
                messageInfo.UserInfoID = applyUserInfo.UserInfoID;
                messageInfo.MessageInfoType = (int)MessageTypeEnum.RejecrApplyJoinGroup;
                messageInfo.Save();
            }

        }



    }




}
