using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailClient.Services.Dtos;
using System.ComponentModel.Composition;
using EmailClient.Services.ServiceRepositories;

namespace EmailClient.Services.Services
{
    [Export(typeof(IMailService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MailService : IMailService
    {
        [ImportingConstructor]
        public MailService(IMailServiceRepository mailServiceRepository = null)
        {
            _MailServiceRepository = mailServiceRepository ?? new MailServiceRepository();
        }

        IMailServiceRepository _MailServiceRepository;
        public void DeleteEmail(MailMessageDto mailMessageDto,string loginEmail)
        {
            _MailServiceRepository.DeleteEmail(mailMessageDto, loginEmail);
        }

        public List<MailMessageDto> GetEmails(string loginEmail, int startIndex)
        {
           return _MailServiceRepository.GetEmails(loginEmail,startIndex);
        }

        public bool IsMailSettingsConfigured(string loginEmail)
        {
            return _MailServiceRepository.IsMailSettingsConfigured(loginEmail);
        }

        public bool SaveMailSettings(MailSettingDto mailSettingDto)
        {
           return _MailServiceRepository.SaveMailSettings(mailSettingDto);
        }

        public void SendEmail(MailMessageDto mailMessageDto,string loginEmail)
        {
            _MailServiceRepository.SendEmail(mailMessageDto, loginEmail);
        }

        public bool IsMailSettingAvailable(string email)
        {
            return _MailServiceRepository.IsMailSettingAvailable(email);
        }
    }
}
