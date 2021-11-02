using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StoreFront.Startup))]
namespace StoreFront
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
