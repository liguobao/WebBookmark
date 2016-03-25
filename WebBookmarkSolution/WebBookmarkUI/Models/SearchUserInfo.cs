using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBookmarkUI.Models
{
    public class SearchUserInfo
    {

        public long UserInfoID { get; set; }

        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public string Phone { get; set; }

        public string QQ { get; set; }



        public string UserInfoComment { get; set; }


        public string UserImagURL { get; set; }


        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否已关注
        /// </summary>
        public bool IsFollow { get; set; }
    }
}