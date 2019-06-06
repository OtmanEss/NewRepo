using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IntecWebShopp.WebUI.Startup))]
namespace IntecWebShopp.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
