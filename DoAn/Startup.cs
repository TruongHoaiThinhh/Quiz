using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoAn.Startup))]
namespace DoAn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
