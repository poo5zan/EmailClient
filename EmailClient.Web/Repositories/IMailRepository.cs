using EmailClient.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailClient.Web.Repositories
{
    public interface IMailRepository
    {
        bool IsMailSettingAvailable(string email);
        bool IsMailSettingsConfigured(string loginEmail);
        bool SaveMailSettings(MailSettingViewModel mailSettingViewModel,string loginEmail);
        List<MailMessageViewModel> GetEmails(string loginEmail,int startIndex);
        void DeleteEmail(MailMessageViewModel mailMessageViewModel, string loginEmail);
        void SendEmail(MailMessageViewModel mailMessageViewModel,string loginEmail);
    }
}