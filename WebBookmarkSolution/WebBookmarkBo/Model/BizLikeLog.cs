//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;
using System.Linq;
using WebBookmarkBo.Service;

namespace WebBookmarkBo.Model
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class BizLikeLog
	{
        private static readonly LikeLogDAL DAL = new LikeLogDAL();
        /// <summary>
        /// 主键
        /// </summary>
		public long LikeLogID{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 外键ID
        /// </summary>
		public long InfoID{get;set;}
            
        /// <summary>
        /// 类型
        /// </summary>
		public int InfoType{get;set;}


        public BizUserInfo LikeUserInfo { get; set; }

        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public LikeLog ToModel()
        {
            return new LikeLog()
            {
                LikeLogID =  LikeLogID,
                UserInfoID =  UserInfoID,
                InfoID =  InfoID,
                InfoType =  InfoType,
            };
        }
        
        
        public BizLikeLog (LikeLog dataInfo)
        {
             LikeLogID =  dataInfo.LikeLogID;
             UserInfoID =  dataInfo.UserInfoID;
             InfoID =  dataInfo.InfoID;
             InfoType =  dataInfo.InfoType;
        }
        
        public  BizLikeLog ()
        {
        
        } 
        

        public void Save()
        {
            if(LikeLogID!=0)
            {
                DAL.Update(ToModel());

            }else
            {
                DAL.Add(ToModel());
            }
        }


        public static BizLikeLog LoadUserIDAndBookmarkID(long userID,long bookmarkID)
        {
            var model = DAL.GetByUserInfoIDAndInfoIDAndInfoType(userID, bookmarkID, (int)InfoTypeEnum.Bookmark);
            return model != null ? new BizLikeLog(model) : null;
        }

        public static List<BizLikeLog> LoadByBookmarkID(long bookmarkID)
        {
            var lstModel = DAL.GetByInfoIDAndInfoType(bookmarkID, (int)InfoTypeEnum.Bookmark);
            if(lstModel !=null && lstModel.Count() >0 )
            {
                var dicUserIDToModel = UserInfoBo.GetListByUIDList(
                    lstModel.Select(model => model.UserInfoID).Distinct().ToList()
                    ).ToDictionary(model=>model.UserInfoID,model=>model);

                return lstModel.Select(model => new BizLikeLog(model) 
                { 
                    LikeUserInfo=dicUserIDToModel.ContainsKey(model.UserInfoID)
                    ? dicUserIDToModel[model.UserInfoID] :new BizUserInfo()
                }).ToList();
            }
            return  new List<BizLikeLog>();
           
        }
	}


    public enum InfoTypeEnum
    {
        Bookmark =1,
    }
}