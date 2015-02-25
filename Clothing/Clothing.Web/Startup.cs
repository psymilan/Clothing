using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Clothing.Web.Startup))]
namespace Clothing.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
