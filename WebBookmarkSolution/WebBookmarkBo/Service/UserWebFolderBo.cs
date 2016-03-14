using System;
using System.Collections.Generic;
using System.Linq;
using WebBookmarkBo.Model;
using WebBookmarkService.DAL;

namespace WebfolderBo.Service
{
    public class UserWebFolderBo
    {
        public static BizResultInfo BatchAddUserWebfolder(List<BizUserWebFolder> lstBizWebfolder)
        {
            BizResultInfo result = new BizResultInfo();
            if(lstBizWebfolder == null || lstBizWebfolder.Count==0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "插入数据不能为空。";
            }
            var lstModel = lstBizWebfolder.Select(biz=>biz.ToModel()).ToList();  
            var isSuccess = new UserWebFolderDAL().BatchAdd(lstModel);
            if(isSuccess)
            {
                result.IsSuccess = isSuccess;
                result.SuccessMessage = "插入成功。";
                result.Target = lstBizWebfolder;

            }else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "插入数据失败，可能是外星人劫持了数据库了，也可能是海底电缆被可爱的鲨鱼咬断了....反正重试一下就是。";
            }

            return result;
        } 
    }
}
