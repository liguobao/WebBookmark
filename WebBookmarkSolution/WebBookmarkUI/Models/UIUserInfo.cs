using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookmarkBo.Model;

namespace WebBookmarkUI.Models
{
    public class UIUserInfo
    {

        public long UserInfoID { get; set; }

        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }


        public string Phone { get; set; }

        public string QQ { get; set; }


        public string LoginName { get; set; }


        public string UserInfoComment { get; set; }


        private string imagURL;
        /// <summary>
        /// 用户头像URL
        /// </summary>
        public string UserImagURL
        {
            get
            {

                return string.IsNullOrEmpty(imagURL) ? "http://s.amazeui.org/media/i/demos/bw-2014-06-19.jpg?imageView/1/w/200/h/200/q/80" : imagURL;
            }
            set
            {
                imagURL = value;
            }
        }


        public DateTime CreateTime { get; set; }
       
    }
   
}