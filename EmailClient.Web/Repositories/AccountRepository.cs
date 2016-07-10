using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using EmailClient.Web.Models;
using WebMatrix.WebData;

namespace EmailClient.Web.Repositories
{
    [Export(typeof(IAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountRepository : IAccountRepository
    {
        public void Initialize()
        {
            if (!WebSecurity.Initialized)
            {
                try
                {
                    WebSecurity.InitializeDatabaseConnection(
                                       "EmailClientConnection",
                                       "Account",
                                       "AccountId",
                                       "LoginEmail",
                                       autoCreateTables: true);
                }
                catch (Exception ex)
                {
                  
                }               
            }
        }

        public void Register(AccountRegisterViewModel accountRegisterViewModel)
        {
            WebSecurity.CreateUserAndAccount(accountRegisterViewModel.LoginEmail,accountRegisterViewModel.Password);
        }

        public bool Login(AccountLoginViewModel accountLoginViewModel)
        {
            return WebSecurity.Login(accountLoginViewModel.LoginEmail,
                accountLoginViewModel.Password,
                persistCookie: accountLoginViewModel.RememberMe);
        }

        public bool ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            return WebSecurity.ChangePassword(changePasswordViewModel.LoginEmail,
                changePasswordViewModel.OldPassword,
                changePasswordViewModel.NewPassword);
        }

        public bool UserExists(string loginEmail)
        {
            return WebSecurity.UserExists(loginEmail);
        }
    }
}