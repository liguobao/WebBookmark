using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebfolderUI.Startup))]
namespace WebfolderUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
