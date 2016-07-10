using AE.Net.Mail;
using EmailClient.Services.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Services.Infrastructure.Mail
{
    [Export(typeof(IMailHelper))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MailHelper : IMailHelper
    {
        ImapClient _imapClient;
        public MailHelper()
        {
           
        }
        private string _GmailImapHost = "imap.gmail.com";
        private int _GmailImapPort = 993;
        private string _GmailSmtpHost = "smtp.gmail.com";
        private int _GmailSmtpPort = 587;

        public AE.Net.Mail.MailMessage[] GetEmails(string email, string password,int startIndex = 0)
        {
           
            if (!_imapClient.IsConnected && !_imapClient.IsAuthenticated)
            {
                return null;
            }

            var msgCount = _imapClient.GetMessageCount();
            return _imapClient.GetMessages(startIndex, msgCount - 1, false);
        }

        // public void DeleteEmail(string email, string password, AE.Net.Mail.MailMessage mailMessage)
        public void DeleteEmail(string email, string password, string uid)
        {
            _imapClient = new ImapClient(_GmailImapHost, email, password, AuthMethods.Login, _GmailImapPort, true);
            if (_imapClient.IsConnected && _imapClient.IsAuthenticated)
            {
                _imapClient.DeleteMessage(uid);
            }            
        }

        public void SendEmail(MailMessageDto mailMessageDto, string emailId, string password)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(_GmailSmtpHost, _GmailSmtpPort);
                                
                var message = new System.Net.Mail.MailMessage()
                {
                    From = new MailAddress(emailId),
                    IsBodyHtml = true,
                    Body = mailMessageDto.Body,
                    Subject = mailMessageDto.Subject
                };

                //if multiple receipients
                if (mailMessageDto.To.IndexOf(';') > -1)
                {
                    var toAddresses = mailMessageDto.To.Split(';');
                    foreach (var a in toAddresses)
                    {
                        message.To.Add(new MailAddress(a));
                    }
                }
                else
                {
                    //single receipient
                    MailAddress address = new MailAddress(mailMessageDto.To);
                    message.To.Add(address);
                }
                               
                smtpClient.Credentials = new NetworkCredential(
                    emailId, password
                    );
                smtpClient.EnableSsl = true;
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                               
            }
        }

        public ImapClient GetImapClientInstance(string email, string password)
        {
            try
            {
                return _imapClient = new ImapClient(_GmailImapHost, email, password, AuthMethods.Login, _GmailImapPort, true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Replace(" ", "").ToLower().Contains("nosuchhost"))
                {
                    throw new Exception("Connection Issue");
                }
                if (ex.Message.Replace(" ", "").ToLower().Contains("nolookup")) {
                    throw new Exception("No user found");
                }
                if (ex.Message.Replace(" ", "").ToLower().Contains("webloginrequired")) {
                    throw new Exception("Security Issue");
                }
                if (ex.Message.Replace(" ", "").ToLower().Contains("invalidcredentials"))
                {
                    throw new Exception("Invalid credentials");
                }
                throw;
            }
        }
    }
}
