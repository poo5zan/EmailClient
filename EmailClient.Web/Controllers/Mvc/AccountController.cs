using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EmailClient.Web.Models;
using EmailClient.Web.Core;
using System.ComponentModel.Composition;
using System.Web.Security;
using EmailClient.Web.Repositories;
using WebMatrix.WebData;

namespace EmailClient.Web.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("account")]
    public class AccountController : MvcControllerBase
    {
        [ImportingConstructor]
        public AccountController(IAccountRepository accountRepository)
        {
            _AccountRepository = accountRepository;
        }

        IAccountRepository _AccountRepository;

        [HttpGet]
        [Route("login")]
        public ActionResult Login(string returnUrl)
        {
            _AccountRepository.Initialize();
            return View(new AccountLoginViewModel() { ReturnUrl = returnUrl });
        }

        [Route("register")]
        public ActionResult Register()
        {
            _AccountRepository.Initialize();
            return View();
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            //WebSecurity.Logout();
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Index","Home");
           
        }

        //[Route("editMailSetting")]
        //public ActionResult EditMailSetting()
        //{
        //    return View();
        //}
    }
}