using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmailClient.Web.Models;
using System.ComponentModel.Composition;
using EmailClient.Services.Services;
using EmailClient.Services.Dtos;
using EmailClient.Common.Utilities;

namespace EmailClient.Web.Repositories
{
    [Export(typeof(IMailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MailRepository : IMailRepository
    {

        //[ImportingConstructor]
        public MailRepository(IMailService mailService = null)
        {
            _MailService = mailService ?? new MailService();
        }

        IMailService _MailService;

        public void DeleteEmail(MailMessageViewModel mailMessageViewModel,string loginEmail)
        {
            var mailMessageDto = new MailMessageDto();
            SimpleMapper.PropertyMap<MailMessageViewModel, MailMessageDto>(mailMessageViewModel, mailMessageDto);
            _MailService.DeleteEmail(mailMessageDto, loginEmail);
            
        }

        public List<MailMessageViewModel> GetEmails(string loginEmail,int startIndex)
        {
            var emails = _MailService.GetEmails(loginEmail, startIndex);
            var mailMessageViewModels = new List<MailMessageViewModel>();
            foreach (var email in emails) {
                var mailMessageViewModel = new MailMessageViewModel();
                SimpleMapper.PropertyMap<MailMessageDto, MailMessageViewModel>(email, mailMessageViewModel);
                mailMessageViewModel.Date = email.Date.ToString();
                mailMessageViewModels.Add(mailMessageViewModel);
            }
            return mailMessageViewModels;
        }

        public bool IsMailSettingsConfigured(string loginEmail)
        {
            return _MailService.IsMailSettingsConfigured(loginEmail);
        }

        public bool SaveMailSettings(MailSettingViewModel mailSettingViewModel,string loginEmail)
        {
            var mailSettingDto = new MailSettingDto();
            SimpleMapper.PropertyMap<MailSettingViewModel, MailSettingDto>(mailSettingViewModel,mailSettingDto);
            mailSettingDto.LoginEmail = loginEmail;            
            return _MailService.SaveMailSettings(mailSettingDto);
           // throw new NotImplementedException();
        }

        public void SendEmail(MailMessageViewModel mailMessageViewModel,string loginEmail)
        {
            var mailMessageDto = new MailMessageDto();
            SimpleMapper.PropertyMap<MailMessageViewModel, MailMessageDto>(mailMessageViewModel, mailMessageDto);
            _MailService.SendEmail(mailMessageDto, loginEmail);
        }

        public bool IsMailSettingAvailable(string email)
        {
            return _MailService.IsMailSettingAvailable(email);
        }
    }
}