using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_5shop.Startup))]
namespace _5shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
