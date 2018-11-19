using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SNIESWebApplication.Startup))]
namespace SNIESWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
