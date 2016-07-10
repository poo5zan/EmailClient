using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailClient.Common.Validation
{
    public static class ValidationHelper
    {
        public static bool IsEmailValid(string email)
        {
            var regex = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            var r = Regex.Match(email, regex);
            if (r.Success)
            {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
