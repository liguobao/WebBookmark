//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace WebBookmarkService.Model
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class UserInfo
	{
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 用户登陆名称
        /// </summary>
		public string UserLoginName{get;set;}
            
        /// <summary>
        /// 用户密码
        /// </summary>
		public string UserPassword{get;set;}
            
        /// <summary>
        /// 用户名
        /// </summary>
		public string UserName{get;set;}
            
        /// <summary>
        /// 用户邮箱
        /// </summary>
		public string UserEmail{get;set;}
            
        /// <summary>
        /// 用户手机号码
        /// </summary>
		public string UserPhone{get;set;}
            
        /// <summary>
        /// QQ
        /// </summary>
		public string UserQQ{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 用户头像URL
        /// </summary>
		public string UserImagURL{get;set;}
            
        /// <summary>
        /// 用户个人简介
        /// </summary>
		public string UserInfoComment{get;set;}
            
        /// <summary>
        /// 激活账号token
        /// </summary>
		public string ActivateAccountToken{get;set;}
            
        /// <summary>
        /// 账号状态。0：未激活；1：已激活
        /// </summary>
		public int AccountStatus{get;set;}
            
	}
}