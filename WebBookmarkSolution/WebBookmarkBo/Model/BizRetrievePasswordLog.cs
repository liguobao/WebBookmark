//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkBo.Model
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class BizRetrievePasswordLog
	{

        private static readonly RetrievePasswordLogDAL DAL = new RetrievePasswordLogDAL();
        /// <summary>
        /// 
        /// </summary>
		public long RetrievePasswordLogID{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 当前状态
        /// </summary>
		public int LogStatus{get;set;}
            
        /// <summary>
        /// token
        /// </summary>
		public string Token{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 最后更新时间
        /// </summary>
		public DateTime LastTime{get;set;}
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public RetrievePasswordLog ToModel()
        {
            return new RetrievePasswordLog()
            {
                RetrievePasswordLogID =  RetrievePasswordLogID,
                UserInfoID =  UserInfoID,
                LogStatus =  LogStatus,
                Token =  Token,
                CreateTime =  CreateTime,
                LastTime =  LastTime,
            };
        }
        
        
        public BizRetrievePasswordLog (RetrievePasswordLog dataInfo)
        {
             RetrievePasswordLogID =  dataInfo.RetrievePasswordLogID;
             UserInfoID =  dataInfo.UserInfoID;
             LogStatus =  dataInfo.LogStatus;
             Token =  dataInfo.Token;
             CreateTime =  dataInfo.CreateTime;
             LastTime =  dataInfo.LastTime;
        }
        
        public  BizRetrievePasswordLog ()
        {
        
        } 


        public void Save()
        {
            if(RetrievePasswordLogID==0)
            {
                DAL.Add(ToModel());
            }else
            {
                DAL.Update(ToModel());
            }
        }



        public static BizRetrievePasswordLog LoadByToken(string token)
        {
            var model =DAL.GetByToken(token);
            return model != null ? new BizRetrievePasswordLog(model) : null;
        }
        
	}
}