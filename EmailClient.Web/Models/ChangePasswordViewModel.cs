using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailClient.Web.Models
{
    public class ChangePasswordViewModel
    {
        public string LoginEmail { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}