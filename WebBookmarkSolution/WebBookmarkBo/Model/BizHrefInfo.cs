using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBookmarkBo.Model
{
    public class BizHrefInfo
    {
        /// <summary>
        /// 主键，自增
        /// </summary>
        public long HrefInfoID { get; set; }

        /// <summary>
        /// 网页收藏夹ID
        /// </summary>
        public long UserWebBookmarkID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserInfoID { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 网页HTML
        /// </summary>
        public string HTML { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 导入XML信息
        /// </summary>
        public string ImportXML { get; set; }
    }
}
