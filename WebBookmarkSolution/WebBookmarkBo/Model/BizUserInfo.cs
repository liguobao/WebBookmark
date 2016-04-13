using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkBo.Model
{
    public  class BizUserInfo
    {
        #region 属性

        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID { get; set; }

        /// <summary>
        /// 用户登陆名称
        /// </summary>
        public string UserLoginName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string UserPhone { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string UserQQ { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        private string imagURL;
        /// <summary>
        /// 用户头像URL
        /// </summary>
        public string UserImagURL {
            get
            {

                return string.IsNullOrEmpty(imagURL) ? "http://s.amazeui.org/media/i/demos/bw-2014-06-19.jpg?imageView/1/w/200/h/200/q/80" : imagURL;
            } 
            set
            {
                imagURL = value;
            } 
        }

        /// <summary>
        /// 用户个人简介
        /// </summary>
        public string UserInfoComment { get; set; }


        #endregion

        public UserInfo ToModel()
        {
            return new UserInfo()
            {
                CreateTime = DateTime.Now,
                UserEmail = UserEmail,
                UserInfoID = UserInfoID,
                UserLoginName = UserLoginName,
                UserName = UserName,
                UserPassword = UserPassword,
                UserPhone = UserPhone,
                UserQQ = UserQQ,
                UserImagURL = UserImagURL,
                UserInfoComment = UserInfoComment,
            };
        }


        public BizUserInfo()
        {

        }



       public BizUserInfo(UserInfo dataInfo)
        {
            if (dataInfo == null)
                return;

            CreateTime = dataInfo.CreateTime;
            UserEmail = dataInfo.UserEmail;
            UserInfoID = dataInfo.UserInfoID;
            UserLoginName = dataInfo.UserLoginName;
            UserName = dataInfo.UserName;
            UserPassword = dataInfo.UserPassword;
            UserPhone = dataInfo. UserPhone;
            UserQQ = dataInfo.UserQQ;
            UserInfoComment = dataInfo.UserInfoComment;
            UserImagURL = dataInfo.UserImagURL;
        }

      

       public void Save()
       {
            if(UserInfoID!=0)
            {
               new UserInfoDAL().Update(this.ToModel());
            }
            else
            {
                new UserInfoDAL().Add(this.ToModel());
            }
        }


       public static BizUserInfo LoadByUserInfoID(long userInfoID)
        {
            return new BizUserInfo(new UserInfoDAL().GetByUserInfoID(userInfoID));
        }


       public static BizUserInfo LoadByUserEmailOrUserLoginName(string emailOrLoginName)
       {
            return new BizUserInfo(new UserInfoDAL().GetByUserEmailOrUserLoginName(emailOrLoginName));
       }
    }
}
