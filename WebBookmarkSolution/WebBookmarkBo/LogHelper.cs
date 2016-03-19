//============================================================
//http://codelover.link author:李国宝
//============================================================
using System;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkService.BizModel
{	
    /// <summary>
    /// 
    /// </summary>
	public static class LogHelper
    {
        /// <summary>
        /// 关键步骤记录
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="info"></param>
        public static void WriteInfo(string title,string content,object info=null)
        {
            SystemLog log = new SystemLog();
            log.CreateTime = DateTime.Now;
            log.LogContent = content;
            log.LogTitle = title;
            log.LogType = Convert.ToInt32(LogType.Info);
            if(info!=null)
            {
                log.DynamicInfo = Newtonsoft.Json.JsonConvert.SerializeObject(info);
            }
            
            new SystemLogDAL().Add(log);
        }


        /// <summary>
        /// 异常信息记录
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        /// <param name="info"></param>
        public static void WriteException(string title,Exception ex, object info)
        {
            SystemLog log = new SystemLog();
            log.CreateTime = DateTime.Now;
            log.LogContent = ex.ToString();
            log.LogTitle = title;
            log.LogType = Convert.ToInt32(LogType.Exception);
            if (info != null)
            {
                log.DynamicInfo = Newtonsoft.Json.JsonConvert.SerializeObject(info);
            }
            new SystemLogDAL().Add(log);
        }


        /// <summary>
        /// 调试信息记录
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name=""></param>
        /// <param name="info"></param>
        public static void WriteDebug(string title,string content, object info)
        {
            SystemLog log = new SystemLog();
            log.CreateTime = DateTime.Now;
            log.LogContent = content;
            log.LogTitle = title;
            log.LogType = Convert.ToInt32(LogType.Info);
            if (info != null)
            {
                log.DynamicInfo = Newtonsoft.Json.JsonConvert.SerializeObject(info);
            }
            new SystemLogDAL().Add(log);
        }

        /// <summary>
        /// 错误信息记录
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        /// <param name="info"></param>
        public static void WriteError(string title, Exception ex, object info =null)
        {
            SystemLog log = new SystemLog();
            log.CreateTime = DateTime.Now;
            log.LogContent = ex.ToString();
            log.LogTitle = title;
            log.LogType = Convert.ToInt32(LogType.Exception);
            if (info != null)
            {
                log.DynamicInfo = Newtonsoft.Json.JsonConvert.SerializeObject(info);
            }
            new SystemLogDAL().Add(log);
        }
    }

    public enum LogType
    {
        /// <summary>
        /// 记录类
        /// 
        /// </summary>
        Info = 0,

        /// <summary>
        /// 警告类
        /// </summary>
        Warning=1,

        /// <summary>
        /// 调试类
        /// </summary>
        Debug=2,

        /// <summary>
        /// 错误类
        /// </summary>
        Error=3,

        /// <summary>
        /// 
        /// </summary>
        Exception=4,


    }

    

}