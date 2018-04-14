using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Administrating.Startup))]
namespace Administrating
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
