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
            
            if (!IsIMGAllowedExtension(file.InputStream))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "啦啦啦，文件类型不合法，你娃是来整木马的？";

                if (System.IO.File.Exists(path+filename))
                {
                    System.IO.File.Delete(path + filename);
                }
                return result;
            }



            result.IsSuccess = true;
            result.ResultID = "~/UploadFiles/UserImg/" + filename;
            return result;

           
        }


     
        public static bool IsIMGAllowedExtension(Stream stream)
        {
            System.IO.BinaryReader r = new System.IO.BinaryReader(stream);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();

            }
            catch
            {

            }
            r.Close();
            if (fileclass == "255216" || fileclass == "7173" || fileclass =="6677" || fileclass == "13780")//说明255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar
            {
                return true;
            }
            else
            {
                return false;
            }

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
            if (file.ContentType.ToLower() != "text/html" && file.ContentType.ToLower() != "text/htm")
            {
                result.IsSuccess = false;
                result.ErrorMessage = "文件格式不正确，请上传后缀为HTML/HTM的书签文件。";
                return result;
            }

            string path = AppDomain.CurrentDomain.BaseDirectory + "UploadFiles/UserImportFile/";
            string filename = Path.GetFileName(file.FileName);
            Random ran = new Random();
            int randKey = ran.Next(0, 99999);
            filename = DateTime.Now.ToString("yyyyMMdd") + randKey + filename;
            file.SaveAs(Path.Combine(path, filename));
            result.IsSuccess = true;

            BizUserWebBookmarkImportLog importLog = new BizUserWebBookmarkImportLog();
            importLog.FileName = filename;
            importLog.Path = "~/UploadFiles/UserImportFile/" + filename;
            importLog.CreateTime = DateTime.Now;
            result.Target = importLog;
            return result;


        }
    }
}