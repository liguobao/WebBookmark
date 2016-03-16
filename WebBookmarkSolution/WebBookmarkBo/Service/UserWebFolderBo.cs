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
                Dictionary<int, int> dicHash = new Dictionary<int, int>();
                foreach(var w in lstBizWebfolder)
                {
                    if (!dicHash.ContainsKey(w.IElementHashcode))
                        dicHash.Add(w.IElementHashcode,w.IElementHashcode);
                }
                var uid = lstBizWebfolder.FirstOrDefault().UserInfoID;
                var lstDataInfo = BizUserWebFolder.LoadAllByUID(uid);

                var dicDataInfo = lstDataInfo.Where(datainfo => dicHash.ContainsKey(datainfo.IElementHashcode)).ToDictionary(model=>model.IElementHashcode,model=>model);
                result.Target = dicDataInfo;

            }else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "插入数据失败，可能是外星人劫持了数据库了，也可能是海底电缆被可爱的鲨鱼咬断了....反正重试一下就是。";
            }

            return result;
        } 


        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="lstBizWebfolder"></param>
        /// <returns></returns>
        public static BizResultInfo BatchUpdateWebfolder(List<BizUserWebFolder> lstBizWebfolder)
        {
            BizResultInfo result = new BizResultInfo();
            if (lstBizWebfolder == null || lstBizWebfolder.Count == 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "插入数据不能为空。";
            }
            var lstModel = lstBizWebfolder.Select(biz => biz.ToModel()).ToList();
            var isSuccess = new UserWebFolderDAL().BatchUpdata(lstModel);
            result.IsSuccess = isSuccess;
            if (isSuccess)
            {
                result.SuccessMessage = "更新成功！";
            }else
            {
                result.ErrorMessage = "更新失败，可能是海底光纤断了...待会再试试咯。";
            }

            return result;
        }
    }
}
