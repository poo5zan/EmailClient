using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Services.Dtos
{
    public class MailMessageDto
    {
        public string MailId { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public string Uid { get; set; }
    }
}
