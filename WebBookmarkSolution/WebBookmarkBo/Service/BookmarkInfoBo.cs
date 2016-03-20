using System;
using System.Collections.Generic;
using System.Linq;
using WebBookmarkBo.Model;
using WebBookmarkService.DAL;

namespace WebfolderBo.Service
{
    public class BookmarkInfoBo
    {
        private static BookmarkInfoDAL DAL = new BookmarkInfoDAL();

        public static BizResultInfo BatchSaveToDB(List<BizBookmarkInfo> lstBizBookmarkInfo)
        {
            BizResultInfo result = new BizResultInfo();
            if(lstBizBookmarkInfo==null || lstBizBookmarkInfo.Count==0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "数据是空的呀，我就直接返回了....";
                return result;
            }
            var lstModel = lstBizBookmarkInfo.Select(info => info.ToModel()).ToList();
            var isSuccess = DAL.BatchInsert(lstModel);
            result.IsSuccess = isSuccess;
            if (!isSuccess)
                result.ErrorMessage = "可能是海底光纤挂了...重新试一下咯！(打死不认是程序的问题！！！)";
            return result;
        }


        public static List<BizBookmarkInfo> LoadByUID(long uid)
        {
            var lstBiz = new List<BizBookmarkInfo>();
            var lstModel = DAL.GetListByUID(uid);
            lstBiz = lstModel.Select(model => new BizBookmarkInfo(model)).ToList();
            return lstBiz;
        }

    }
}
