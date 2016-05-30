using System;
using System.Collections.Generic;
using System.Linq;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkBo.Model
{
    public class BizUserWebBookmarkImportLog
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
		public long UserWebBookmarkImportLogID { get; set; }

        /// <summary>
        /// 用户UID
        /// </summary>
        public long UserInfoID { get; set; }

        /// <summary>
        /// 文件所在路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }


        #endregion


        private static UserWebBookmarkImportLogDAL DAL = new UserWebBookmarkImportLogDAL();

        public UserWebBookmarkImportLog ToModel()
        {
            return new UserWebBookmarkImportLog()
            {
                UserInfoID = UserInfoID,
                UserWebBookmarkImportLogID = UserWebBookmarkImportLogID,
                Path = Path,
                FileName = FileName,
                CreateTime = CreateTime,
            };
        }



        public BizUserWebBookmarkImportLog(UserWebBookmarkImportLog dataInfo)
        {
            if (dataInfo == null)
                return;
            UserInfoID = dataInfo.UserInfoID;
            UserWebBookmarkImportLogID = dataInfo.UserWebBookmarkImportLogID;
            Path = dataInfo.Path;
            FileName = dataInfo.FileName;
            CreateTime = dataInfo.CreateTime;
        }

        public BizUserWebBookmarkImportLog()
        {
        }


        /// <summary>
        /// 更新/插入
        /// </summary>
        public void Save()
        {
            
            if(UserWebBookmarkImportLogID== default(long))
            {
                DAL.Add(ToModel());
                var model = DAL.GetByFileName(UserInfoID,FileName);
                if(model!=null)
                {
                    UserWebBookmarkImportLogID = model.UserWebBookmarkImportLogID;
                }
            }
            else
            {
                DAL.Update(ToModel());
            }
        }


        /// <summary>
        /// 通过用户ID获取所有的导入记录
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static IEnumerable<BizUserWebBookmarkImportLog> LoadAllByUID(long uid)
        {

            var lstDataInfo = DAL.LoadByUID(uid);
            if(lstDataInfo!=null)
            {
                return lstDataInfo.Select(data => new BizUserWebBookmarkImportLog(data));
            }
            return null;
        }


        /// <summary>
        /// 加载最后一条用户的导入记录
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static BizUserWebBookmarkImportLog LoadImportLogID(long importLogID)
        {
           var model = DAL.GetByUserWebBookmarkImportLogID(importLogID);
           if (model != null)
               return new BizUserWebBookmarkImportLog(model);
            return null;
        }
    }
}
