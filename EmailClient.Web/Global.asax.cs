using EmailClient.Common.Bootstrapper;
using EmailClient.Data.DataRepositories;
using EmailClient.Services.Services;
using EmailClient.Web.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace EmailClient.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MailService).Assembly));
            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(EmailHistoryRepository).Assembly));
            CompositionContainer container = MefLoader.Init(catalog.Catalogs);

            DependencyResolver.SetResolver(new MefDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new MefApiDependencyResolver(container);

        }

        //protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        //{
        //    System.Web.HttpCookie authCookie =
        //System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
        //    if (authCookie != null)
        //    {
        //        FormsAuthenticationTicket authTicket = null;
        //        authTicket = FormsAuthentication.Decrypt(authCookie.Value);

        //        if (authTicket != null && !authTicket.Expired)
        //        {
        //            FormsAuthenticationTicket newAuthTicket = authTicket;

        //            if (FormsAuthentication.SlidingExpiration)
        //            {
        //                newAuthTicket = FormsAuthentication.RenewTicketIfOld(authTicket);
        //            }
        //            string userData = newAuthTicket.UserData;
        //            string[] roles = userData.Split(',');

        //            System.Web.HttpContext.Current.User =
        //                new System.Security.Principal.GenericPrincipal(new FormsIdentity(newAuthTicket), roles);
        //        }
        //    }

        //    //var authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
        //    //if (authCookie != null)
        //    //{
        //    //    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //    //    if (authTicket != null && !authTicket.Expired)
        //    //    {
        //    //        var roles = authTicket.UserData.Split(',');
        //    //        System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
        //    //    }
        //    //}
        //}

    }
}
