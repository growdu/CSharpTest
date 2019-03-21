using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MailTest
{
    /// <summary>
    /// 邮件测试程序，收发邮件以及下载邮件附件，使用mailkit,mimekit
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //创建获取邮件客户端并连接到邮件服务器
            ImapClient client = new ImapClient();
            //client.Connect("mail.gildata.com");
            //必须调用该事件获取服务器证书，否则无法正常连接
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
            //带端口号和协议的连接方式
            client.Connect("imap.gildata.com", 993, true);
            string account = "YHTX@gildata.com";
            string passWord = "Hello1234";
            client.Authenticate(account, passWord);
            //获取所有文件夹
            List<IMailFolder> mailFolderList = client.GetFolders(client.PersonalNamespaces[0]).ToList();
            //获取收件箱文件夹
            var folder = client.GetFolder("INBOX");
            //以只读方式打开文件夹
            folder.Open(MailKit.FolderAccess.ReadOnly);
            //选出日期在2019-03-14后的邮件
            var uidss = folder.Search(SearchQuery.DeliveredAfter(DateTime.Parse("2019-03-14")));
            folder.Fetch(uidss, MessageSummaryItems.UniqueId | MessageSummaryItems.Full);
            foreach (var item in uidss)
            {
                MimeMessage message = folder.GetMessage(new UniqueId(item.Id));
                foreach (MimePart attachment in message.Attachments)
                {
                    //下载附件
                    using (var cancel = new System.Threading.CancellationTokenSource())
                    {
                        string filePath = Path.Combine(".", attachment.FileName);
                        using (var stream = File.Create(filePath))
                        {
                            attachment.ContentObject.DecodeTo(stream, cancel.Token);
                        }
                    }
                }
                
            }
            //关闭文件
            folder.Close();
        }

        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }
    }
}
