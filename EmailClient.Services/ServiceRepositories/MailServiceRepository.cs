using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailClient.Services.Dtos;
using EmailClient.Services.Infrastructure.Mail;
using System.ComponentModel.Composition;
using AE.Net.Mail;
using EmailClient.Common.Utilities;
using EmailClient.Data.DataContracts;
using EmailClient.Data.Entities;
using EmailClient.Data.DataRepositories;
using EmailClient.Common;

namespace EmailClient.Services.ServiceRepositories
{
    [Export(typeof(IMailServiceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MailServiceRepository : IMailServiceRepository
    {
        // [ImportingConstructor]
        public MailServiceRepository(IMailHelper mailHelper = null,
            IEmailSettingRepository emailSettingRepository = null)
        {
            _MailHelper = mailHelper ?? new MailHelper();
            _EmailSettingRepository = emailSettingRepository ?? new EmailSettingRepository();
        }

        IMailHelper _MailHelper;
        IEmailSettingRepository _EmailSettingRepository;
        public void DeleteEmail(MailMessageDto mailMessageDto, string loginEmail)
        {
            var mailSetting = _EmailSettingRepository.Get(loginEmail);
            // var mailMessage = new MailMessage();
            //SimpleMapper.PropertyMap<MailMessageDto, MailMessage>(mailMessageDto, mailMessage);
            //mailMessage.From = new System.Net.Mail.MailAddress(mailMessageDto.From);


            _MailHelper.DeleteEmail(mailSetting.Email, mailSetting.Password, mailMessageDto.Uid);
        }

        public List<MailMessageDto> GetEmails(string loginEmail, int startIndex)
        {
            var mailSettings = _EmailSettingRepository.Get(loginEmail);
            var emails = _MailHelper.GetEmails(mailSettings.Email, mailSettings.Password, startIndex);
            var emailMessages = new List<MailMessageDto>();
            foreach (var email in emails)
            {
                var emailMessage = new MailMessageDto();
                //SimpleMapper.PropertyMap<MailMessage, MailMessageDto>(email, emailMessage);
                emailMessage.From = email.From.Address;
                emailMessage.Body = email.Body;
                emailMessage.Date = email.Date;
                emailMessage.MailId = email.MessageID;
                emailMessage.Subject = email.Subject;
                StringBuilder sb = new StringBuilder();
                foreach (var to in email.To)
                {
                    sb.Append(to.Address);
                    sb.Append(";");
                }
                emailMessage.To = sb.ToString();
                emailMessage.Uid = email.Uid;
                emailMessages.Add(emailMessage);
            }
            return emailMessages;
        }

        public bool IsMailSettingsConfigured(string loginEmail)
        {
            var emailSetting = _EmailSettingRepository.Get(loginEmail);
            if (emailSetting == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool SaveMailSettings(MailSettingDto mailSettingDto)
        {
            var emailSetting = new EmailSetting();
            SimpleMapper.PropertyMap<MailSettingDto, EmailSetting>(mailSettingDto, emailSetting);
            emailSetting.Domain = EnumHelper.EmailDomain.Gmail.ToString();
            var response = _EmailSettingRepository.Add(emailSetting);
            if (response != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SendEmail(MailMessageDto mailMessageDto, string loginEmail)
        {
            var mailSetting = _EmailSettingRepository.Get(loginEmail);
            _MailHelper.SendEmail(mailMessageDto, mailSetting.Email, mailSetting.Password);
        }

        public bool IsMailSettingAvailable(string email)
        {
            return _EmailSettingRepository.IsMailSettingAvailable(email);
        }
    }
}
