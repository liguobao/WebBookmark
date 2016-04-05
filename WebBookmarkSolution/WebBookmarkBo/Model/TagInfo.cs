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
	public class BizTagInfo
	{

        private static TagInfoDAL DAL = new TagInfoDAL();
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long TagInfoID{get;set;}
            
        /// <summary>
        /// URL信息ID
        /// </summary>
		public string TagName{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public TagInfo ToModel()
        {
            return new TagInfo()
            {
                TagInfoID =  TagInfoID,
                TagName =  TagName,
                UserInfoID =  UserInfoID,
                CreateTime =  CreateTime,
            };
        }
        
        
        public BizTagInfo (TagInfo dataInfo)
        {
             TagInfoID =  dataInfo.TagInfoID;
             TagName =  dataInfo.TagName;
             UserInfoID =  dataInfo.UserInfoID;
             CreateTime =  dataInfo.CreateTime;
        }
        
        public  BizTagInfo ()
        {
        
        } 
        

        public void Save()
        {
            if(TagInfoID!=0)
            {
                DAL.Update(ToModel());
            }else
            {
                DAL.Add(ToModel());
                var model = LoadByTagNameAndUserID(TagName,UserInfoID);
                if(model!=null)
                {
                    TagInfoID = model.TagInfoID;
                }
            }
        }


        public static BizTagInfo LoadByTagNameAndUserID(string tagname ,long userInfoID)
        {
           var model = DAL.GetByTagNameAndUserInfoID(tagname,userInfoID);
           return model != null ? new BizTagInfo(model) : null;
        }
	}
}