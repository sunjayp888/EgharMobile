using Microsoft.Owin;
using Egharpay;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Egharpay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
