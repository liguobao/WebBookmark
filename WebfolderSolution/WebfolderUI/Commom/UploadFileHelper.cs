using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebfolderBo.Model;

namespace WebfolderUI
{
    public class UploadFileHelper
    {
        public static BizResultInfo UploadFile(HttpRequestBase requestBase)
        {
            BizResultInfo result = new BizResultInfo();

            if (requestBase.Files.Count==0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "没有文件呀，选择文件之后再上传啊。";
                return result;
            }
            var file = requestBase.Files[0];
            string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/userImg/";
            string filename = Path.GetFileName(file.FileName);
            file.SaveAs(Path.Combine(path, filename));
            result.IsSuccess = true;
            result.ResultID = "uploads/userImg/"+filename;
            return result;
        }
    }
}