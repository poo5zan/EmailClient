using EmailClient.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Web.Repositories
{
    public interface IAccountRepository
    {
        void Initialize();
        void Register(AccountRegisterViewModel accountRegisterViewModel);
        bool Login(AccountLoginViewModel accountLoginViewModel);
        bool ChangePassword(ChangePasswordViewModel changePasswordViewModel);
        bool UserExists(string loginEmail);
    }
}
