//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace WebBookmarkService.BizModel
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class BizUserWebFolder
	{
        /// <summary>
        /// 
        /// </summary>
		public long UserWebFolderID{get;set;}
            
        /// <summary>
        /// 收藏夹名称
        /// </summary>
		public string WebFolderName{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 等级,0:对外公开，1：对关注者公开，2对群组公开，3：仅自己可见
        /// </summary>
		public int Grade{get;set;}
            
        /// <summary>
        /// 父收藏夹ID
        /// </summary>
		public long ParentWebfolderID{get;set;}
            
        /// <summary>
        /// 收藏夹描述
        /// </summary>
		public string IntroContent{get;set;}
            
        /// <summary>
        /// IElement 序列化数据
        /// </summary>
		public string IElementJSON{get;set;}
            
        /// <summary>
        /// IElementHashcode
        /// </summary>
		public int IElementHashcode{get;set;}
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public UserWebFolder ToModel()
        {
            return new UserWebFolder()
            {
                UserWebFolderID =  UserWebFolderID,
                WebFolderName =  WebFolderName,
                UserInfoID =  UserInfoID,
                CreateTime =  CreateTime,
                Grade =  Grade,
                ParentWebfolderID =  ParentWebfolderID,
                IntroContent =  IntroContent,
                IElementJSON =  IElementJSON,
                IElementHashcode =  IElementHashcode,
            };
        }
        
        
        public BizUserWebFolder (UserWebFolder dataInfo)
        {
             UserWebFolderID =  dataInfo.UserWebFolderID;
             WebFolderName =  dataInfo.WebFolderName;
             UserInfoID =  dataInfo.UserInfoID;
             CreateTime =  dataInfo.CreateTime;
             Grade =  dataInfo.Grade;
             ParentWebfolderID =  dataInfo.ParentWebfolderID;
             IntroContent =  dataInfo.IntroContent;
             IElementJSON =  dataInfo.IElementJSON;
             IElementHashcode =  dataInfo.IElementHashcode;
        }
        
        public  BizUserWebFolder ()
        {
        
        } 
        
	}
}