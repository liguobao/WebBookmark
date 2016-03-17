using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkBo.Model;
using WebBookmarkService.DAL;

namespace WebfolderBo.Service
{
    public class BookmarkInfoBo
    {
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
            var isSuccess = new BookmarkInfoDAL().BatchInsert(lstModel);
            result.IsSuccess = isSuccess;
            if (!isSuccess)
                result.ErrorMessage = "可能是海底光纤挂了...重新试一下咯！(打死不认是程序的问题！！！)";
            return result;
        }
    }
}
