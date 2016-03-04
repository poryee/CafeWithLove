using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CafeWithLove.Startup))]
namespace CafeWithLove
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
