using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkBo.Model;
using WebBookmarkService;

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
                    }
                }catch(Exception ex)
                {
                    LogHelper.WriteException("CreateMessage Exception", ex, new { UserID = userID, MessageTypeEnum = type, ObjectInfo = info });
                }
            });
           
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



    }




}
