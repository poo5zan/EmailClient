using AE.Net.Mail;
using EmailClient.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Services.ServiceRepositories
{
    public interface IMailServiceRepository
    {
        bool IsMailSettingAvailable(string email);
        bool IsMailSettingsConfigured(string loginEmail);
        bool SaveMailSettings(MailSettingDto mailSettingDto);
        List<MailMessageDto> GetEmails(string loginEmail,int startIndex);
        void DeleteEmail(MailMessageDto mailMessageDto, string loginEmail);
        void SendEmail(MailMessageDto mailMessageDto, string loginEmail);
    }
}
