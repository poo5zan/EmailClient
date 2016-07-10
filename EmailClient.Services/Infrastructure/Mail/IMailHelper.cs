using AE.Net.Mail;
using EmailClient.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Services.Infrastructure.Mail
{
    public interface IMailHelper
    {
        ImapClient GetImapClientInstance(string email, string password);
        AE.Net.Mail.MailMessage[] GetEmails(string email, string password,int startIndex = 0);
        void SendEmail(MailMessageDto mailMessageDto, string emailId, string password);
        //void DeleteEmail(string email, string password, AE.Net.Mail.MailMessage mailMessage);
        void DeleteEmail(string email, string password, string uid);

    }
}
