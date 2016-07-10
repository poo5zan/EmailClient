using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Services.Dtos
{
    public class MailSettingDto
    {
        public string LoginEmail { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
