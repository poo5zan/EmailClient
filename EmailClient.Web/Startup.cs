using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmailClient.Web.Startup))]
namespace EmailClient.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
