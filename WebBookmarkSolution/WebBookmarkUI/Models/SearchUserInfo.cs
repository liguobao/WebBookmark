using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBookmarkUI.Models
{
    public class SearchUserInfo :UIUserInfo
    {
        /// <summary>
        /// 是否已关注
        /// </summary>
        public bool IsFollow { get; set; }
    }
}