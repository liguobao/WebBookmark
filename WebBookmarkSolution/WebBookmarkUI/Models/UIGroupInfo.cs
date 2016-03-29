using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBookmarkUI.Models
{
    public class UIGroupInfo
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


        public UIUserInfo CreateUesrInfo { get; set; }
    }
}