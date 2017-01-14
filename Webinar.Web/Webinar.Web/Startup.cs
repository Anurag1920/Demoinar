using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Webinar.Web.Startup))]
namespace Webinar.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
