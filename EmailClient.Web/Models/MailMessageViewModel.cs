using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailClient.Web.Models
{
    public class MailMessageViewModel
    {
        public string MailId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
        public string Uid { get; set; }
    }
}