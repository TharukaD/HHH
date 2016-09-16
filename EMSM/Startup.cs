using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EMSM.Startup))]
namespace EMSM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
