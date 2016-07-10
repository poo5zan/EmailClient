using EmailClient.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailClient.Web.Controllers.Mvc
{   
    public class MailController : MvcControllerBase
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }

        //[Route("email/compose")]
        //public ActionResult Create()
        //{
        //    return View();
        //}
    }
}