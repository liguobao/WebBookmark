using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebBookmarkBo.Model;

namespace WebBookmarkUI
{
    public class UploadFileHelper
    {
        public static BizResultInfo UploadFileToUserImg(HttpRequestBase requestBase)
        {
            BizResultInfo result = new BizResultInfo();

            if (requestBase.Files.Count==0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "没有文件呀，选择文件之后再上传啊。";
                return result;
            }
            var file = requestBase.Files[0];
            string path = AppDomain.CurrentDomain.BaseDirectory + "UploadFiles/UserImg/";
            string filename = Path.GetFileName(file.FileName);
            Random ran = new Random();
            int randKey = ran.Next(0, 99999);
            filename = DateTime.Now.ToString("yyyyMMdd")+ randKey + filename;
            file.SaveAs(Path.Combine(path, filename));
            result.IsSuccess = true;
            result.ResultID = "~/UploadFiles/UserImg/" + filename;
            return result;

           
        }


        public static BizResultInfo UploadFileToUserImportFile(HttpRequestBase requestBase)
        {
            BizResultInfo result = new BizResultInfo();

            if (requestBase.Files.Count == 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "没有文件呀，选择文件之后再上传啊。";
                return result;
            }
            var file = requestBase.Files[0];
            string path = AppDomain.CurrentDomain.BaseDirectory + "UploadFiles/UserImportFile/";
            string filename = Path.GetFileName(file.FileName);
            Random ran = new Random();
            int randKey = ran.Next(0, 99999);
            filename = DateTime.Now.ToString("yyyyMMdd") + randKey + filename;
            file.SaveAs(Path.Combine(path, filename));
            result.IsSuccess = true;
            result.ResultID = "~/UploadFiles/UserImportFile/" + filename;
            return result;


        }
    }
}