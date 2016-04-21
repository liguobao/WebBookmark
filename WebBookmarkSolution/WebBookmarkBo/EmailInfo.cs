using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkBo.BizModel;
using WebBookmarkService;

namespace WebBookmarkBo
{
    public class EmailInfo
    {
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 收件人邮箱
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string ReceiverName { get; set; }


        /// <summary>
        /// 发送
        /// </summary>
        public void Send()
        {
            try
            {
                var lstEmailConfiguration = BizConfigurationInfo.LoadByConfigurationNo(Conts.EmailConfigurationNO);
                    var emailAccount = lstEmailConfiguration.Find(c => c.ConfigurationKey == "EmailAccount").ConfigurationValue;
                    var emailPassword = lstEmailConfiguration.Find(c => c.ConfigurationKey == "EmailPassword").ConfigurationValue;
           
            
                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress(emailAccount);
                    mail.To.Add(new MailAddress(Receiver));

                    mail.Subject = Subject;
                    mail.SubjectEncoding = Encoding.UTF8;

                    mail.Body = Body;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.qq.com";
                    smtp.Credentials = new NetworkCredential(emailAccount, emailPassword);

                    smtp.Send(mail);
            }catch(Exception ex)
            {
                LogHelper.WriteException("SendEmail Exception", ex, this);
            }
          

        }


    }
}
