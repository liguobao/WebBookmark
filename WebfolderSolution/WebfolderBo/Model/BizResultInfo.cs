﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebfolderBo.Model
{
    public class BizResultInfo
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 返回值
        /// </summary>
        public long ResultID { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string ResultMessage { get; set; }


        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
