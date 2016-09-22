using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreditStore.Admin.Startup))]
namespace CreditStore.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
