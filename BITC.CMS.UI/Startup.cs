using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BITC.CMS.UI.Startup))]
namespace BITC.CMS.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
