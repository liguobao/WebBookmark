using System;
using System.Collections.Generic;
using System.Linq;
using WebfolderService.DAL;
using WebfolderService.Model;

namespace WebfolderBo.Model
{
    public class BizUserWebFolderImportLog
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
		public long UserWebFolderImportLogID { get; set; }

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


        public UserWebFolderImportLog ToModel()
        {
            return new UserWebFolderImportLog()
            {
                UserInfoID = UserInfoID,
                UserWebFolderImportLogID = UserWebFolderImportLogID,
                Path = Path,
                FileName = FileName,
                CreateTime = CreateTime,
            };
        }



        public BizUserWebFolderImportLog(UserWebFolderImportLog dataInfo)
        {
            if (dataInfo == null)
                return;
            UserInfoID = dataInfo.UserInfoID;
            UserWebFolderImportLogID = dataInfo.UserWebFolderImportLogID;
            Path = dataInfo.Path;
            FileName = dataInfo.FileName;
            CreateTime = dataInfo.CreateTime;
        }

        public BizUserWebFolderImportLog()
        {
        }


        /// <summary>
        /// 更新/插入
        /// </summary>
        public void Save()
        {
            
            if(UserWebFolderImportLogID== default(long))
            {
                new UserWebFolderImportLogDAL().Add(ToModel());
            }
            else
            {
                new UserWebFolderImportLogDAL().Update(ToModel());
            }
        }


        /// <summary>
        /// 通过用户ID获取所有的导入记录
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static IEnumerable<BizUserWebFolderImportLog> LoadAllByUID(long uid)
        {
           
            var lstDataInfo = new UserWebFolderImportLogDAL().LoadByUID(uid);
            if(lstDataInfo!=null)
            {
                return lstDataInfo.Select(data => new BizUserWebFolderImportLog(data));
            }
            return null;
        }


        /// <summary>
        /// 加载最后一条用户的导入记录
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static BizUserWebFolderImportLog LoadOne(long uid)
        {
            BizUserWebFolderImportLog biz = null;
            var list = LoadAllByUID(uid);
            if(list!=null && list.Count() >0)
            {
                biz = list.FirstOrDefault();
            }
            return biz;
        }
    }
}
